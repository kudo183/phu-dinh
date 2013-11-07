CREATE TABLE [dbo].[rLoaiNguyenLieu] (
    [Ma]      INT         IDENTITY (1, 1) NOT NULL,
    [TenLoai] NCHAR (100) NOT NULL,
    CONSTRAINT [PK_rLoaiNguyenLieu] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

