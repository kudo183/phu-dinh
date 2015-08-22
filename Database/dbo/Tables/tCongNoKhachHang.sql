CREATE TABLE [dbo].[tCongNoKhachHang] (
    [Ma]          INT  IDENTITY (1, 1) NOT NULL,
    [MaKhachHang] INT  NOT NULL,
    [Ngay]        DATE NOT NULL,
    [SoTien]      INT  NOT NULL,
    CONSTRAINT [PK_tCongNoKhachHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tCongNoKhachHang_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma])
);

