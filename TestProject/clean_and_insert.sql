USE Awqaf33;
GO

-- تعطيل القيود مؤقتاً
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";
GO

-- حذف البيانات
DELETE FROM Notifications;
DELETE FROM Projects;
DELETE FROM Certificates;
DELETE FROM WorkOffer;
DELETE FROM AdditionalCostType;
DELETE FROM sectionTypes;
DELETE FROM sections;
DELETE FROM Classification;
DELETE FROM Almjals;
DELETE FROM Users;
DELETE FROM Lands;
GO

-- إعادة تعيين IDENTITY
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Almjals', RESEED, 0);
DBCC CHECKIDENT ('Classification', RESEED, 0);
DBCC CHECKIDENT ('sections', RESEED, 0);
DBCC CHECKIDENT ('sectionTypes', RESEED, 0);
DBCC CHECKIDENT ('WorkOffer', RESEED, 0);
DBCC CHECKIDENT ('AdditionalCostType', RESEED, 0);
DBCC CHECKIDENT ('Certificates', RESEED, 0);
DBCC CHECKIDENT ('Projects', RESEED, 0);
DBCC CHECKIDENT ('Notifications', RESEED, 0);
DBCC CHECKIDENT ('Lands', RESEED, 0);
GO

-- إعادة تفعيل القيود
EXEC sp_MSforeachtable "ALTER TABLE ? CHECK CONSTRAINT all";
GO

PRINT 'تم حذف البيانات القديمة';
