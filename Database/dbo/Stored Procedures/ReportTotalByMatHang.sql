-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ReportTotalByMatHang 
	-- Add the parameters for the stored procedure here
	@dateFrom datetime,
	@dateTo datetime
AS
BEGIN
	select mh.TenMatHang, isnull(sum(ct.SoLuong), 0) SoLuong
	from dbo.tMatHang mh
	left join ( select ct.MaMatHang, ct.SoLuong
	from dbo.tChiTietDonHang ct
	join dbo.tDonHang dh on dh.Ma = ct.MaDonHang
	where dh.Ngay between @dateFrom and @dateTo) as ct on mh.Ma = ct.MaMatHang
	group by mh.TenMatHang
END