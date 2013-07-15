/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/****** Object:  Table [dbo].[rNuoc]    Script Date: 07/15/2013 14:07:24 ******/
SET IDENTITY_INSERT [dbo].[rNuoc] ON
INSERT [dbo].[rNuoc] ([Ma], [TenNuoc]) VALUES (1, N'Việt Nam')
SET IDENTITY_INSERT [dbo].[rNuoc] OFF
/****** Object:  Table [dbo].[rDiaDiem]    Script Date: 07/15/2013 14:07:24 ******/
SET IDENTITY_INSERT [dbo].[rDiaDiem] ON
INSERT [dbo].[rDiaDiem] ([Ma], [MaNuoc], [Tinh]) VALUES (1, 1, N'Chưa biết')
INSERT [dbo].[rDiaDiem] ([Ma], [MaNuoc], [Tinh]) VALUES (2, 1, N'Bảo Lộc')
INSERT [dbo].[rDiaDiem] ([Ma], [MaNuoc], [Tinh]) VALUES (3, 1, N'Đắc Ru')
INSERT [dbo].[rDiaDiem] ([Ma], [MaNuoc], [Tinh]) VALUES (4, 1, N'Nghệ An')
INSERT [dbo].[rDiaDiem] ([Ma], [MaNuoc], [Tinh]) VALUES (5, 1, N'Trà Vinh')
SET IDENTITY_INSERT [dbo].[rDiaDiem] OFF
/****** Object:  Table [dbo].[rKhachHang]    Script Date: 07/15/2013 14:07:24 ******/
SET IDENTITY_INSERT [dbo].[rKhachHang] ON
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (1, 1, N'Tân Tân')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (2, 1, N'Hà Bù Nho')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (3, 1, N'Kho Đồng Tâm')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (4, 1, N'Chợ')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (5, 1, N'Bình Phú')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (6, 1, N'Hà Tuân')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (7, 1, N'Thanh Lam')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (8, 1, N'Đồng Lợi')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (13, 1, N'Gò Công')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (9, 2, N'Khôi Bảo Lộc')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (12, 3, N'Hoàng Thanh Đắc Ru')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (10, 4, N'Cường Nghệ An')
INSERT [dbo].[rKhachHang] ([Ma], [MaDiaDiem], [TenKhachHang]) VALUES (11, 5, N'Tuấn Trà Vinh')
SET IDENTITY_INSERT [dbo].[rKhachHang] OFF

/****** Object:  Table [dbo].[rLoaiHang]    Script Date: 07/15/2013 14:16:52 ******/
SET IDENTITY_INSERT [dbo].[rLoaiHang] ON
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (1, N'B')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (2, N'Cáo mũ')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (3, N'Cáo kẽm')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (4, N'Chì')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (5, N'Fectina')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (6, N'Kẽm lam')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (7, N'Lưới vuông')
INSERT [dbo].[rLoaiHang] ([Ma], [TenLoai]) VALUES (8, N'Lưới sấy')
SET IDENTITY_INSERT [dbo].[rLoaiHang] OFF
/****** Object:  Table [dbo].[tMatHang]    Script Date: 07/15/2013 14:16:52 ******/
SET IDENTITY_INSERT [dbo].[tMatHang] ON
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (1, 1, N'B1F 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (2, 1, N'B1F5 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (3, 1, N'B2 lớn 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (4, 1, N'B2 1m 29kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (5, 1, N'B2 1m 39kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (6, 1, N'B2 lớn 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (7, 1, N'B2 lớn 1m5')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (8, 1, N'B3 trung 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (9, 1, N'B3 trung 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (10, 1, N'B3 trung 1m5')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (11, 1, N'B3 nhỏ 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (12, 1, N'B3 nhỏ 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (13, 1, N'B3 lớn 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (14, 1, N'B3 lớn 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (15, 1, N'B3 lớn 1m5')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (16, 2, N'Cáo mũ 5t 4kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (17, 2, N'Cáo mũ 6t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (18, 2, N'Cáo mũ 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (19, 2, N'Cáo mũ 1F2 5t 6kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (20, 2, N'Cáo mũ 1F2 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (21, 3, N'Cáo kẽm 5t mỏng')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (22, 3, N'Cáo kẽm 5t dầy')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (23, 3, N'Cáo kẽm 6tấc 7kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (24, 3, N'Cáo kẽm 1F2 7kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (25, 3, N'Cáo kẽm 1F2 8kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (26, 3, N'Cáo kẽm 1F2 9kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (27, 3, N'Cáo kẽm 1F2 10kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (28, 3, N'Cáo kẽm 1F2 12kg5')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (29, 3, N'Cáo kẽm 1F2 18kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (30, 3, N'Cáo kẽm 1F2 22kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (31, 3, N'Cáo kẽm 1F5 9kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (32, 4, N'Chì 5ly dầy')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (33, 4, N'Chì 8ly dầy')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (34, 4, N'Chì 1F2 20m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (35, 4, N'Chì 1F5 20m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (36, 4, N'Chì 2F 20m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (37, 4, N'Chì 2F 2ly 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (38, 4, N'Chì 2F 2ly 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (39, 4, N'Chì 3F6 2ly 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (40, 4, N'Chì 3F6 2ly 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (41, 4, N'Chì 3F6 2ly5 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (42, 4, N'Chì 3F6 2ly5 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (43, 4, N'Chì 3F6 2ly5 1m5')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (44, 4, N'Chì 5F 2ly7 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (45, 4, N'Chì 5F 2ly7 1m2')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (46, 4, N'Chì 5F 2ly7 1m5')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (47, 5, N'Fectina 2t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (48, 5, N'Fectina 3t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (49, 5, N'Fectina 4t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (50, 6, N'Kẽm lam màu')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (51, 6, N'Kẽm lam trắng')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (52, 7, N'Vuông trắng nhỏ 8t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (53, 7, N'Vuông trắng trung 8t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (54, 7, N'Vuông trắng cối 8t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (55, 7, N'Vuông đen nhỏ 8t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (56, 7, N'Vuông đen trung 8t')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (57, 7, N'Vuông trắng nhỏ 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (58, 7, N'Vuông trắng trung 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (59, 7, N'Vuông trắng cối 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (60, 7, N'Vuông đen nhỏ 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (61, 7, N'Vuông đen trung 1m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (62, 7, N'Vuông 8ly cối')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (63, 7, N'Vuông 1F cối')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (64, 7, N'Vuông 1F2 cối')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (65, 7, N'Vuông 1F2 21kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (66, 7, N'Vuông 1F2 25kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (67, 7, N'Vuông 1F2 28kg')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (68, 7, N'Vuông 1F5 ĐB')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (69, 7, N'Vuông 1F5 cối')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (70, 7, N'Vuông 2F trung')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (71, 7, N'Vuông 2F ĐB')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (72, 7, N'Vuông 2F cối')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (73, 7, N'Vuông 3F cối')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (74, 8, N'Sấy nhỏ 20m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (75, 8, N'Sấy nhỏ 24m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (76, 8, N'Sấy lớn 20m')
INSERT [dbo].[tMatHang] ([Ma], [MaLoai], [TenMatHang]) VALUES (77, 8, N'Sấy lớn 24m')
SET IDENTITY_INSERT [dbo].[tMatHang] OFF