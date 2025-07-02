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
CREATE TABLE [Periods] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Periods] PRIMARY KEY ([Id])
);

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [NickName] nvarchar(100) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Level] int NOT NULL,
    [Rating] int NOT NULL,
    [IsPremium] bit NOT NULL,
    [XP] bigint NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Topics] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [PeriodId] int NOT NULL,
    CONSTRAINT [PK_Topics] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Topics_Periods_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [Periods] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Units] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Order] int NOT NULL,
    [TopicId] int NOT NULL,
    CONSTRAINT [PK_Units] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Units_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Topics_PeriodId] ON [Topics] ([PeriodId]);

CREATE INDEX [IX_Units_TopicId] ON [Units] ([TopicId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250623125637_AddPeriodIdToTopic', N'9.0.5');

COMMIT;
GO

