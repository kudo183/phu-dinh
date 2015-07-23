CREATE TABLE [dbo].[ThamSoNgay] (
    [Ma]     INT           IDENTITY (1, 1) NOT NULL,
    [Ten]    NVARCHAR (50) NOT NULL,
    [GiaTri] DATE          NOT NULL,
    CONSTRAINT [PK_ThamSoNgay] PRIMARY KEY CLUSTERED ([Ma] ASC)
);



