CREATE TABLE [dbo].[rKhachHang] (
    [Ma]           INT            IDENTITY (1, 1) NOT NULL,
    [TenKhachHang] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_rKhachHang] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

