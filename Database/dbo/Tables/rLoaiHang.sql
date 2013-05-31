CREATE TABLE [dbo].[rLoaiHang] (
    [Ma]      INT            IDENTITY (1, 1) NOT NULL,
    [TenLoai] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_rProductType] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

