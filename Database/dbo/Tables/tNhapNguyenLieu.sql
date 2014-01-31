CREATE TABLE [dbo].[tNhapNguyenLieu] (
    [Ma]           INT  IDENTITY (1, 1) NOT NULL,
    [Ngay]         DATE NOT NULL,
    [MaNguyenLieu] INT  NOT NULL,
    [MaNhaCungCap] INT  NOT NULL,
    [SoLuong]      INT  NOT NULL,
    CONSTRAINT [PK_NhapNguyenLieu] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_tNhapNguyenLieu_rNguyenLieu] FOREIGN KEY ([MaNguyenLieu]) REFERENCES [dbo].[rNguyenLieu] ([Ma]),
    CONSTRAINT [FK_tNhapNguyenLieu_rNhaCungCap] FOREIGN KEY ([MaNhaCungCap]) REFERENCES [dbo].[rNhaCungCap] ([Ma])
);



