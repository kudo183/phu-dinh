CREATE TABLE [dbo].[tChiTietChuyenHangDonHang] (
    [Ma]                  INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHangDonHang] INT NOT NULL,
    [MaChiTietDonHang]    INT NOT NULL,
    [SoLuong]             INT NOT NULL,
    CONSTRAINT [PK_tChiTietChuyenHangDonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietChuyenHangDonHang_tChiTietDonHang] FOREIGN KEY ([MaChiTietDonHang]) REFERENCES [dbo].[tChiTietDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietChuyenHangDonHang_tChuyenHangDonHang] FOREIGN KEY ([MaChuyenHangDonHang]) REFERENCES [dbo].[tChuyenHangDonHang] ([Ma])
);







