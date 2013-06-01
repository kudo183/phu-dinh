CREATE TABLE [dbo].[tChiTietChuyenHangDonHang] (
    [Ma]                  INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHangDonHang] INT NOT NULL,
    [MaMatHang]           INT NOT NULL,
    [SoLuong]             INT NOT NULL,
    CONSTRAINT [PK_tChiTietChuyenHangMatHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietChuyenHangDonHang_tChuyenHangDonHang] FOREIGN KEY ([MaChuyenHangDonHang]) REFERENCES [dbo].[tChuyenHangDonHang] ([Ma]),
    CONSTRAINT [FK_tChiTietChuyenHangDonHang_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);



