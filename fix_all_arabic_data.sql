USE Awqaf33;
GO

-- ============================================================
-- إصلاح شامل لجميع البيانات العربية في قاعدة البيانات Awqaf33
-- ============================================================

-- 1. إصلاح جدول Almjals (المجالات)
UPDATE Almjals SET AlmjalName = N'البناء'        WHERE Id = 1;
UPDATE Almjals SET AlmjalName = N'الكهرباء'      WHERE Id = 2;
UPDATE Almjals SET AlmjalName = N'السباكة'       WHERE Id = 3;
UPDATE Almjals SET AlmjalName = N'التكييف'       WHERE Id = 4;
UPDATE Almjals SET AlmjalName = N'الدهان'        WHERE Id = 5;
GO

-- 2. إصلاح جدول Classification (التصنيفات)
UPDATE Classification SET ClassificationName = N'بناء سكني'          WHERE Id = 1;
UPDATE Classification SET ClassificationName = N'بناء تجاري'         WHERE Id = 2;
UPDATE Classification SET ClassificationName = N'تركيبات كهربائية'   WHERE Id = 3;
UPDATE Classification SET ClassificationName = N'صيانة كهربائية'     WHERE Id = 4;
UPDATE Classification SET ClassificationName = N'تركيب سباكة'        WHERE Id = 5;
UPDATE Classification SET ClassificationName = N'صيانة سباكة'        WHERE Id = 6;
UPDATE Classification SET ClassificationName = N'تكييف مركزي'        WHERE Id = 7;
UPDATE Classification SET ClassificationName = N'تكييف سبليت'        WHERE Id = 8;
UPDATE Classification SET ClassificationName = N'دهان داخلي'         WHERE Id = 9;
UPDATE Classification SET ClassificationName = N'دهان خارجي'         WHERE Id = 10;
GO

-- 3. إصلاح جدول WorkOffer (عروض العمل)
UPDATE WorkOffer SET 
    OfferName = N'مشروع بناء فيلا سكنية',
    Notes     = N'مشروع بناء فيلا دورين في حي النرجس'
WHERE Id = 1;

UPDATE WorkOffer SET 
    OfferName = N'صيانة كهربائية لمجمع',
    Notes     = N'صيانة دورية للمجمع التجاري'
WHERE Id = 2;

UPDATE WorkOffer SET 
    OfferName = N'تركيب سباكة لعمارة',
    Notes     = N'تركيب شبكات المياه والصرف'
WHERE Id = 3;

UPDATE WorkOffer SET 
    OfferName = N'تكييف مركزي لفيلا',
    Notes     = N'تركيب وحدات تكييف مركزي'
WHERE Id = 4;
GO

-- 4. إصلاح جدول Notifications (الإشعارات)
UPDATE Notifications SET 
    Message  = N'مرحباً بك في النظام',
    UserType = N'عميل'
WHERE Id = 1;

UPDATE Notifications SET 
    Message  = N'لديك عرض عمل جديد',
    UserType = N'مقاول'
WHERE Id = 2;

UPDATE Notifications SET 
    Message  = N'موعد اجتماع غداً',
    UserType = N'عميل'
WHERE Id = 3;

UPDATE Notifications SET 
    Message  = N'تم تحديث بياناتك بنجاح',
    UserType = N'مقاول'
WHERE Id = 4;
GO

-- 5. إصلاح جدول Projects (المشاريع)
UPDATE Projects SET 
    ProjName    = N'فيلا النرجس',
    ProjField   = N'إنشاءات',
    ProjRole    = N'مقاول رئيسي',
    ProjCompany = N'شركة الإنشاءات السعودية',
    ProjFrom    = N'2022',
    ProjTo      = N'2023',
    ProjValue   = N'500000'
WHERE Id = 1;

UPDATE Projects SET 
    ProjName    = N'صيانة مجمع الرياض',
    ProjField   = N'كهرباء',
    ProjRole    = N'مهندس كهرباء',
    ProjCompany = N'شركة الطاقة',
    ProjFrom    = N'2023',
    ProjTo      = N'2023',
    ProjValue   = N'120000'
WHERE Id = 2;

UPDATE Projects SET 
    ProjName    = N'عمارة الياسمين',
    ProjField   = N'سباكة',
    ProjRole    = N'فني سباكة',
    ProjCompany = N'مؤسسة المياه',
    ProjFrom    = N'2021',
    ProjTo      = N'2022',
    ProjValue   = N'80000'
WHERE Id = 3;

UPDATE Projects SET 
    ProjName    = N'فيلا الفيصلية',
    ProjField   = N'تكييف',
    ProjRole    = N'فني تكييف',
    ProjCompany = N'شركة التكييف الحديث',
    ProjFrom    = N'2023',
    ProjTo      = N'2024',
    ProjValue   = N'95000'
WHERE Id = 4;
GO

-- 6. إصلاح جدول Certificates (الشهادات)
UPDATE Certificates SET 
    CertName = N'شهادة PMP',
    CertOrg  = N'معهد إدارة المشاريع',
    CertType = N'مهنية',
    CertYear = N'2020',
    Almgal   = N'إدارة',
    Depart   = N'مشاريع',
    Type     = N'دولية'
WHERE Id = 1;

UPDATE Certificates SET 
    CertName = N'شهادة سكرتارية',
    CertOrg  = N'كلية الإدارة',
    CertType = N'أكاديمية',
    CertYear = N'2018',
    Almgal   = N'إدارة',
    Depart   = N'سكرتارية',
    Type     = N'محلية'
WHERE Id = 2;

UPDATE Certificates SET 
    CertName = N'شهادة أمن سلامة',
    CertOrg  = N'هيئة السلامة',
    CertType = N'مهنية',
    CertYear = N'2021',
    Almgal   = N'سلامة',
    Depart   = N'وقاية',
    Type     = N'محلية'
WHERE Id = 3;

UPDATE Certificates SET 
    CertName = N'شهادة مهندس معماري',
    CertOrg  = N'جامعة الملك سعود',
    CertType = N'أكاديمية',
    CertYear = N'2015',
    Almgal   = N'هندسة',
    Depart   = N'معمارية',
    Type     = N'دولية'
WHERE Id = 4;
GO

-- 7. إصلاح جدول Users (المستخدمين) - الحقول الأخرى
UPDATE Users SET 
    Second_Name   = N'محمد',
    Third_Name    = N'علي',
    Fourth_Name   = N'الفهد',
    Nickname      = N'أحمد',
    Sex           = N'ذكر',
    Nationality   = N'سعودي',
    MaritalStatus = N'متزوج'
WHERE ID = 1;

UPDATE Users SET 
    Second_Name   = N'علي',
    Third_Name    = N'حسن',
    Fourth_Name   = N'السعود',
    Nickname      = N'فاطمة',
    Sex           = N'أنثى',
    Nationality   = N'سعودية',
    MaritalStatus = N'عزباء'
WHERE ID = 2;

UPDATE Users SET 
    Second_Name   = N'عبدالله',
    Third_Name    = N'سلطان',
    Fourth_Name   = N'الراشد',
    Nickname      = N'خالد',
    Sex           = N'ذكر',
    Nationality   = N'سعودي',
    MaritalStatus = N'متزوج'
WHERE ID = 3;

UPDATE Users SET 
    Second_Name   = N'سالم',
    Third_Name    = N'فهد',
    Fourth_Name   = N'العتيبي',
    Nickname      = N'نورة',
    Sex           = N'أنثى',
    Nationality   = N'سعودية',
    MaritalStatus = N'متزوجة'
WHERE ID = 4;
GO

-- ============================================================
-- إضافة بيانات للجداول الفارغة المهمة (lookup tables)
-- ============================================================

-- 8. AccessRoadType (أنواع طرق الوصول)
IF NOT EXISTS (SELECT 1 FROM AccessRoadType)
BEGIN
    INSERT INTO AccessRoadType (AccessRoad) VALUES
    (N'طريق معبّد'),
    (N'طريق ترابي'),
    (N'طريق إسفلتي'),
    (N'ممر خاص'),
    (N'طريق دولي');
END
GO

-- 9. DocumentType (أنواع الوثائق)
IF NOT EXISTS (SELECT 1 FROM DocumentType)
BEGIN
    INSERT INTO DocumentType (DocumentTypeName) VALUES
    (N'هوية وطنية'),
    (N'جواز سفر'),
    (N'رخصة قيادة'),
    (N'سجل تجاري'),
    (N'وثيقة ملكية'),
    (N'عقد إيجار'),
    (N'شهادة ميلاد');
END
GO

-- 10. PlaceType (أنواع الأماكن)
IF NOT EXISTS (SELECT 1 FROM PlaceType)
BEGIN
    INSERT INTO PlaceType (PlaceTypeName) VALUES
    (N'فيلا'),
    (N'شقة'),
    (N'مكتب'),
    (N'مجمع تجاري'),
    (N'مستودع'),
    (N'أرض'),
    (N'محل تجاري');
END
GO

-- 11. purposeSurveys (أغراض المسح)
IF NOT EXISTS (SELECT 1 FROM purposeSurveys)
BEGIN
    INSERT INTO purposeSurveys (Name) VALUES
    (N'مسح حدودي'),
    (N'مسح طبوغرافي'),
    (N'مسح وصفي'),
    (N'مسح مساحي'),
    (N'مسح جيوتقني');
END
GO

-- 12. sectionTypes (أنواع الأقسام)
IF NOT EXISTS (SELECT 1 FROM sectionTypes)
BEGIN
    INSERT INTO sectionTypes (SectionTypeName) VALUES
    (N'قسم هندسي'),
    (N'قسم إداري'),
    (N'قسم مالي'),
    (N'قسم تقني'),
    (N'قسم قانوني');
END
GO

-- 13. Workpaymentperiods (فترات دفع العمل)
IF NOT EXISTS (SELECT 1 FROM Workpaymentperiods)
BEGIN
    INSERT INTO Workpaymentperiods (Name) VALUES
    (N'يومي'),
    (N'أسبوعي'),
    (N'نصف شهري'),
    (N'شهري'),
    (N'عند الإنجاز');
END
GO

-- 14. ownerships (أنواع الملكية)
IF NOT EXISTS (SELECT 1 FROM ownerships)
BEGIN
    INSERT INTO ownerships (Name) VALUES
    (N'ملكية خاصة'),
    (N'ملكية حكومية'),
    (N'ملكية مشتركة'),
    (N'إيجار'),
    (N'وقف');
END
GO

-- 15. Characteristicpeople (صفات الأشخاص)
IF NOT EXISTS (SELECT 1 FROM Characteristicpeople)
BEGIN
    INSERT INTO Characteristicpeople (Name) VALUES
    (N'منضبط'),
    (N'خبير'),
    (N'موثوق'),
    (N'سريع'),
    (N'محترف');
END
GO

PRINT N'تم إصلاح جميع البيانات العربية بنجاح!';
GO
