CREATE TABLE [dbo].[tChiTietChuyenKho] (
    [Ma]          INT IDENTITY (1, 1) NOT NULL,
    [MaChuyenKho] INT NOT NULL,
    [MaMatHang]   INT NOT NULL,
    [SoLuong]     INT NOT NULL,
    CONSTRAINT [PK_tChiTietChuyenKho] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tChiTietChuyenKho_tChuyenKho] FOREIGN KEY ([MaChuyenKho]) REFERENCES [dbo].[tChuyenKho] ([Ma]),
    CONSTRAINT [FK_tChiTietChuyenKho_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);

