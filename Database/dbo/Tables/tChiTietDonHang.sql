CREATE TABLE [dbo].[tChiTietDonHang] (
    [Ma]        INT IDENTITY (1, 1) NOT NULL,
    [MaDonHang] INT NOT NULL,
    [MaMatHang] INT NOT NULL,
    [SoLuong]   INT NOT NULL,
    [Xong]      BIT CONSTRAINT [DF_tChiTietDonHang_Xong] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tChiTietDonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietDonHang_tDonHang] FOREIGN KEY ([MaDonHang]) REFERENCES [dbo].[tDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietDonHang_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);












GO
CREATE TRIGGER [dbo].[tChiTietDonHang_trUpdateXong]
	ON [dbo].[tChiTietDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		Declare @Evt Int = 0;

		SET NOCOUNT ON

		IF(EXISTS(SELECT * FROM inserted))
			Set @Evt = @Evt + 1;
		IF(EXISTS(SELECT * FROM deleted))
			Set @Evt = @Evt + 2;
			
		IF(@Evt = 1) -- inserted
		begin
			print('trigger inserted chitietdonhang')
			update [dbo].[tDonHang]
			set Xong = 0
			where [dbo].[tDonHang].Ma in (select distinct (i.MaDonHang) from inserted as i)
			and [dbo].[tDonHang].Xong = 1
		end

		if(@Evt = 2) -- deleted
		begin
			print('trigger deleted chitietdonhang')
			update [dbo].[tDonHang]
			set Xong = 1
			where [dbo].[tDonHang].Ma in (select distinct (d.MaDonHang) from deleted as d where d.Xong = 0)			
			and [dbo].[tDonHang].Xong = 0
			and NOT EXISTS(select * from [dbo].[tChiTietDonHang] as ct where ct.MaDonHang = [dbo].[tDonHang].Ma and ct.Xong = 0)
		end

		if(@Evt = 3) -- updated
		begin
			print('trigger updated chitietdonhang')
			-- update xong [tChiTietDonHang]
			if(update(SoLuong))
			begin
				update [dbo].[tChiTietDonHang]
				set Xong = 1
				from inserted as i
				where [dbo].[tChiTietDonHang].Ma = i.Ma
				and i.Xong = 0
				and [dbo].[tChiTietDonHang].SoLuong = (select Sum(ct.SoLuong) from [dbo].[tChiTietChuyenHangDonHang] as ct where ct.MaChiTietDonHang = [dbo].[tChiTietDonHang].Ma)

				update [dbo].[tChiTietDonHang]
				set Xong = 0
				from inserted as i
				where [dbo].[tChiTietDonHang].Ma = i.Ma
				and i.Xong = 1
				and ([dbo].[tChiTietDonHang].SoLuong <> (select Sum(ct.SoLuong) from [dbo].[tChiTietChuyenHangDonHang] as ct where ct.MaChiTietDonHang = [dbo].[tChiTietDonHang].Ma)
				or NOT EXISTS(select * from [dbo].[tChiTietChuyenHangDonHang] as ct where ct.MaChiTietDonHang = [dbo].[tChiTietDonHang].Ma))

				print('update xong chitietdonhang because update soluong')
			end
			-- truong hop insert
			update [dbo].[tDonHang]
			set Xong = 0
			where [dbo].[tDonHang].Ma in (select deleted.MaDonHang from deleted union select inserted.MaDonHang from inserted)
			and [dbo].[tDonHang].Xong = 1
			and EXISTS(select * from [dbo].[tChiTietDonHang] as ct where ct.MaDonHang = [dbo].[tDonHang].Ma and ct.Xong = 0)

			update [dbo].[tDonHang]
			set Xong = 1
			where [dbo].[tDonHang].Ma in (select deleted.MaDonHang from deleted union select inserted.MaDonHang from inserted)
			and [dbo].[tDonHang].Xong = 0
			and NOT EXISTS(select * from [dbo].[tChiTietDonHang] as ct where ct.MaDonHang = [dbo].[tDonHang].Ma and ct.Xong = 0)
		end
		 
	END
GO

CREATE TRIGGER [dbo].[tChiTietDonHang_trUpdateTonKho]
	ON [dbo].[tChiTietDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tk
		set tk.SoLuong = tk.SoLuong - i.SoLuong
		from inserted i join tDonHang dh on i.MaDonHang = dh.Ma
		join tTonKho tk on tk.MaMatHang = i.MaMatHang and tk.MaKhoHang = dh.MaKhoHang
		where tk.Ngay >= dh.Ngay

		update tk
		set tk.SoLuong = tk.SoLuong + d.SoLuong
		from deleted d join tDonHang dh on d.MaDonHang = dh.Ma
		join tTonKho tk on tk.MaMatHang = d.MaMatHang and tk.MaKhoHang = dh.MaKhoHang
		where tk.Ngay >= dh.Ngay

	END
GO

CREATE TRIGGER [dbo].[tChiTietDonHang_trUpdateDonHangTongSoLuong]
	ON [dbo].[tChiTietDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tDonHang
		set TongSoLuong = (select ISNULL(sum(SoLuong), 0) from tChiTietDonHang where MaDonHang = tDonHang.Ma)
		from (select i.MaDonHang from inserted i union select d.MaDonHang from deleted d) as t
		where tDonHang.Ma = t.MaDonHang
	END