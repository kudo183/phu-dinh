CREATE TABLE [dbo].[tChiTietChuyenKho] (
    [Ma]          INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenKho] INT NOT NULL,
    [MaMatHang]   INT NOT NULL,
    [SoLuong]     INT NOT NULL,
    CONSTRAINT [PK_tChiTietChuyenKho] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietChuyenKho_tChuyenKho] FOREIGN KEY ([MaChuyenKho]) REFERENCES [dbo].[tChuyenKho] ([Ma]),
    CONSTRAINT [FK_tChiTietChuyenKho_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);




GO


CREATE TRIGGER [dbo].[tChiTietChuyenKho_trUpdateTonKho]
	ON [dbo].[tChiTietChuyenKho]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		update tk
		set tk.SoLuong = tk.SoLuong - i.SoLuong
		from inserted i join tChuyenKho dh on i.MaChuyenKho = dh.Ma
		join tTonKho tk on tk.MaMatHang = i.MaMatHang and tk.MaKhoHang = dh.MaKhoHangXuat
		where tk.Ngay >= dh.Ngay
		
		update tk
		set tk.SoLuong = tk.SoLuong + i.SoLuong
		from inserted i join tChuyenKho dh on i.MaChuyenKho = dh.Ma
		join tTonKho tk on tk.MaMatHang = i.MaMatHang and tk.MaKhoHang = dh.MaKhoHangNhap
		where tk.Ngay >= dh.Ngay

		update tk
		set tk.SoLuong = tk.SoLuong + d.SoLuong
		from deleted d join tChuyenKho dh on d.MaChuyenKho = dh.Ma
		join tTonKho tk on tk.MaMatHang = d.MaMatHang and tk.MaKhoHang = dh.MaKhoHangXuat
		where tk.Ngay >= dh.Ngay
		
		update tk
		set tk.SoLuong = tk.SoLuong - d.SoLuong
		from deleted d join tChuyenKho dh on d.MaChuyenKho = dh.Ma
		join tTonKho tk on tk.MaMatHang = d.MaMatHang and tk.MaKhoHang = dh.MaKhoHangNhap
		where tk.Ngay >= dh.Ngay

	END