CREATE TABLE [dbo].[rMatHangNguyenLieu] (
    [Ma]           INT IDENTITY (1, 1) NOT NULL,
    [MaMatHang]    INT NOT NULL,
    [MaNguyenLieu] INT NOT NULL,
    CONSTRAINT [PK_rMatHangNguyenLieu] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_rMatHangNguyenLieu_rNguyenLieu] FOREIGN KEY ([MaNguyenLieu]) REFERENCES [dbo].[rNguyenLieu] ([Ma]),
    CONSTRAINT [FK_rMatHangNguyenLieu_tMatHang] FOREIGN KEY ([MaMatHang]) REFERENCES [dbo].[tMatHang] ([Ma])
);

