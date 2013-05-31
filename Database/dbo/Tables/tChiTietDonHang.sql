CREATE TABLE [dbo].[tChiTietDonHang] (
    [Ma]        INT IDENTITY (1, 1) NOT NULL,
    [MaDonHang] INT NOT NULL,
    [MaMatHang] INT NOT NULL,
    [SoLuong]   INT NOT NULL,
    CONSTRAINT [PK_tChiTietDonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietDonHang_tDonHang] FOREIGN KEY ([MaDonHang]) REFERENCES [dbo].[tDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietDonHang_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);

