# الجرد الهندسي الكامل للمشروع

**تاريخ الجرد:** 24 يوليو 2026  
**نطاق الجرد:** `D:\EngMohammed` + `D:\EngMohammed_Assets_Extracted` + `D:\EngMohammed_Assets`  
**طريقة القراءة:** فحص شجرة الملفات، ملفات الحلول والمشروعات والإعدادات، المراجع البرمجية، حالة Git، وأحجام/بصمات الملفات. لم يُعدّل أي ملف من ملفات المشروع أثناء التحليل.

## 1. الخلاصة التنفيذية

هذه المجلدات الثلاثة تمثل بيئة مشروع واحدة، لكنها تحتوي طبقات مختلفة:

```text
D:\EngMohammed                         ← المستودع Git الحالي (الكود والخدمات الحالية)
├─ Mntry_Awqaf                          ← تطبيق MVC الحالي لـ Tanjez/Awqaf
├─ TestProject                          ← API وتجارب وقاعدة SQL Server
├─ LandClassificationProject            ← تطبيق Python للذكاء الاصطناعي/تصنيف الأراضي
├─ hhh                                  ← نماذج واجهات HTML
└─ قواعد بيانات، نسخ احتياطية، وثائق

D:\EngMohammed_Assets_Extracted        ← استخراج جزئي من اللقطة التاريخية
└─ Mntry_Awqaf + TestProject

D:\EngMohammed_Assets                  ← مستودع الأصول المحلية الكبيرة
├─ sam_vit_b_01ec64.pth                 ← أوزان نموذج الذكاء الاصطناعي
├─ alnahari22-05-2026.rar               ← الأرشيف الأصلي
└─ alnahari22-05-2026\                 ← اللقطة الموسعة المفكوكة من الأرشيف
```

المستودع Git الحالي على الفرع `main` ومتصل بـ `https://github.com/EngAlnahari/moh_Alnahari.git`. لا توجد ملفات غير متتبعة؛ توجد ملفات محلية متجاهلة حسب `.gitignore`، أهمها قواعد البيانات ومخرجات البناء. حجم مجلد الأصول كله نحو **1,869.61 MB**.

## 2. خريطة المشاريع والمجلدات

| المجلد/المشروع | التقنية والدور | المكونات المهمة | حالته بالنسبة إلى Git |
|---|---|---|---|
| `Mntry_Awqaf/Mntry_Awqaf` | ASP.NET Core MVC، .NET 8 | Controllers، Models، Views، `wwwroot`، SQLite للنماذج الديناميكية | الكود وموارد الواجهة متتبعة؛ `bin` و`obj` و`.vs` متجاهلة |
| `TestProject/Test` | ASP.NET Core Web API، .NET 8 | Controllers، Models، EF Core Migrations، Services، PDFs وUploads | متتبع؛ يتصل بـ SQL Server `Awqaf33` |
| `TestProject/CheckData` | أداة Console .NET 8 | فحص بيانات SQL Server | متتبع |
| `TestProject/InsertArabicData` | أداة Console .NET 8 | إدراج/إصلاح بيانات عربية | متتبع |
| `LandClassificationProject` | Python / FastAPI / OpenCV / PyTorch | واجهة/API لتجزئة وتصنيف الأراضي بـ SAM | متتبع؛ يعتمد على نموذج خارجي |
| `hhh` | HTML/CSS/JS | نماذج واجهات وعقود وعروض عمل | متتبع |
| الجذر | SQL، تقارير، ملفات HTML تجريبية، تشغيل | `run_all.bat`، سكربتات SQL ووثائق | معظمها متتبع؛ قواعد البيانات متجاهلة |
| `EngMohammed_Assets_Extracted` | لقطة تاريخية مفكوكة | نسخة من `Mntry_Awqaf` و`TestProject` | خارج المستودع الرئيسي |
| `EngMohammed_Assets/alnahari22-05-2026` | لقطة تاريخية موسعة | المصدر المفكوك ومخرجات Debug/Release/Publish | خارج المستودع الرئيسي |

## 3. الأصول الكبيرة خارج GitHub

| الأصل | المكان | الحجم | الاستخدام/العلاقة |
|---|---|---:|---|
| `sam_vit_b_01ec64.pth` | `D:\EngMohammed_Assets` | 375,042,383 بايت (357.67 MB) | أوزان PyTorch لنموذج Segment Anything؛ يحتاجها `LandClassificationProject` |
| `alnahari22-05-2026.rar` | `D:\EngMohammed_Assets` | 415,308,496 بايت (396.07 MB) | الأرشيف الأصلي/لقطة مرجعية تاريخية لمشروعي `Mntry_Awqaf` و`TestProject` |
| `alnahari22-05-2026\` | `D:\EngMohammed_Assets` | 1,115.87 MB، 6,325 ملفًا | النسخة المفكوكة الموسعة من اللقطة التاريخية؛ تشمل كودًا ومخرجات بناء ونشر |
| `EngMohammed_Assets_Extracted` | مجلد مستقل | 481.28 MB، 2,748 ملفًا | استخراج/نسخة فرعية من اللقطة السابقة؛ مرجع استعادة وليس مشروعًا مستقلاً |

ملف `.gitignore` يستبعد عمدًا امتدادات النماذج (`.pth/.pt/.onnx/.ckpt`)، الأرشيفات (`.rar/.zip/.7z`) وقواعد البيانات (`.mdf/.ldf/.bak/.db/.sqlite`). لذلك لا يمكن لاستنساخ GitHub وحده تشغيل كل وظائف البيئة.

## 4. ربط كل أصل بالمشروع المستهلك

| الأصل | المشروع المستهلك | الدليل الفني | ملاحظة تشغيلية |
|---|---|---|---|
| `sam_vit_b_01ec64.pth` | `LandClassificationProject` | `config.py` يحدد الاسم، و`land_classifier.py` و`land_classifier_online.py` يمررانه إلى `sam_model_registry["vit_b"]` | المسار الحالي نسبي؛ يلزم وضع الملف في مجلد التشغيل أو تمرير مساره صراحةً |
| `alnahari22-05-2026.rar` | `Mntry_Awqaf` و`TestProject` | اسم/بنية اللقطة يحتويان المشروعين والحلول التابعة لهما | أصل استعادة/أرشفة، وليس تبعية تشغيل وقت التنفيذ |
| `alnahari22-05-2026\` | `Mntry_Awqaf` و`TestProject` | لقطة مفكوكة تشمل الكود والملفات التنفيذية و`wwwroot` و`bin/obj` | لا ينبغي البناء أو التطوير داخلها؛ مرجع تاريخي فقط |
| `EngMohammed_Assets_Extracted` | `Mntry_Awqaf` و`TestProject` | يحتوي نسخة أصغر من اللقطة نفسها | مطابق، للمحتوى غير البنائي، لـ 163 ملفًا من اللقطة الموسعة؛ لا يمثل مصدرًا مستقلاً |
| `Awqaf33.mdf` و`Awqaf33_log.ldf` | `TestProject` وسكربتات SQL | إعداد `TestProject/Test/appsettings.json` وسكربتات SQL تشير إلى قاعدة `Awqaf33` | ملفات SQL Server محلية، ليست داخل مجلد Assets لكنها خارج GitHub |
| `SmartBusStation1.bak` | غير محسوم | لا توجد إشارة برمجية مباشرة إليه | نسخة احتياطية SQL Server محفوظة؛ علاقتها الوظيفية الحالية غير موثقة |
| `dynamic_forms.db` | `Mntry_Awqaf` | وجود `DynamicFormsDbContext` وحزمة EF Core SQLite | قاعدة محلية للنماذج الديناميكية؛ يلزم تأكيد أي الملفين المعتمدين فعليًا |

## 5. ملف `sam_vit_b_01ec64.pth`

المسار المتاح هو `D:\EngMohammed_Assets\sam_vit_b_01ec64.pth`، وبصمته SHA-256 هي:

`EC2DF62732614E57411CDCF32A23FFDF28910380D03139EE0F4FCBE91EB8C912`

يُستخدم بواسطة صنفي `LandSegmenterSAM` في:

- `LandClassificationProject/land_classifier.py`
- `LandClassificationProject/land_classifier_online.py`

ويُستدعى أيضًا عبر `api.py`، كما تسميه قيمة `SAM_MODEL_PATH` في `config.py`. المشروع يطلب `segment-anything` و`torch` ضمن `requirements.txt`. لم يُعثر على أي مستهلك آخر للملف ضمن المشاريع الثلاثة.

## 6. قواعد البيانات والنسخ الاحتياطية

| الملف | المكان | الحجم | التصنيف والحالة |
|---|---|---:|---|
| `Awqaf33.mdf` | جذر المستودع | 8 MB | قاعدة SQL Server محلية؛ متجاهلة من Git |
| `Awqaf33_log.ldf` | جذر المستودع | 8 MB | سجل SQL Server المرافق؛ متجاهل من Git |
| `SmartBusStation1.bak` | جذر المستودع | 3.15 MB | نسخة احتياطية SQL Server؛ متجاهلة من Git؛ لا مرجع مباشر |
| `dynamic_forms.db` | `Mntry_Awqaf/Mntry_Awqaf` | 20 KB | SQLite محلية؛ متجاهلة من Git |
| `DynamicForms.db` | `Mntry_Awqaf/Mntry_Awqaf` | 0 بايت | SQLite فارغة/بديلة محتملة؛ متجاهلة |
| `Mntry_AwqafTest.db` | `TestProject/Test` | 0 بايت | ملف محلي فارغ؛ متجاهل |

`TestProject` يستهدف قاعدة SQL Server اسمها `Awqaf33`. توجد كذلك سكربتات لإدراج البيانات العربية وإصلاح الترميز، مثل `fix_all_arabic_data.sql` وملفات SQL داخل `TestProject`. إعداد الاتصال ومواد النشر تحتوي بيانات حساسة/ثابتة؛ يجب نقلها إلى Secret Store أو متغيرات بيئة قبل أي نشر عام.

## 7. ما كان خارج GitHub أو مفقودًا منه

المقصود هنا «غير متاح من الاستنساخ الحالي لـ GitHub»، وليس «مفقودًا من القرص»:

1. **أصول تشغيل لازمة:** نموذج SAM `sam_vit_b_01ec64.pth`؛ لا يعمل تصنيف الأراضي بكامل وظيفته بدونه.
2. **بيانات محلية:** `Awqaf33.mdf` و`Awqaf33_log.ldf` و`SmartBusStation1.bak` وقواعد SQLite المحلية؛ كلها مستبعدة بقواعد Git.
3. **لقطة تاريخية كاملة:** الأرشيف RAR والنسخة المفكوكة ومجلد الاستخراج؛ لا توجد في المستودع الرئيسي.
4. **مخرجات بيئية غير قابلة لإعادة الاستخدام كمصدر:** `bin` و`obj` و`.vs` و`__pycache__`؛ وهي مستبعدة صحيحًا من Git.
5. بالمقارنة بين المحتوى غير البنائي للمجلد المستخرج والمستودع الرئيسي، توجد ملفان تاريخيان فقط لا يقابلهما اسم مطابق في المشروع الرئيسي: `Mntry_Awqaf/.gitattributes` و`Mntry_Awqaf/.gitignore`. توجد ثلاث نسخ تاريخية مختلفة من `appsettings.json` و`DbContextFirst.cs` و`DbContextFirstModelSnapshot.cs`. لا ينبغي استعادتها تلقائيًا؛ المشروع الرئيسي هو الخط المرجعي الحالي.

## 8. التكرار والملفات غير الضرورية تشغيليًا

- `EngMohammed_Assets_Extracted` مطابق تمامًا، خارج مجلدات البناء، لـ **163** ملفًا من اللقطة الموسعة داخل `EngMohammed_Assets/alnahari22-05-2026`; لذلك هو نسخة تكرارية للاستعادة.
- المجلد الموسع نفسه يحتفظ بمخرجات كثيفة قابلة لإعادة التوليد: نحو **375.42 MB** في `Mntry_Awqaf/bin` و**475.62 MB** في `Mntry_Awqaf/obj`، إضافة إلى مخرجات `TestProject` وبيانات Visual Studio. هذا يفسر الحجم الكبير ولا يمثل أصول مصدرية فريدة.
- في المستودع العامل توجد مخرجات محلية متجاهلة مجموعها نحو **304.20 MB** (1251 ملفًا)، يغلب عليها `bin` و`obj` و`.vs`. هذه ليست ملفات يجب أرشفتها كأصول.
- توجد نسخ/مسودات واجهات صريحة مثل `Create_Backup.cshtml` و`Create_Enhanced_Backup.cshtml` و`Create_Old.cshtml` وملفات `index_test*.html` وملفات `Create1..Create6.cshtml`. لا تُحذف بناءً على هذا التقرير، لكنها تحتاج قرار مالك واضح: اعتماد نسخة واحدة، أو نقل البقية إلى أرشيف موثق.
- وجود `DynamicForms.db` بحروف مختلفة أحدهما فارغ يستحق الحسم؛ على Windows لا يسبب اختلاف الحرفين مسارين مستقلين، لكن الفرق في حالة الاسم يشير إلى التباس في تسمية الأصل.

## 9. ما يجب إدارته مستقبلًا بواسطة Git LFS أو مستودع Assets منفصل

| الفئة | التوصية |
|---|---|
| أوزان النماذج (`.pth`, `.pt`, `.onnx`, `.ckpt`) | مستودع Assets/تخزين كائنات خاص؛ Git LFS ممكن إن كان الحجم والحصص مقبولين. احفظ SHA-256 ونسخة النموذج وملف manifest. |
| الأرشيفات واللقطات التاريخية (`.rar`) | مستودع Assets منفصل أو تخزين أرشيفي؛ لا تُكررها داخل Git أو LFS دون حاجة فعلية. |
| قواعد البيانات والنسخ الاحتياطية (`.mdf`, `.ldf`, `.bak`) | تخزين نسخ احتياطية مشفر ومضبوط الوصول، مع سكربتات migrations/seed في Git. لا تستخدم Git LFS لقاعدة تشغيلية متغيرة. |
| ملفات المستخدمين (Uploads/PDFs/صور) | تخزين كائنات أو مستودع Media منفصل مع فهرس metadata؛ لا تُضمَّن بيانات حقيقية في مصدر التطبيق. |
| أصول الواجهة الصغيرة الضرورية للبناء | تبقى في Git عند وجود ترخيص مناسب؛ أما مكتبات الواجهة فالأفضل ضبطها عبر مدير حزم/manifest بدلاً من النسخ اليدوي حيثما أمكن. |
| `bin/obj/.vs/__pycache__` | لا Git LFS ولا مستودع Assets؛ تُحذف من أي حزمة أرشيفية مستقبلية بعد بناء نسخة قابلة لإعادة الإنتاج. |

## 10. هيكل الإدارة المقترح

```text
mahwar-platform/                 # Git: الكود والوثائق وملفات البناء القابلة لإعادة الإنتاج
├─ apps/
│  ├─ tanjez-mvc/                # Mntry_Awqaf
│  ├─ tanjez-api/                # TestProject/Test
│  ├─ data-tools/                # CheckData وInsertArabicData وسكربتات SQL
│  └─ land-classification/       # مشروع Python
├─ prototypes/                   # hhh والنماذج التجريبية
├─ docs/
├─ infra/                        # إعدادات آمنة وقوالب اتصال ومخططات نشر
└─ assets.manifest.json          # الاسم، الإصدار، URL، SHA-256، المستهلك، طريقة الاسترجاع

mahwar-assets/                   # وصول مقيد/تخزين كائنات أو مستودع منفصل
├─ models/sam/sam_vit_b_01ec64.pth
├─ archives/alnahari22-05-2026.rar
├─ database-backups/
└─ media/
```

سير العمل المقترح:

1. يبقى Git مصدر الحقيقة للكود وملفات الإعداد الآمنة النموذجية فقط (`appsettings.Example.json`)، مع منع الأسرار والبيانات التشغيلية.
2. لكل أصل خارجي، يُسجل المسار المنطقي والإصدار والبصمة والحجم والمستهلك في `assets.manifest.json` أو في وثيقة Assets منظمة.
3. تُنشأ عملية استعادة واحدة قابلة للتنفيذ تتحقق من SHA-256 وتنزّل/تنسخ الأصول المطلوبة بحسب المشروع، بدلاً من النسخ اليدوي بين المجلدات.
4. تُفصل اللقطات التاريخية عن مساحة التطوير؛ تُحتفَظ نسخة أصلية واحدة غير قابلة للتعديل، ولا تُحتفَظ معها مخرجات البناء إلا إذا كانت مطلوبة قانونيًا أو لتدقيق محدد.
5. تُعتمد migrations ونسخ بيانات seed منقحة داخل Git، وتُدار قواعد البيانات والنسخ الاحتياطية في نظام نسخ احتياطي آمن ومجدول.

## 11. قرار الحالة المرجعية

**المرجع التطويري الحالي هو `D:\EngMohammed`/GitHub، وليس أي لقطة داخل مجلدات Assets.**  
تُعامل `D:\EngMohammed_Assets_Extracted` و`D:\EngMohammed_Assets\alnahari22-05-2026` كمصادر استعادة ومقارنة تاريخية، ويُعامل `sam_vit_b_01ec64.pth` كاعتماد تشغيل خارجي موثق لمشروع تصنيف الأراضي.
