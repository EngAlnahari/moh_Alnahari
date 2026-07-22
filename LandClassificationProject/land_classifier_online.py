import torch
import cv2
import numpy as np
from segment_anything import sam_model_registry, SamAutomaticMaskGenerator

class LandSegmenterSAM:
    def __init__(self, model_path="sam_vit_b_01ec64.pth"):
        print("Loading SAM model...")
        sam = sam_model_registry["vit_b"](checkpoint=model_path)
        sam.to("cpu")

        # إعدادات محسنة للسرعة والدقة
        self.mask_generator = SamAutomaticMaskGenerator(
            model=sam,
            points_per_side=16,  # تقليل من 32 إلى 16 لتسريع المعالجة
            pred_iou_thresh=0.88,  # زيادة العتبة لتقليل عدد القطع
            stability_score_thresh=0.95,  # زيادة العتبة لتحسين الجودة
            min_mask_region_area=5000  # زيادة لتجاهل القطع الصغيرة
        )
        print("SAM Loaded Successfully")

        # نطاقات الألوان مبنية لتحسين الدقة (قمت بضبطها بناءً على الصورة التي أرسلتها)
        # كل قيمة هي (H, S, V)
        self.color_ranges = {
            'أرض زراعية': [
                ((35, 40, 40), (85, 255, 255)),    # أخضر رئيسي
                ((25, 30, 40), (35, 200, 255)),    # أخضر مصفح/مصفر
            ],
            'يابسة': [
                ((10, 20, 80), (25, 150, 255)),    # بني/ترابي
                ((8, 30, 60), (20, 120, 200)),     # بني فاتح
            ],
            'متصحرة': [
                ((0, 0, 160), (20, 40, 255)),      # تراب فاتح / رملي
            ],
            'طريق إسفلتي': [
                ((0, 0, 5), (180, 40, 110)),      # رمادي داكن (أساسا V منخفض، S منخفض)
                ((0, 0, 0), (180, 20, 70)),
            ],
            'مبنى': [
                ((0, 0, 100), (25, 60, 255)),      # خرسانة فاتحة / رمادي فاتح
                ((0, 30, 60), (10, 255, 200)),     # ألوان بني مبنى قديمة
            ]
        }

        # ألوان للرسم (BGR)
        self.draw_colors = {
            'أرض زراعية': (0, 255, 0),
            'يابسة': (0, 255, 255),
            'متصحرة': (0, 200, 255),
            'طريق إسفلتي': (120, 120, 120),
            'مبنى': (0, 0, 255),
            'غير معروف': (255, 255, 255)
        }

    def _mask_from_polygon(self, image_shape, polygon):
        mask = np.zeros(image_shape[:2], dtype=np.uint8)
        cv2.fillPoly(mask, [polygon.astype(np.int32)], 255)
        return mask

    def _shadow_compensate_hsv(self, hsv):
        """
        بسيط: نعطي قيمة V مهيأة بقليل (CLAHE على V) للتخفيف من تأثير الظلال.
        لا نغيّر HSV الأصلي، نستخدم نسخة لمعالجة التصنيف.
        """
        h, s, v = cv2.split(hsv)
        clahe = cv2.createCLAHE(clipLimit=2.0, tileGridSize=(8,8))
        v_eq = clahe.apply(v)
        hsv_eq = cv2.merge([h, s, v_eq])
        return hsv_eq

    def _texture_variance(self, image, mask):
        """
        حساب تباين لابلاسيان داخل المنطقة كمعيار للنسيج.
        قيمة عالية -> مباني أو تضاريس خشنة.
        """
        gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        lap = cv2.Laplacian(gray, cv2.CV_64F)
        lap_masked = lap * (mask.astype(np.float64) / 255.0)
        # جمع القيم داخل المنطقة
        nonzero = mask > 0
        if np.count_nonzero(nonzero) == 0:
            return 0.0
        vals = lap[nonzero]
        return float(np.var(vals))

    def classify_region_by_color_and_texture(self, image, polygon):
        """
        التصنيف: نستخدم HSV ranges لكل نوع + فحص نسيج للتحقق من المباني.
        نطبق تعويض بسيط للظلال (CLAHE على V) ثم نحسب نسبة البيكسلات المنتمية لكل فئة داخل الـ polygon.
        نرجع (label, score_ratio)
        """
        mask = self._mask_from_polygon(image.shape, polygon)
        if cv2.countNonZero(mask) == 0:
            return "غير معروف", 0.0

        hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)
        hsv_eq = self._shadow_compensate_hsv(hsv)  # نسخة مُحسَّنة للإضاءة

        best_label = "غير معروف"
        best_ratio = 0.0

        # تحقق لكل نوع
        for land_type, ranges in self.color_ranges.items():
            match_mask = np.zeros_like(mask)
            for (lower, upper) in ranges:
                lower = np.array(lower, dtype=np.uint8)
                upper = np.array(upper, dtype=np.uint8)
                temp = cv2.inRange(hsv_eq, lower, upper)
                match_mask = cv2.bitwise_or(match_mask, temp)

            final_mask = cv2.bitwise_and(match_mask, match_mask, mask=mask)
            n_total = cv2.countNonZero(mask)
            n_match = cv2.countNonZero(final_mask)
            ratio = n_match / n_total if n_total > 0 else 0.0

            # update best if ratio higher
            if ratio > best_ratio:
                best_ratio = ratio
                best_label = land_type

        # الآن نضيف فحص النسيج لتمييز المبنى عن الطرق أو اليابسة
        texture = self._texture_variance(image, mask)
        # معايير عملية للتفريق:
        # - إذا أفضل تصنيف هو طريق ولكن النسيج عالي جدًا => قد يكون مبنى
        # - إذا أفضل تصنيف هو مبنى ولكن النسبة ضعيفة وإجمالي S منخفض => قد يكون طريق
        if best_label == 'طريق إسفلتي':
            if texture > 200.0:  # عتبة عملية: مباني/أسطح تظهر تباين لابلاسيان أعلى
                # تحقق إذا اللون يشبه مبنى كذلك
                # نعيد الحساب لعلامة 'مبنى' لمعرفة النسبة
                building_ratio = 0.0
                if 'مبنى' in self.color_ranges:
                    match_mask = np.zeros_like(mask)
                    for (lower, upper) in self.color_ranges['مبنى']:
                        lower = np.array(lower, dtype=np.uint8)
                        upper = np.array(upper, dtype=np.uint8)
                        temp = cv2.inRange(hsv_eq, lower, upper)
                        match_mask = cv2.bitwise_or(match_mask, temp)
                    final_mask = cv2.bitwise_and(match_mask, match_mask, mask=mask)
                    building_ratio = cv2.countNonZero(final_mask) / cv2.countNonZero(mask)
                if building_ratio > 0.2:  # مبنى محتمل
                    best_label = 'مبنى'
                    best_ratio = building_ratio

        if best_label == 'مبنى':
            # إذا كان النسيج منخفض جداً ولكن اللون يشير لمبنى فقد يكون سطوح لأسفلت/خرسانة ملساء (نفهمها كـ مبنى).
            # لا تعديل هنا، لكن يمكن تسجيل ذلك لاحقًا كحالة غير مؤكدة.
            pass

        # حد أدنى للقبول: إن كانت النسبة قليلة جدًا، نعتبر "غير معروف"
        if best_ratio < 0.25:
            return "غير معروف", best_ratio

        return best_label, best_ratio

    def segment_and_classify(self, image):
        """
        يعيد قائمة من القطع: Polygon + النوع + لون رسم + مركز + الطول والعرض + score
        """
        masks = self.mask_generator.generate(image)
        results = []

        for m in masks:
            mask = m['segmentation'].astype(np.uint8) * 255
            contours, _ = cv2.findContours(mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

            for cnt in contours:
                area = cv2.contourArea(cnt)
                if area < 1500:  # تجاهل القطع الصغيرة جداً وليست مهمة
                    continue

                epsilon = 0.01 * cv2.arcLength(cnt, True)
                approx = cv2.approxPolyDP(cnt, epsilon, True)
                polygon = approx.reshape(-1, 2)

                label, score = self.classify_region_by_color_and_texture(image, polygon)
                color = self.draw_colors.get(label, (255, 255, 255))

                xs = polygon[:, 0]
                ys = polygon[:, 1]
                width = int(max(xs) - min(xs))
                height = int(max(ys) - min(ys))
                center_x = int(np.mean(xs))
                center_y = int(np.mean(ys))

                results.append({
                    "polygon": polygon,
                    "label": label,
                    "color": color,
                    "center": (center_x, center_y),
                    "width": width,
                    "height": height,
                    "bbox": (int(min(xs)), int(min(ys)), int(max(xs)), int(max(ys))),
                    "score": float(score)
                })

        # نرتب النتائج حسب المساحة من الأكبر للأصغر لتجنب تداخل النصوص للقطع الكبيرة
        results.sort(key=lambda r: (r['width'] * r['height']), reverse=True)
        return results
