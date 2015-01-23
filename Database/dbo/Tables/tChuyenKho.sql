CREATE TABLE [dbo].[tChuyenKho] (
    [Ma]            INT  IDENTITY (1, 1) NOT NULL,
    [MaNhanVien]    INT  NOT NULL,
    [MaKhoHangXuat] INT  NOT NULL,
    [MaKhoHangNhap] INT  NOT NULL,
    [Ngay]          DATE NOT NULL,
    CONSTRAINT [PK_tChuyenKho] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChuyenKho_rKhoHangNhap] FOREIGN KEY ([MaKhoHangNhap]) REFERENCES [dbo].[rKhoHang] ([Ma]),
    CONSTRAINT [FK_tChuyenKho_rKhoHangXuat] FOREIGN KEY ([MaKhoHangXuat]) REFERENCES [dbo].[rKhoHang] ([Ma]),
    CONSTRAINT [FK_tChuyenKho_rNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [dbo].[rNhanVien] ([Ma])
);




GO

create TRIGGER [dbo].[tChuyenKho_trUpdateTonKho]
	ON [dbo].[tChuyenKho]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN

		SET NOCOUNT ON

		IF(EXISTS(SELECT * FROM inserted) and EXISTS(SELECT * FROM deleted))
		BEGIN
			update tk
			set tk.SoLuong = tk.SoLuong - ct.SoLuong
			from inserted i join tChiTietChuyenKho ct on i.Ma = ct.MaChuyenKho
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = i.MaKhoHangXuat
			where tk.Ngay >= i.Ngay

			update tk
			set tk.SoLuong = tk.SoLuong + ct.SoLuong
			from inserted i join tChiTietChuyenKho ct on i.Ma = ct.MaChuyenKho
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = i.MaKhoHangNhap
			where tk.Ngay >= i.Ngay

			update tk
			set tk.SoLuong = tk.SoLuong + ct.SoLuong
			from deleted d join tChiTietChuyenKho ct on d.Ma = ct.MaChuyenKho
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = d.MaKhoHangXuat
			where tk.Ngay >= d.Ngay
						
			update tk
			set tk.SoLuong = tk.SoLuong - ct.SoLuong
			from deleted d join tChiTietChuyenKho ct on d.Ma = ct.MaChuyenKho
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = d.MaKhoHangNhap
			where tk.Ngay >= d.Ngay
		END

	END