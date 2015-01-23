CREATE TABLE [dbo].[tNhapHang] (
    [Ma]           INT  IDENTITY (1, 1) NOT NULL,
    [MaNhanVien]   INT  NOT NULL,
    [MaKhoHang]    INT  NOT NULL,
    [MaNhaCungCap] INT  NOT NULL,
    [Ngay]         DATE NOT NULL,
    CONSTRAINT [PK_tNhapHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tNhapHang_rKhoHang] FOREIGN KEY ([MaKhoHang]) REFERENCES [dbo].[rKhoHang] ([Ma]),
    CONSTRAINT [FK_tNhapHang_rNhaCungCap] FOREIGN KEY ([MaNhaCungCap]) REFERENCES [dbo].[rNhaCungCap] ([Ma]),
    CONSTRAINT [FK_tNhapHang_rNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [dbo].[rNhanVien] ([Ma])
);




GO

create TRIGGER [dbo].[tNhapHang_trUpdateTonKho]
	ON [dbo].[tNhapHang]
	after DELETE, INSERT, UPDATE
	AS
	BEGIN

		SET NOCOUNT ON

		IF(EXISTS(SELECT * FROM inserted) and EXISTS(SELECT * FROM deleted))
		BEGIN
			update tk
			set tk.SoLuong = tk.SoLuong + ct.SoLuong
			from inserted i join tChiTietNhapHang ct on i.Ma = ct.MaNhapHang
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = i.MaKhoHang
			where tk.Ngay >= i.Ngay

			update tk
			set tk.SoLuong = tk.SoLuong - ct.SoLuong
			from deleted d join tChiTietNhapHang ct on d.Ma = ct.MaNhapHang
			join tTonKho tk on tk.MaMatHang = ct.MaMatHang and tk.MaKhoHang = d.MaKhoHang
			where tk.Ngay >= d.Ngay
		END

	END