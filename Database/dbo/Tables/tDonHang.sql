CREATE TABLE [dbo].[tDonHang] (
    [Ma]           INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenHang] INT NOT NULL,
    [MaKhachHang]  INT NOT NULL,
    [MaChanh]      INT NULL,
    CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tDonHang_rChanh] FOREIGN KEY ([MaChanh]) REFERENCES [dbo].[rChanh] ([Ma]),
    CONSTRAINT [FK_tDonHang_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma]),
    CONSTRAINT [FK_tDonHang_tChuyenHang] FOREIGN KEY ([MaChuyenHang]) REFERENCES [dbo].[tChuyenHang] ([Ma])
);



