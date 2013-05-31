CREATE TABLE [dbo].[rDiaDiem] (
    [Ma]   INT           NOT NULL,
    [Nuoc] NVARCHAR (50) NOT NULL,
    [Mien] NVARCHAR (50) NOT NULL,
    [Tinh] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_rDiaDiem] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

