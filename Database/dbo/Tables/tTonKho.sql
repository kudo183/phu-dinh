CREATE TABLE [dbo].[tTonKho] (
    [Ma]        INT  IDENTITY (1, 1) NOT NULL,
    [MaKhoHang] INT  NOT NULL,
    [MaMatHang] INT  NOT NULL,
    [Ngay]      DATE NOT NULL,
    [SoLuong]   INT  NOT NULL,
    [SoLuongCu] INT  NOT NULL,
    CONSTRAINT [PK_tTonKho] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tTonKho_rKhoHang] FOREIGN KEY ([MaKhoHang]) REFERENCES [dbo].[rKhoHang] ([Ma]),
    CONSTRAINT [FK_tTonKho_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);



