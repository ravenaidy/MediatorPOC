USE [master]
GO

/****** Object:  Database [Mediator]    Script Date: 2022/02/23 20:16:48 ******/
CREATE DATABASE [Mediator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Mediator', FILENAME = N'/var/opt/mssql/data/Mediator.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Mediator_log', FILENAME = N'/var/opt/mssql/data/Mediator.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Mediator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Mediator] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Mediator] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Mediator] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Mediator] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Mediator] SET ARITHABORT OFF 
GO

ALTER DATABASE [Mediator] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Mediator] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Mediator] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Mediator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Mediator] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Mediator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Mediator] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Mediator] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Mediator] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Mediator] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Mediator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Mediator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Mediator] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Mediator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Mediator] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Mediator] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Mediator] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Mediator] SET RECOVERY FULL 
GO

ALTER DATABASE [Mediator] SET  MULTI_USER 
GO

ALTER DATABASE [Mediator] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Mediator] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Mediator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Mediator] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Mediator] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Mediator] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [Mediator] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Mediator] SET  READ_WRITE 
GO

USE [Mediator]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 2022/02/23 20:18:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserPassword] [nvarchar](15) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Account] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



/****** Object:  StoredProcedure [dbo].[pr_AccountExists]    Script Date: 2022/02/23 20:17:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_AccountExists]
	@Username NVARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT acc.AccountId
	FROM dbo.Account acc
	WHERE acc.UserName = @Username
END
GO

/****** Object:  StoredProcedure [dbo].[pr_CreateAccount]    Script Date: 2022/02/23 20:17:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alistair Ockhuis
-- Create date: 17/02/2022
-- Description:	Add Account
-- =============================================
CREATE PROCEDURE [dbo].[pr_CreateAccount]
	-- Add the parameters for the stored procedure here
	@Username NVARCHAR(50),
	@Password NVARCHAR(15)	
AS
BEGIN
	INSERT INTO dbo.Account (UserName, UserPassword)
	VALUES (@Username, @Password)
END
GO

/****** Object:  StoredProcedure [dbo].[pr_GetAccountByUsernameAndPassword]    Script Date: 2022/02/23 20:17:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_GetAccountByUsernameAndPassword]
	-- Add the parameters for the stored procedure here
	@Username NVARCHAR(50),
	@Password NVARCHAR(15)
AS
BEGIN
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT acc.AccountId,
	acc.UserName,
	acc.UserPassword,
	acc.IsActive,
	acc.CreatedDate
	FROM dbo.Account acc
	WHERE acc.UserName = @Username AND acc.UserPassword = @Password

END
GO

/****** Object:  StoredProcedure [dbo].[pr_GetAllAccounts]    Script Date: 2022/02/23 20:29:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_GetAllAccounts]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT acc.AccountId,
	acc.CreatedDate,
	acc.IsActive,
	acc.Username
	FROM [dbo].[Account] acc
	ORDER BY acc.AccountId
END
GO
