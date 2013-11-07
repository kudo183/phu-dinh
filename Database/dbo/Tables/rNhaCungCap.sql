CREATE TABLE [dbo].[rNhaCungCap] (
    [Ma]            INT         IDENTITY (1, 1) NOT NULL,
    [TenNhaCungCap] NCHAR (200) NOT NULL,
    CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

