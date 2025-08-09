SET IDENTITY_INSERT [dbo].[students] ON
INSERT INTO [dbo].[students] ([student_id], [FirstName], [LastName], [Address1], [Address2], [City], [StateProvince], [ZipPostalCode], [Country], [Description], [Email], [Phone], [Fax], [BankAccountNumber], [IsActive]) VALUES (1, N'Mohit1', N'Kumar', N'main streat', N'mahipalpur', N'new delhi', N'Delhi', NULL, N'India', NULL, N'test@test.com', N'`1234567', NULL, N'1234567', 0)
SET IDENTITY_INSERT [dbo].[students] OFF
