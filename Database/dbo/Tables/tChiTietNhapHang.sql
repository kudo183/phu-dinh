CREATE TABLE [dbo].[tChiTietNhapHang] (
    [Ma]         INT IDENTITY (1, 1) NOT NULL,
    [MaNhapHang] INT NOT NULL,
    [MaMatHang]  INT NOT NULL,
    [SoLuong]    INT NOT NULL,
    CONSTRAINT [PK_NhapMatHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietNhapHang_tNhapHang] FOREIGN KEY ([MaNhapHang]) REFERENCES [dbo].[tNhapHang] ([Ma]),
    CONSTRAINT [FK_tNhapMatHang_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);




GO

create TRIGGER [dbo].[tChiTietNhapHang_trUpdateTonKho]
	ON [dbo].[tChiTietNhapHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tk
		set tk.SoLuong = tk.SoLuong + i.SoLuong
		from inserted i join tNhapHang nh on i.MaNhapHang = nh.Ma
		join tTonKho tk on tk.MaMatHang = i.MaMatHang and tk.MaKhoHang = nh.MaKhoHang
		where tk.Ngay >= nh.Ngay

		update tk
		set tk.SoLuong = tk.SoLuong - d.SoLuong
		from deleted d join tNhapHang nh on d.MaNhapHang = nh.Ma
		join tTonKho tk on tk.MaMatHang = d.MaMatHang and tk.MaKhoHang = nh.MaKhoHang
		where tk.Ngay >= nh.Ngay

	END