CREATE TABLE [dbo].[tChuyenHangDonHang] (
    [Ma]           INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHang] INT NOT NULL,
    [MaDonHang]    INT NOT NULL,
    CONSTRAINT [PK_tChuyenHangDonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChuyenHangDonHang_tChuyenHang] FOREIGN KEY ([MaChuyenHang]) REFERENCES [dbo].[tChuyenHang] ([Ma]),
    CONSTRAINT [FK_tChuyenHangDonHang_tDonHang] FOREIGN KEY ([MaDonHang]) REFERENCES [dbo].[tDonHang] ([Ma])
);

