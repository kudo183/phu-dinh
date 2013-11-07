CREATE TABLE [dbo].[rNguyenLieu] (
    [Ma]               INT IDENTITY (1, 1) NOT NULL,
    [MaLoaiNguyenLieu] INT NOT NULL,
    [DuongKinh]        INT NOT NULL,
    CONSTRAINT [PK_rNguyenLieu] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_rNguyenLieu_rLoaiNguyenLieu] FOREIGN KEY ([MaLoaiNguyenLieu]) REFERENCES [dbo].[rLoaiNguyenLieu] ([Ma])
);

