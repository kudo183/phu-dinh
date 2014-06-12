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
create TRIGGER [dbo].[tChiTietDonHang_trUpdateXong]
	ON [dbo].[tChiTietDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		Declare @Evt Int = 0;
		Declare @SoLuong Int = 0;
		Declare @SoLuongChiTietDonHang Int = 0;
		Declare @MaDonHang Int = 0;
		Declare @MaChiTietDonHang Int = 0;
		Declare @Xong bit = 0;

		SET NOCOUNT ON

		IF(EXISTS(SELECT * FROM inserted))
			Set @Evt = @Evt + 1;
		IF(EXISTS(SELECT * FROM deleted))
			Set @Evt = @Evt + 2;
			
		IF(@Evt = 1) -- inserted
		begin
			select @MaDonHang = MaDonHang
			from inserted

			set @Xong = 0;
		end

		if(@Evt = 2) -- deleted
		begin
			select @MaDonHang = MaDonHang
			from deleted
			
			set @Xong = 1;

			if(EXISTS(select * from [dbo].[tChiTietDonHang] where MaDonHang = @MaDonHang and Xong = 0))
				set @Xong = 0;
		end

		if(@Evt = 3) -- updated
		begin
			select @MaChiTietDonHang = Ma, @SoLuongChiTietDonHang = SoLuong, @MaDonHang = MaDonHang
			from inserted
				
			if(EXISTS(select * from [dbo].[tChiTietChuyenHangDonHang] where MaChiTietDonHang = @MaChiTietDonHang))
			begin
				select @SoLuong = sum(SoLuong)
				from [dbo].[tChiTietChuyenHangDonHang]
				where MaChiTietDonHang = @MaChiTietDonHang

				set @Xong = 0;

				if(@SoLuong = @SoLuongChiTietDonHang)
					set @Xong = 1;

				update [dbo].[tChiTietDonHang]
				set Xong = @Xong
				where Ma = @MaChiTietDonHang
								
				set @Xong = 1;

				if(EXISTS(select * from [dbo].[tChiTietDonHang] where MaDonHang = @MaDonHang and Xong = 0))
					set @Xong = 0;
			end
		end
		
		begin		
			update [dbo].[tDonHang]
			set Xong = @Xong
			where Ma = @MaDonHang
		end
	END