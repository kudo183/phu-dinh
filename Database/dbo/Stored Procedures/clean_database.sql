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
	
	dbcc checkident (tChiTietChuyenHangDonHang, reseed, 0)
	dbcc checkident (tChuyenHangDonHang, reseed, 0)
	dbcc checkident (tChiTietDonHang, reseed, 0)
	dbcc checkident (tDonHang, reseed, 0)
	dbcc checkident (tChuyenHang, reseed, 0)
	dbcc checkident (tChiPhiNhanVienGiaoHang, reseed, 0)
	dbcc checkident (rNhanVienGiaoHang, reseed, 0)
	dbcc checkident (tMatHang, reseed, 0)
	dbcc checkident (rKhachHang, reseed, 0)
	dbcc checkident (rPhuongTien, reseed, 0)
	dbcc checkident (rLoaiHang, reseed, 0)
	dbcc checkident (rLoaiChiPhi, reseed, 0)
	dbcc checkident (rDiaDiem, reseed, 0)
	dbcc checkident (rChanh, reseed, 0)
	dbcc checkident (rBaiXe, reseed, 0)
END