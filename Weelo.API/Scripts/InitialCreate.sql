IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Owners] (
    [IdOwner] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Photo] nvarchar(max) NOT NULL,
    [Birthday] datetime2 NOT NULL,
    CONSTRAINT [PK_Owners] PRIMARY KEY ([IdOwner])
);
GO

CREATE TABLE [Properties] (
    [IdProperty] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    [CodeInternal] nvarchar(max) NOT NULL,
    [Year] nvarchar(max) NOT NULL,
    [OwnerIdOwner] int NULL,
    CONSTRAINT [PK_Properties] PRIMARY KEY ([IdProperty]),
    CONSTRAINT [FK_Properties_Owners_OwnerIdOwner] FOREIGN KEY ([OwnerIdOwner]) REFERENCES [Owners] ([IdOwner]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PropertyImages] (
    [IdPropertyImage] int NOT NULL IDENTITY,
    [File] tinyint NOT NULL,
    [Enable] bit NOT NULL,
    [PropertyIdProperty] int NULL,
    CONSTRAINT [PK_PropertyImages] PRIMARY KEY ([IdPropertyImage]),
    CONSTRAINT [FK_PropertyImages_Properties_PropertyIdProperty] FOREIGN KEY ([PropertyIdProperty]) REFERENCES [Properties] ([IdProperty]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PropertyTraces] (
    [IdPropertyTrace] int NOT NULL IDENTITY,
    [DateSale] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [Tax] nvarchar(max) NOT NULL,
    [PropertyIdProperty] int NULL,
    CONSTRAINT [PK_PropertyTraces] PRIMARY KEY ([IdPropertyTrace]),
    CONSTRAINT [FK_PropertyTraces_Properties_PropertyIdProperty] FOREIGN KEY ([PropertyIdProperty]) REFERENCES [Properties] ([IdProperty]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Properties_OwnerIdOwner] ON [Properties] ([OwnerIdOwner]);
GO

CREATE INDEX [IX_PropertyImages_PropertyIdProperty] ON [PropertyImages] ([PropertyIdProperty]);
GO

CREATE INDEX [IX_PropertyTraces_PropertyIdProperty] ON [PropertyTraces] ([PropertyIdProperty]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220129020608_InitialCreate', N'6.0.0-preview.5.21301.9');
GO

COMMIT;
GO

