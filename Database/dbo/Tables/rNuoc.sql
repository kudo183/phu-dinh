CREATE TABLE [dbo].[rNuoc] (
    [Ma]      INT            IDENTITY (1, 1) NOT NULL,
    [TenNuoc] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_rNuoc] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

