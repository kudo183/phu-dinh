CREATE TABLE [dbo].[rChanh] (
    [Ma]       INT            IDENTITY (1, 1) NOT NULL,
    [TenChanh] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_rChanh] PRIMARY KEY CLUSTERED ([Ma] ASC)
);

