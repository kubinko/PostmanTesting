SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Workshops](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NOT NULL DEFAULT (''),
    [Description] nvarchar(500) NOT NULL DEFAULT (''),
    [Date] datetime2(2) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [Price] decimal(28,8) NOT NULL DEFAULT (0),
    [AttendeesCount] int NOT NULL DEFAULT (0),
    [CreatedTimestamp] datetimeoffset(2) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [CreatedBy] bigint NOT NULL,
    [LastModifiedTimestamp] datetimeoffset(2) NOT NULL,
    [LastModifiedBy] bigint NOT NULL
 CONSTRAINT [PK_Workshops] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[People](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50) NOT NULL DEFAULT (''),
    [Surname] nvarchar(100) NOT NULL DEFAULT (''),
    [Street] nvarchar(255) NOT NULL DEFAULT (''),
    [PostCode] nvarchar(20) NOT NULL DEFAULT (''),
    [City] nvarchar(255) NOT NULL DEFAULT (''),
    [Country] nvarchar(255) NOT NULL DEFAULT (''),
    [PhoneNumber] nvarchar(40) NOT NULL DEFAULT (''),
    [Email] nvarchar(255) NOT NULL DEFAULT (''),
    [CreatedTimestamp] datetimeoffset(2) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [CreatedBy] bigint NOT NULL,
    [LastModifiedTimestamp] datetimeoffset(2) NOT NULL,
    [LastModifiedBy] bigint NOT NULL
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO