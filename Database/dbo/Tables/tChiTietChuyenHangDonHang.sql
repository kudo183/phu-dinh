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
CREATE TRIGGER [dbo].[tChiTietChuyenHangDonHang_trUpdateXong]
	ON [dbo].[tChiTietChuyenHangDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		Declare @Evt Int = 0;
		Declare @SoLuong Int = 0;
		Declare @SoLuongChiTietDonHang Int = 0;
		Declare @MaDonHang Int = 0;
		Declare @MaChiTietDonHang Int = 0;
		Declare @MaChiTietDonHangDeleted Int = 0;
		Declare @Xong bit = 0;

		SET NOCOUNT ON

		IF(EXISTS(SELECT * FROM inserted))
			Set @Evt = @Evt + 1;
		IF(EXISTS(SELECT * FROM deleted))
			Set @Evt = @Evt + 2;
			
		IF(@Evt = 1) -- inserted
		begin
			select @MaChiTietDonHang = MaChiTietDonHang
			from inserted
		end

		if(@Evt = 2) -- deleted
		begin
			select @MaChiTietDonHang = MaChiTietDonHang
			from deleted
		end

		if(@Evt = 3) -- updated
		begin
			select @MaChiTietDonHang = MaChiTietDonHang
			from inserted

			select @MaChiTietDonHangDeleted = MaChiTietDonHang
			from deleted

			if(@MaChiTietDonHang <> @MaChiTietDonHangDeleted)
			begin
				update [dbo].[tChiTietDonHang]
				set Xong = 0
				where Ma = @MaChiTietDonHangDeleted
			end
		end
		
		select @SoLuongChiTietDonHang = SoLuong, @MaDonHang = MaDonHang
		from [dbo].[tChiTietDonHang]
		where Ma = @MaChiTietDonHang

		select @SoLuong = sum(SoLuong)
		from [dbo].[tChiTietChuyenHangDonHang]
		where MaChiTietDonHang = @MaChiTietDonHang

		set @Xong = 0;

		if(@SoLuong = @SoLuongChiTietDonHang)
			set @Xong = 1

		begin		
			update [dbo].[tChiTietDonHang]
			set Xong = @Xong
			where Ma = @MaChiTietDonHang

			set @Xong = 1;

			if(EXISTS(select * from [dbo].[tChiTietDonHang] where MaDonHang = @MaDonHang and Xong = 0))
				set @Xong = 0;

			update [dbo].[tDonHang]
			set Xong = @Xong
			where Ma = @MaDonHang
		end
	END