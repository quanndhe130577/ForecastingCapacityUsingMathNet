USE [master]
GO
/****** Object:  Database [ForecastingCapacity]    Script Date: 6/2/2020 5:27:12 PM ******/
CREATE DATABASE [ForecastingCapacity]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ForecastingCapacity', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ForecastingCapacity.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ForecastingCapacity_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ForecastingCapacity_log.ldf' , SIZE = 401408KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ForecastingCapacity] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ForecastingCapacity].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ForecastingCapacity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET ARITHABORT OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ForecastingCapacity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ForecastingCapacity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ForecastingCapacity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ForecastingCapacity] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET RECOVERY FULL 
GO
ALTER DATABASE [ForecastingCapacity] SET  MULTI_USER 
GO
ALTER DATABASE [ForecastingCapacity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ForecastingCapacity] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ForecastingCapacity] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ForecastingCapacity] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ForecastingCapacity] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ForecastingCapacity', N'ON'
GO
ALTER DATABASE [ForecastingCapacity] SET QUERY_STORE = OFF
GO
USE [ForecastingCapacity]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 6/2/2020 5:27:12 PM ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [DESKTOP-VD9SS8K\quanh]    Script Date: 6/2/2020 5:27:12 PM ******/
CREATE USER [DESKTOP-VD9SS8K\quanh] FOR LOGIN [DESKTOP-VD9SS8K\quanh] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_owner] ADD MEMBER [DESKTOP-VD9SS8K\quanh]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/2/2020 5:27:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/2/2020 5:27:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Fullname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[IdentifyCode] [varchar](20) NOT NULL,
	[SaltPassword] [varchar](20) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DuLieuDuDoan]    Script Date: 6/2/2020 5:27:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DuLieuDuDoan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[capacity_db] [float] NOT NULL,
	[ghi_db] [float] NOT NULL,
	[envtemp_db] [float] NOT NULL,
	[time_db] [datetime] NOT NULL,
 CONSTRAINT [PK_DuLieuDuBao] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DuLieuLichSu]    Script Date: 6/2/2020 5:27:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DuLieuLichSu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[capacity] [float] NOT NULL,
	[ghi] [float] NOT NULL,
	[envtemp] [float] NOT NULL,
	[time] [datetime] NOT NULL,
 CONSTRAINT [PK_DuLieuLichSu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ForecastingCapacity] SET  READ_WRITE 
GO
