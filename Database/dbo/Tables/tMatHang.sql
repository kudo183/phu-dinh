CREATE TABLE [dbo].[tMatHang] (
    [Ma]         INT            IDENTITY (1, 1) NOT NULL,
    [MaLoai]     INT            NOT NULL,
    [TenMatHang] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tMatHang_rLoaiHang] FOREIGN KEY ([MaLoai]) REFERENCES [dbo].[rLoaiHang] ([Ma])
);

