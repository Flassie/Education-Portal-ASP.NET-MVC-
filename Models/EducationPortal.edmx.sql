
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2018 16:22:26
-- Generated from EDMX file: C:\Users\Flassie\source\repos\EducationPortal\EducationPortal\Models\EducationPortal.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EducationPortalData];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CourseLesson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_CourseLesson];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLesson_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLesson] DROP CONSTRAINT [FK_UserLesson_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLesson_Lesson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLesson] DROP CONSTRAINT [FK_UserLesson_Lesson];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts_User] DROP CONSTRAINT [FK_User_inherits_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_Admin_inherits_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts_Admin] DROP CONSTRAINT [FK_Admin_inherits_Account];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[Lessons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lessons];
GO
IF OBJECT_ID(N'[dbo].[Accounts_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts_User];
GO
IF OBJECT_ID(N'[dbo].[Accounts_Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts_Admin];
GO
IF OBJECT_ID(N'[dbo].[UserLesson]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLesson];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [CreationTime] datetime  NOT NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [LastModified] datetime  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [About] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [CreationTime] datetime  NOT NULL
);
GO

-- Creating table 'Lessons'
CREATE TABLE [dbo].[Lessons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [VideoLink] nvarchar(max)  NOT NULL,
    [CourseId] int  NOT NULL,
    [CreationTime] datetime  NOT NULL
);
GO

-- Creating table 'Accounts_User'
CREATE TABLE [dbo].[Accounts_User] (
    [LastActivity] datetime  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Accounts_Admin'
CREATE TABLE [dbo].[Accounts_Admin] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserLesson'
CREATE TABLE [dbo].[UserLesson] (
    [FinishedUsers_Id] int  NOT NULL,
    [FinishedLessons_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [PK_Lessons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts_User'
ALTER TABLE [dbo].[Accounts_User]
ADD CONSTRAINT [PK_Accounts_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts_Admin'
ALTER TABLE [dbo].[Accounts_Admin]
ADD CONSTRAINT [PK_Accounts_Admin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [FinishedUsers_Id], [FinishedLessons_Id] in table 'UserLesson'
ALTER TABLE [dbo].[UserLesson]
ADD CONSTRAINT [PK_UserLesson]
    PRIMARY KEY CLUSTERED ([FinishedUsers_Id], [FinishedLessons_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CourseId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_CourseLesson]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseLesson'
CREATE INDEX [IX_FK_CourseLesson]
ON [dbo].[Lessons]
    ([CourseId]);
GO

-- Creating foreign key on [FinishedUsers_Id] in table 'UserLesson'
ALTER TABLE [dbo].[UserLesson]
ADD CONSTRAINT [FK_UserLesson_User]
    FOREIGN KEY ([FinishedUsers_Id])
    REFERENCES [dbo].[Accounts_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FinishedLessons_Id] in table 'UserLesson'
ALTER TABLE [dbo].[UserLesson]
ADD CONSTRAINT [FK_UserLesson_Lesson]
    FOREIGN KEY ([FinishedLessons_Id])
    REFERENCES [dbo].[Lessons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLesson_Lesson'
CREATE INDEX [IX_FK_UserLesson_Lesson]
ON [dbo].[UserLesson]
    ([FinishedLessons_Id]);
GO

-- Creating foreign key on [Id] in table 'Accounts_User'
ALTER TABLE [dbo].[Accounts_User]
ADD CONSTRAINT [FK_User_inherits_Account]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Accounts_Admin'
ALTER TABLE [dbo].[Accounts_Admin]
ADD CONSTRAINT [FK_Admin_inherits_Account]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------