CREATE TABLE [dbo].[tChiTietChuyenHangDonHang] (
    [Ma]                  INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHangDonHang] INT NOT NULL,
    [MaChiTietDonHang]    INT NOT NULL,
    [SoLuong]             INT NOT NULL,
    CONSTRAINT [PK_tChiTietChuyenHangDonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietChuyenHangDonHang_tChiTietDonHang] FOREIGN KEY ([MaChiTietDonHang]) REFERENCES [dbo].[tChiTietDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietChuyenHangDonHang_tChuyenHangDonHang] FOREIGN KEY ([MaChuyenHangDonHang]) REFERENCES [dbo].[tChuyenHangDonHang] ([Ma])
);
















GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\ADMINI~1\AppData\Local\Temp\~vsF671.sql
CREATE TRIGGER [dbo].[tChiTietChuyenHangDonHang_trUpdateXong]
	ON [dbo].[tChiTietChuyenHangDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update [dbo].[tChiTietDonHang]
		set Xong = 0
		from (select MaChiTietDonHang as Ma from inserted
			union select MaChiTietDonHang as Ma from deleted) as t
		where Xong = 1 and [dbo].[tChiTietDonHang].Ma = t.Ma 
		and (SoLuong <> (select sum(SoLuong) from [dbo].[tChiTietChuyenHangDonHang] where MaChiTietDonHang = t.Ma)
		or NOT EXISTS(select * from [dbo].[tChiTietChuyenHangDonHang] where MaChiTietDonHang = t.Ma))

		update [dbo].[tChiTietDonHang]
		set Xong = 1
		from (select MaChiTietDonHang as Ma from inserted
			union select MaChiTietDonHang as Ma from deleted) as t
		where Xong = 0 and [dbo].[tChiTietDonHang].Ma = t.Ma 
		and SoLuong = (select sum(SoLuong) from [dbo].[tChiTietChuyenHangDonHang] where MaChiTietDonHang = t.Ma)					
	END