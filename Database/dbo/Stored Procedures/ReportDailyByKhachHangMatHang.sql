-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[ReportDailyByKhachHangMatHang] 
	-- Add the parameters for the stored procedure here
	@date datetime
AS
BEGIN
	select kh.TenKhachHang, mh.TenMatHang, ct.SoLuong
	from dbo.tDonHang dh
	join dbo.tChiTietDonHang ct on dh.Ma = ct.MaDonHang
	join dbo.rKhachHang kh on dh.MaKhachHang = kh.Ma
	join dbo.tMatHang mh on ct.MaMatHang = mh.Ma
	where dh.Ngay = @date
	group by kh.TenKhachHang, mh.TenMatHang, ct.SoLuong
END