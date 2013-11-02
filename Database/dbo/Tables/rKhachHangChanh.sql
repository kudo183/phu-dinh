CREATE TABLE [dbo].[rKhachHangChanh] (
    [Ma]          INT IDENTITY (1, 1) NOT NULL,
    [MaChanh]     INT NOT NULL,
    [MaKhachHang] INT NOT NULL,
    [LaMacDinh]   BIT CONSTRAINT [DF_rKhachHangChanh_LaMacDinh] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_rKhachHangChanh] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_rKhachHangChanh_rChanh] FOREIGN KEY ([MaChanh]) REFERENCES [dbo].[rChanh] ([Ma]),
    CONSTRAINT [FK_rKhachHangChanh_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma])
);

