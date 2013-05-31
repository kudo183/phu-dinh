CREATE TABLE [dbo].[rKhachHang] (
    [Ma]           INT            IDENTITY (1, 1) NOT NULL,
    [MaDiaDiem]    INT            NOT NULL,
    [TenKhachHang] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_rKhachHang] PRIMARY KEY CLUSTERED ([Ma] ASC),
    CONSTRAINT [FK_rKhachHang_rDiaDiem] FOREIGN KEY ([MaDiaDiem]) REFERENCES [dbo].[rDiaDiem] ([Ma])
);



