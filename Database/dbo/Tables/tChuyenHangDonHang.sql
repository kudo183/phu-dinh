CREATE TABLE [dbo].[tChuyenHangDonHang] (
    [Ma]                     INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHang]           INT NOT NULL,
    [MaDonHang]              INT NOT NULL,
    [TongSoLuongTheoDonHang] INT CONSTRAINT [DF_tChuyenHangDonHang_TongSoLuongTheoDonHang] DEFAULT ((0)) NOT NULL,
    [TongSoLuongThucTe]      INT CONSTRAINT [DF_tChuyenHangDonHang_TongSoLuongThucTe] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tChuyenHangDonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChuyenHangDonHang_tChuyenHang] FOREIGN KEY ([MaChuyenHang]) REFERENCES [dbo].[tChuyenHang] ([Ma]),
    CONSTRAINT [FK_tChuyenHangDonHang_tDonHang] FOREIGN KEY ([MaDonHang]) REFERENCES [dbo].[tDonHang] ([Ma])
);




GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\ADMINI~1\AppData\Local\Temp\~vsF671.sql
CREATE TRIGGER [dbo].[tChuyenHangDonHang_trUpdateChuyenHangDonHang_TongSoLuongTheoDonHang]
	ON [dbo].[tChuyenHangDonHang]
	after INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tChuyenHangDonHang
		set TongSoLuongTheoDonHang = (select TongSoLuong from tDonHang where Ma = tChuyenHangDonHang.MaDonHang)
		from inserted i
		where tChuyenHangDonHang.Ma = i.Ma
	END
GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\ADMINI~1\AppData\Local\Temp\~vsF671.sql
CREATE TRIGGER [dbo].[tChuyenHangDonHang_trUpdateChuyenHang_TongSoLuongTheoDonHang]
	ON [dbo].[tChuyenHangDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tChuyenHang
		set TongSoLuongTheoDonHang = (select ISNULL(sum(TongSoLuongTheoDonHang), 0) from tChuyenHangDonHang where MaChuyenHang = tChuyenHang.Ma)
		from (select i.MaChuyenHang from inserted i union select d.MaChuyenHang from deleted d) as t
		where tChuyenHang.Ma = t.MaChuyenHang
	END
GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\ADMINI~1\AppData\Local\Temp\~vsF671.sql
CREATE TRIGGER [dbo].[tChuyenHangDonHang_trUpdateChuyenHang_TongDonHang]
	ON [dbo].[tChuyenHangDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tChuyenHang
		set TongDonHang = (select count(*) from tChuyenHangDonHang where MaChuyenHang = tChuyenHang.Ma)
		from (select i.MaChuyenHang from inserted i union select d.MaChuyenHang from deleted d) as t
		where tChuyenHang.Ma = t.MaChuyenHang
	END