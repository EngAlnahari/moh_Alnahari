import torch
import cv2
import numpy as np
from segment_anything import sam_model_registry, SamAutomaticMaskGenerator
from land_classifier_online import LandSegmenterSAM as OnlineLandSegmenterSAM

class LandColorClassifier:
    """
    فئة مصنف الأراضي باللون - تغلف وظائف land_classifier_online
    """
    def __init__(self, model_path="sam_vit_b_01ec64.pth"):
        self.segmenter = OnlineLandSegmenterSAM(model_path)
    
    def classify_land(self, image_path):
        """
        تصنيف الصورة وإرجاع التصنيف الرئيسي والنتائج التفصيلية
        """
        image = cv2.imread(image_path)
        if image is None:
            raise ValueError(f"Cannot read image: {image_path}")
        
        results = self.segmenter.segment_and_classify(image)
        
        # حساب النسب
        total_area = sum(r['width'] * r['height'] for r in results)
        if total_area == 0:
            return 'unknown', {}
        
        class_counts = {}
        for r in results:
            label = r['label']
            area = r['width'] * r['height']
            class_counts[label] = class_counts.get(label, 0) + area
        
        # إيجاد التصنيف الرئيسي
        main_class = max(class_counts, key=class_counts.get) if class_counts else 'unknown'
        
        # حساب النسب
        ratios = {k: v / total_area for k, v in class_counts.items()}
        
        # إضافة معلومات إضافية
        ratios['texture_variance'] = self._calculate_texture_variance(image)
        ratios['dominant_colors'] = self._get_dominant_colors(image)
        
        return main_class, ratios
    
    def create_land_map(self, image_path):
        """
        إنشاء خريطة تصنيف الأراضي
        """
        image = cv2.imread(image_path)
        results = self.segmenter.segment_and_classify(image)
        
        # إنشاء خريطة ملونة
        land_map = image.copy()
        for r in results:
            pts = r["polygon"].astype(np.int32)
            color = r["color"]
            cv2.fillPoly(land_map, [pts], color)
            cv2.polylines(land_map, [pts], True, (255, 255, 255), 1)
        
        return land_map
    
    def _calculate_texture_variance(self, image):
        """حساب تباين النسيج"""
        gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        lap = cv2.Laplacian(gray, cv2.CV_64F)
        return float(np.var(lap))
    
    def _get_dominant_colors(self, image, k=5):
        """استخراج الألوان السائدة"""
        pixels = image.reshape(-1, 3)
        pixels = pixels.astype(np.float32)
        
        criteria = (cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 10, 1.0)
        _, labels, centers = cv2.kmeans(pixels, k, None, criteria, 10, cv2.KMEANS_RANDOM_CENTERS)
        
        return centers.astype(int).tolist()

class LandSegmenterSAM:
    def __init__(self, model_path="sam_vit_b_01ec64.pth"):
        print("Loading SAM model...")
        sam = sam_model_registry["vit_b"](checkpoint=model_path)
        sam.to("cpu")

        # إعدادات SAM المحسنة لاكتشاف أدق التفاصيل
        self.mask_generator = SamAutomaticMaskGenerator(
            model=sam,
            points_per_side=64,             # تمت الزيادة من 32 إلى 64 لمسح أدق
            pred_iou_thresh=0.7,            # تقليل العتبة لاكتشاف قطع أكثر
            stability_score_thresh=0.85,    # زيادة الحساسية للحدود
            min_mask_region_area=100        # تقليل المساحة لاكتشاف أصغر المباني (كانت 2000)
        )
        print("SAM Loaded Successfully")

    def _mask_from_polygon(self, image_shape, polygon):
        mask = np.zeros(image_shape[:2], dtype=np.uint8)
        cv2.fillPoly(mask, [polygon.astype(np.int32)], 255)
        return mask

    def segment_and_classify(self, image):
        """
        يعيد قائمة من القطع: polygon + mask
        """
        masks = self.mask_generator.generate(image)
        results = []

        for m in masks:
            mask = m['segmentation'].astype(np.uint8) * 255
            contours, _ = cv2.findContours(mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

            for cnt in contours:
                area = cv2.contourArea(cnt)
                if area < 50:  # تم تقليلها لاكتشاف أصغر التفاصيل
                    continue

                epsilon = 0.01 * cv2.arcLength(cnt, True)
                approx = cv2.approxPolyDP(cnt, epsilon, True)
                polygon = approx.reshape(-1, 2)

                results.append({
                    "polygon": polygon
                })

        return results
