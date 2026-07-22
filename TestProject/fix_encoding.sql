-- إصلاح ترميز قاعدة البيانات للعربية
USE Awqaf33;
GO

-- التحقق من الترميز الحالي
SELECT name, collation_name 
FROM sys.databases 
WHERE name = 'Awqaf33';
GO

-- تحديث الترميز إلى Arabic_CI_AS
ALTER DATABASE Awqaf33 COLLATE Arabic_CI_AS;
GO

-- حذف البيانات القديمة وإعادة الإدخال بالترميز الصحيح
DELETE FROM [Notifications];
DELETE FROM [Projects];
DELETE FROM [Certificates];
DELETE FROM [AdditionalCostType];
DELETE FROM [WorkOffer];
DELETE FROM [sectionTypes];
DELETE FROM [sections];
DELETE FROM [Classification];
DELETE FROM [Almjals];
DELETE FROM [Users];
DELETE FROM [Lands];
GO

-- إعادة تعيين IDENTITY
DBCC CHECKIDENT ('[Users]', RESEED, 0);
DBCC CHECKIDENT ('[Almjals]', RESEED, 0);
DBCC CHECKIDENT ('[Classification]', RESEED, 0);
DBCC CHECKIDENT ('[sections]', RESEED, 0);
DBCC CHECKIDENT ('[sectionTypes]', RESEED, 0);
DBCC CHECKIDENT ('[WorkOffer]', RESEED, 0);
DBCC CHECKIDENT ('[AdditionalCostType]', RESEED, 0);
DBCC CHECKIDENT ('[Certificates]', RESEED, 0);
DBCC CHECKIDENT ('[Projects]', RESEED, 0);
DBCC CHECKIDENT ('[Notifications]', RESEED, 0);
DBCC CHECKIDENT ('[Lands]', RESEED, 0);
GO

-- إدخال المستخدمين
INSERT INTO [Users] ([Frist_Name], [Second_Name], [Third_Name], [Fourth_Name], [Nickname], [User_type], [Phone_number1], [Phone_number2], [Email], [Pass], [Sex], [Nationality], [MaritalStatus], [BirthDate], [RelativePhone])
VALUES 
(N'أحمد', N'محمد', N'علي', N'الفهد', N'أحمد', N'عميل', '0501234567', '0559876543', 'ahmed@test.com', 'password123', N'ذكر', N'سعودي', N'متزوج', '1990-05-15', '0501112222'),
(N'فاطمة', N'علي', N'حسن', N'السعود', N'فاطمة', N'مقاول', '0502345678', NULL, 'fatima@test.com', 'password123', N'أنثى', N'سعودية', N'عزباء', '1995-08-20', '0503334444'),
(N'خالد', N'عبدالله', N'سلطان', N'الراشد', N'خالد', N'عميل', '0503456789', '0561234567', 'khaled@test.com', 'password123', N'ذكر', N'سعودي', N'متزوج', '1985-12-10', '0505556666'),
(N'نورة', N'سالم', N'فهد', N'العتيبي', N'نورة', N'مقاول', '0504567890', NULL, 'noura@test.com', 'password123', N'أنثى', N'سعودية', N'متزوجة', '1992-03-25', '0507778888');
GO

-- إدخال المجالات
INSERT INTO [Almjals] ([AlmjalName]) VALUES 
(N'البناء'),
(N'الكهرباء'),
(N'السباكة'),
(N'التكييف'),
(N'الدهان');
GO

-- إدخال التصنيفات
INSERT INTO [Classification] ([ClassificationName], [AlmjalId]) VALUES 
(N'بناء سكني', 1),
(N'بناء تجاري', 1),
(N'تركيبات كهربائية', 2),
(N'صيانة كهربائية', 2),
(N'تركيب سباكة', 3),
(N'صيانة سباكة', 3),
(N'تكييف مركزي', 4),
(N'تكييف سبليت', 4),
(N'دهان داخلي', 5),
(N'دهان خارجي', 5);
GO

-- إدخال عروض العمل
INSERT INTO [WorkOffer] ([OfferName], [EndDate], [WagesTemplate], [TransportTemplate], [WorkWagesType], [IsAllTypesIncluded], [LatePenaltyPercentage], [AdvancePaymentPercentage], [ResignationAfterAdvancePercentage], [OnSitePaymentPercentage], [Notes], [UserID])
VALUES 
(N'مشروع بناء فيلا سكنية', '2025-12-31', N'قالب أجور بناء', N'قالب نقل عادي', N'يومي', 1, 5.00, 20.00, 10.00, 30.00, N'مشروع بناء فيلا دورين في حي النرجس', 1),
(N'صيانة كهربائية لمجمع', '2025-10-15', N'قالب أجور كهرباء', N'قالب نقل داخلي', N'بالمتر', 0, 3.00, 15.00, 5.00, 25.00, N'صيانة دورية للمجمع التجاري', 2),
(N'تركيب سباكة لعمارة', '2025-11-30', N'قالب أجور سباكة', N'قالب نقل خارجي', N'بالقطعة', 0, 4.00, 25.00, 8.00, 35.00, N'تركيب شبكات المياه والصرف', 3),
(N'تكييف مركزي لفيلا', '2025-09-20', N'قالب أجور تكييف', N'قالب نقل داخلي', N'بالوحدة', 1, 6.00, 30.00, 12.00, 40.00, N'تركيب وحدات تكييف مركزي', 4);
GO

-- إدخال أنواع التكاليف
INSERT INTO [AdditionalCostType] ([AdditionalCostTypeName], [UserID]) VALUES 
(N'تكلفة مواد', 1),
(N'تكلفة نقل', 2),
(N'تكلفة إقامة', 3),
(N'تكلفة معدات', 4);
GO

-- إدخال الشهادات
INSERT INTO [Certificates] ([CertName], [Almgal], [CertType], [CertYear], [UserID]) VALUES 
(N'شهادة PMP', N'إدارة المشاريع', N'دولية', '2020', 1),
(N'شهادة سكرتارية', N'إدارة مكتبية', N'محلية', '2019', 2),
(N'شهادة أمن سلامة', N'السلامة المهنية', N'دولية', '2021', 3),
(N'شهادة مهندس معماري', N'هندسة معمارية', N'جامعية', '2018', 4);
GO

-- إدخال المشاريع
INSERT INTO [Projects] ([ProjName], [ProjCompany], [ProjField], [ProjFrom], [ProjTo], [ProjValue], [ProjRole], [UserID]) VALUES 
(N'فيلا النرجس', N'مؤسسة البناء السريع', N'بناء سكني', '2025-01-01', '2025-12-31', N'2,500,000 ريال', N'مدير مشروع', 1),
(N'صيانة مجمع الرياض', N'شركة الصيانة المتكاملة', N'صيانة كهربائية', '2025-03-01', '2025-10-15', N'500,000 ريال', N'فني كهرباء', 2),
(N'عمارة الياسمين', N'مؤسسة السباكة الحديثة', N'سباكة', '2025-04-01', '2025-11-30', N'800,000 ريال', N'سباك', 3),
(N'فيلا الفيصلية', N'شركة التكييف العالمية', N'تكييف مركزي', '2025-05-01', '2025-09-20', N'1,200,000 ريال', N'فني تكييف', 4);
GO

-- إدخال الإشعارات
INSERT INTO [Notifications] ([UserId], [ReciveId], [UserType], [ContractId], [Message], [Url], [IsRead], [CreatedAt]) VALUES 
(1, 1, N'عميل', NULL, N'مرحباً بك في النظام', N'/home', 0, GETDATE()),
(2, 2, N'مقاول', 1, N'لديك عرض عمل جديد', N'/offers/1', 0, GETDATE()),
(3, 3, N'عميل', NULL, N'موعد اجتماع غداً', N'/meetings', 0, GETDATE()),
(4, 4, N'مقاول', NULL, N'تم تحديث بياناتك بنجاح', N'/profile', 1, GETDATE());
GO

-- إدخال الأراضي
INSERT INTO [Lands] ([ParcelNumber], [OwnerName], [Area], [Shape], [CreatedAt], [CreatedBy], [UpdatedBy]) VALUES 
('12345', N'أحمد محمد الفهد', 500.5, N'مستطيل', GETDATE(), 'admin', 'admin'),
('23456', N'فاطمة علي السعود', 750.0, N'مربع', GETDATE(), 'admin', 'admin'),
('34567', N'خالد عبدالله الراشد', 1200.0, N'حرف L', GETDATE(), 'admin', 'admin'),
('45678', N'نورة سالم العتيبي', 350.25, N'مستطيل', GETDATE(), 'admin', 'admin');
GO

SELECT N'تم إدخال البيانات بالترميز الصحيح!' AS Result;
