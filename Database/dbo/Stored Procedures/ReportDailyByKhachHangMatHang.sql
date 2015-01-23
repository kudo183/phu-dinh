-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ReportDailyByKhachHangMatHang] 
	-- Add the parameters for the stored procedure here
	@date datetime
AS
BEGIN
	select kh.TenKhachHang, mh.TenMatHang, sum(ct.SoLuong) SoLuong
	from dbo.tDonHang dh
	join dbo.tChiTietDonHang ct on dh.Ma = ct.MaDonHang
	join dbo.rKhachHang kh on dh.MaKhachHang = kh.Ma
	join dbo.tMatHang mh on ct.MaMatHang = mh.Ma
	where dh.Ngay = @date
	group by kh.TenKhachHang, mh.TenMatHang
	order by kh.TenKhachHang
END