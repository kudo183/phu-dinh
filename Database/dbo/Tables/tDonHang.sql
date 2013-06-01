CREATE TABLE [dbo].[tDonHang] (
    [Ma]          INT  IDENTITY (1, 1) NOT NULL,
    [MaKhachHang] INT  NOT NULL,
    [MaChanh]     INT  NULL,
    [Ngay]        DATE NOT NULL,
    CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tDonHang_rChanh] FOREIGN KEY ([MaChanh]) REFERENCES [dbo].[rChanh] ([Ma]),
    CONSTRAINT [FK_tDonHang_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma])
);





