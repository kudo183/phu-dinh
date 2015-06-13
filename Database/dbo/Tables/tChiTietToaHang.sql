CREATE TABLE [dbo].[tChiTietToaHang] (
    [Ma]               INT IDENTITY (1, 1) NOT NULL,
    [MaToaHang]        INT NOT NULL,
    [MaChiTietDonHang] INT NOT NULL,
    [GiaTien]          INT NOT NULL,
    CONSTRAINT [PK_tChiTietToaHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietToaHang_tChiTietDonHang] FOREIGN KEY ([MaChiTietDonHang]) REFERENCES [dbo].[tChiTietDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietToaHang_tToaHang] FOREIGN KEY ([MaToaHang]) REFERENCES [dbo].[tToaHang] ([Ma])
);

