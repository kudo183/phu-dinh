CREATE TABLE [dbo].[tChiTietToaHang] (
    [Ma]               INT IDENTITY (1, 1) NOT NULL,
    [MaToaHang]        INT NOT NULL,
    [MaChiTietDonHang] INT NOT NULL,
    [GiaTien]          INT NOT NULL,
    CONSTRAINT [PK_tChiTietToaHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietToaHang_tChiTietDonHang] FOREIGN KEY ([MaChiTietDonHang]) REFERENCES [dbo].[tChiTietDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietToaHang_tToaHang] FOREIGN KEY ([MaToaHang]) REFERENCES [dbo].[tToaHang] ([Ma])
);




GO

create TRIGGER [dbo].[tChiTietToaHang_trUpdateCongNo]
	ON [dbo].[tChiTietToaHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update cn
		set cn.SoTien = cn.SoTien + (i.GiaTien*ct.SoLuong)
		from inserted i join tToaHang th on i.MaToaHang = th.Ma
		join tCongNoKhachHang cn on cn.MaKhachHang = th.MaKhachHang
		join tChiTietDonHang ct on i.MaChiTietDonHang = ct.Ma
		where cn.Ngay >= th.Ngay

		update cn
		set cn.SoTien = cn.SoTien - (d.GiaTien*ct.SoLuong)
		from deleted d join tToaHang th on d.MaToaHang = th.Ma
		join tCongNoKhachHang cn on cn.MaKhachHang = th.MaKhachHang
		join tChiTietDonHang ct on d.MaChiTietDonHang = ct.Ma
		where cn.Ngay >= th.Ngay
	END