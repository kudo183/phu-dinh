-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[ReportTotalByLoaiHang] 
	-- Add the parameters for the stored procedure here
	@dateFrom datetime,
	@dateTo datetime
AS
BEGIN
	select lh.TenLoai, isnull(sum(ct.SoLuong), 0) SoLuong
	from dbo.rLoaiHang lh
	left join ( select mh.MaLoai, ct.SoLuong
	from dbo.tChiTietDonHang ct
	join dbo.tDonHang dh on dh.Ma = ct.MaDonHang
	join dbo.tMatHang mh on mh.Ma = ct.MaMatHang
	where dh.Ngay between @dateFrom and @dateTo) as ct on lh.Ma = ct.MaLoai
	group by lh.TenLoai
END