CREATE TABLE [dbo].[tNhapMatHang] (
    [Ma]         INT  IDENTITY (1, 1) NOT NULL,
    [Ngay]       DATE NOT NULL,
    [MaNhanVien] INT  NOT NULL,
    [MaMatHang]  INT  NOT NULL,
    [SoLuong]    INT  NOT NULL,
    CONSTRAINT [PK_NhapMatHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tNhapMatHang_rNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [dbo].[rNhanVien] ([Ma]),
    CONSTRAINT [FK_tNhapMatHang_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);

