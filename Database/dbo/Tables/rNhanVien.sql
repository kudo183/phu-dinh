CREATE TABLE [dbo].[rNhanVien] (
    [Ma]           INT            IDENTITY (1, 1) NOT NULL,
    [MaPhuongTien] INT            NOT NULL,
    [TenNhanVien]  NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_NhanVienGiaoHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_rNhanVienGiaoHang_rPhuongTien] FOREIGN KEY ([MaPhuongTien]) REFERENCES [dbo].[rPhuongTien] ([Ma])
);

