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

