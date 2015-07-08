CREATE TABLE [dbo].[tDonHang] (
    [Ma]          INT  IDENTITY (1, 1) NOT NULL,
    [MaKhachHang] INT  NOT NULL,
    [MaChanh]     INT  NULL,
    [Ngay]        DATE NOT NULL,
    [Xong]        BIT  CONSTRAINT [DF_tDonHang_Xong] DEFAULT ((0)) NOT NULL,
    [MaKhoHang]   INT  CONSTRAINT [DF_tDonHang_MaKhoHang] DEFAULT ((1)) NOT NULL,
    [TongSoLuong] INT  CONSTRAINT [DF_tDonHang_TongSoLuong] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tDonHang_rChanh] FOREIGN KEY ([MaChanh]) REFERENCES [dbo].[rChanh] ([Ma]),
    CONSTRAINT [FK_tDonHang_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma]),
    CONSTRAINT [FK_tDonHang_rKhoHang] FOREIGN KEY ([MaKhoHang]) REFERENCES [dbo].[rKhoHang] ([Ma])
);
















GO
create TRIGGER [dbo].[tDonHang_trUpdateTonKho]
	ON [dbo].[tDonHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN

		SET NOCOUNT ON

		IF(EXISTS(SELECT * FROM inserted) and EXISTS(SELECT * FROM deleted))
		BEGIN
			update tk
			set tk.SoLuong = tk.SoLuong - ct.SoLuong
			from inserted i join tChiTietDonHang ct on i.Ma = ct.MaDonHang
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = i.MaKhoHang
			where tk.Ngay >= i.Ngay

			update tk
			set tk.SoLuong = tk.SoLuong + ct.SoLuong
			from deleted d join tChiTietDonHang ct on d.Ma = ct.MaDonHang
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = d.MaKhoHang
			where tk.Ngay >= d.Ngay
		END

	END
GO
create TRIGGER [dbo].[tDonHang_trUpdateTongSoLuongTheoDonHang]
	ON [dbo].[tDonHang]
	after UPDATE
	AS
	BEGIN

		SET NOCOUNT ON

		IF(update(TongSoLuong))
		BEGIN
			update tChuyenHangDonHang
			set TongSoLuongTheoDonHang = i.TongSoLuong
			from inserted i
			where tChuyenHangDonHang.MaDonHang = i.Ma

			update tChuyenHang
			set TongSoLuongTheoDonHang = (select ISNULL(sum(TongSoLuongTheoDonHang), 0) from tChuyenHangDonHang where MaChuyenHang = tChuyenHang.Ma)
			from (inserted i join tChuyenHangDonHang chdh on i.Ma = chdh.MaDonHang)
			where tChuyenHang.Ma = chdh.MaChuyenHang
		END

	END