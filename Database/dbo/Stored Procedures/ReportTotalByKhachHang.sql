-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[ReportTotalByKhachHang] 
	-- Add the parameters for the stored procedure here
	@dateFrom datetime,
	@dateTo datetime
AS
BEGIN
	select kh.TenKhachHang, isnull(sum(ct.SoLuong), 0) SoLuong
	from dbo.rKhachHang kh
	left join ( select dh.MaKhachHang, ct.SoLuong
	from dbo.tChiTietDonHang ct
	join dbo.tDonHang dh on dh.ma = ct.MaDonHang
	where dh.Ngay between @dateFrom and @dateTo) as ct on kh.Ma = ct.MaKhachHang
	group by kh.TenKhachHang
END