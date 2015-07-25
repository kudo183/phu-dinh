CREATE TABLE [dbo].[tChiTietChuyenHangDonHang] (
    [Ma]                  INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHangDonHang] INT NOT NULL,
    [MaChiTietDonHang]    INT NOT NULL,
    [SoLuong]             INT NOT NULL,
    [SoLuongTheoDonHang]  INT CONSTRAINT [DF_tChiTietChuyenHangDonHang_SoLuongTheoDonHang] DEFAULT ((0)) NOT NULL,
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
		IF trigger_nestlevel() < 2
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
	END
GO

GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\ADMINI~1\AppData\Local\Temp\~vsF671.sql
create TRIGGER [dbo].[tChiTietChuyenHangDonHang_trUpdateTongSoLuongThucTe]
	ON [dbo].[tChiTietChuyenHangDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
		
		update tChuyenHangDonHang
		set TongSoLuongThucTe = (select ISNULL(sum(SoLuong), 0) from tChiTietChuyenHangDonHang where MaChuyenHangDonHang = tChuyenHangDonHang.Ma)
		from (select i.MaChuyenHangDonHang from inserted i union select d.MaChuyenHangDonHang from deleted d) as t
		where tChuyenHangDonHang.Ma	 = t.MaChuyenHangDonHang

		update tChuyenHang
		set TongSoLuongThucTe = 
			(select ISNULL(sum(TongSoLuongThucTe), 0)
			from tChuyenHangDonHang chdh
			where chdh.MaChuyenHang = tChuyenHang.Ma)
		from (select i.MaChuyenHangDonHang from inserted i union select d.MaChuyenHangDonHang from deleted d) as t
		join tChuyenHangDonHang chdh on t.MaChuyenHangDonHang = chdh.Ma
		where tChuyenHang.Ma = chdh.MaChuyenHang
	END
GO
-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Users\ADMINI~1\AppData\Local\Temp\~vsF671.sql
create TRIGGER [dbo].[tChiTietChuyenHangDonHang_trUpdateSoLuongTheoDonHang]
	ON [dbo].[tChiTietChuyenHangDonHang]
	after INSERT, UPDATE
	AS
	BEGIN
		IF trigger_nestlevel() < 2
		BEGIN
			SET NOCOUNT ON
		
			update tChiTietChuyenHangDonHang
			set SoLuongTheoDonHang = (select SoLuong from tChiTietDonHang where Ma = tChiTietChuyenHangDonHang.MaChiTietDonHang)
			from inserted i
			where tChiTietChuyenHangDonHang.Ma	 = i.Ma
		END
	END