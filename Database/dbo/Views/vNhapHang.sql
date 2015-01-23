CREATE VIEW dbo.[vNhapHang]
AS
SELECT        dbo.tNhapHang.Ngay, dbo.rNhanVien.Ma AS MaNV, dbo.rNhanVien.TenNhanVien, dbo.tMatHang.Ma AS MaMatHang, dbo.tMatHang.TenMatHang, 
                         dbo.tChiTietNhapHang.SoLuong
FROM            dbo.tNhapHang INNER JOIN
                         dbo.tChiTietNhapHang ON dbo.tNhapHang.Ma = dbo.tChiTietNhapHang.MaNhapHang INNER JOIN
                         dbo.tMatHang ON dbo.tChiTietNhapHang.MaMatHang = dbo.tMatHang.Ma INNER JOIN
                         dbo.rNhanVien ON dbo.tNhapHang.MaNhanVien = dbo.rNhanVien.Ma
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vNhapHang';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vNhapHang';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tNhapHang"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "tChiTietNhapHang"
            Begin Extent = 
               Top = 10
               Left = 391
               Bottom = 139
               Right = 561
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tMatHang"
            Begin Extent = 
               Top = 6
               Left = 599
               Bottom = 135
               Right = 793
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rNhanVien"
            Begin Extent = 
               Top = 174
               Left = 435
               Bottom = 286
               Right = 606
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vNhapHang';

