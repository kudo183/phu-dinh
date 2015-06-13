CREATE TABLE [dbo].[tToaHang] (
    [Ma]          INT  IDENTITY (1, 1) NOT NULL,
    [Ngay]        DATE NOT NULL,
    [MaKhachHang] INT  NOT NULL,
    CONSTRAINT [PK_tToaHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tToaHang_rKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[rKhachHang] ([Ma])
);

