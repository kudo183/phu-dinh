-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[clean_database]
as
BEGIN
	delete from dbo.tChiTietChuyenHangDonHang
	delete from dbo.tChuyenHangDonHang
	delete from dbo.tChiTietDonHang
	delete from dbo.tDonHang
	delete from dbo.tChuyenHang
	delete from dbo.tChiPhiNhanVienGiaoHang
	delete from dbo.rNhanVienGiaoHang
	delete from dbo.tMatHang
	delete from dbo.rKhachHang
	delete from dbo.rPhuongTien
	delete from dbo.rLoaiHang
	delete from dbo.rLoaiChiPhi
	delete from dbo.rDiaDiem
	delete from dbo.rChanh
	delete from dbo.rBaiXe
END