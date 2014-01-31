CREATE TABLE [dbo].[tChiTietNhapHang] (
    [Ma]         INT IDENTITY (1, 1) NOT NULL,
    [MaNhapHang] INT NOT NULL,
    [MaMatHang]  INT NOT NULL,
    [SoLuong]    INT NOT NULL,
    CONSTRAINT [PK_NhapMatHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietNhapHang_tNhapHang] FOREIGN KEY ([MaNhapHang]) REFERENCES [dbo].[tNhapHang] ([Ma]),
    CONSTRAINT [FK_tNhapMatHang_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);

