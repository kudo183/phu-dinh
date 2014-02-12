-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ReportTotal
	-- Add the parameters for the stored procedure here
	@dateFrom datetime,
	@dateTo datetime
AS
BEGIN
	select sum(SoLuong) SoLuong
from dbo.tDonHang dh join dbo.tChiTietDonHang ct on dh.Ma = ct.MaDonHang
where dh.Ngay between @dateFrom and @dateTo
END