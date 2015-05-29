USE [master]
GO
/****** Object:  Database [DBEmployee]    Script Date: 5/29/2015 9:24:18 AM ******/
CREATE DATABASE [DBEmployee]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBEmployee', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DBEmployee.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBEmployee_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DBEmployee_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBEmployee] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBEmployee].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBEmployee] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBEmployee] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBEmployee] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBEmployee] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBEmployee] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBEmployee] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBEmployee] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBEmployee] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBEmployee] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBEmployee] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBEmployee] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBEmployee] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBEmployee] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBEmployee] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBEmployee] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBEmployee] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBEmployee] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBEmployee] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBEmployee] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBEmployee] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBEmployee] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBEmployee] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBEmployee] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBEmployee] SET RECOVERY FULL 
GO
ALTER DATABASE [DBEmployee] SET  MULTI_USER 
GO
ALTER DATABASE [DBEmployee] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBEmployee] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBEmployee] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBEmployee] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBEmployee', N'ON'
GO
USE [DBEmployee]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/29/2015 9:24:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NULL,
	[LastName] [varchar](20) NULL,
	[Email] [varchar](30) NULL,
	[FSU] [varchar](10) NULL,
	[Position] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (1, N'Van', N'Nguyensdfsd', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (2, N'Van', N'Nguyen', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (3, N'Van', N'Nguyen', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (4, N'Van', N'Nguyen', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (5, N'Van', N'Nguyen', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (6, N'Van', N'Nguyen', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (7, N'Van', N'Nguyen', N'VanNTV', N'FSU1', N'Staff')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (8, N'bf', N'xzcb', N'zfzf', N'FSU2', N'zggzfd')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (9, N'bf', N'xzcb', N'zfzf', N'FSU3', N'zggzfd')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (10, N'zbf', N'xzgfdcb', N'zfzfgzf', N'FSU4', N'zggzfd')
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Email], [FSU], [Position]) VALUES (11, N'zgbf', N'xzzgcb', N'zfzzgf', N'FSU4', N'zggzfd')
SET IDENTITY_INSERT [dbo].[Employee] OFF
USE [master]
GO
ALTER DATABASE [DBEmployee] SET  READ_WRITE 
GO
