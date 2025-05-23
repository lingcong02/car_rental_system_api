USE [master]
GO
/****** Object:  Database [car_rental_system]    Script Date: 7/4/2025 3:22:39 PM ******/
CREATE DATABASE [car_rental_system]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'car_rental_system', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\car_rental_system.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'car_rental_system_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\car_rental_system_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [car_rental_system] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [car_rental_system].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [car_rental_system] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [car_rental_system] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [car_rental_system] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [car_rental_system] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [car_rental_system] SET ARITHABORT OFF 
GO
ALTER DATABASE [car_rental_system] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [car_rental_system] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [car_rental_system] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [car_rental_system] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [car_rental_system] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [car_rental_system] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [car_rental_system] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [car_rental_system] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [car_rental_system] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [car_rental_system] SET  DISABLE_BROKER 
GO
ALTER DATABASE [car_rental_system] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [car_rental_system] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [car_rental_system] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [car_rental_system] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [car_rental_system] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [car_rental_system] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [car_rental_system] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [car_rental_system] SET RECOVERY FULL 
GO
ALTER DATABASE [car_rental_system] SET  MULTI_USER 
GO
ALTER DATABASE [car_rental_system] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [car_rental_system] SET DB_CHAINING OFF 
GO
ALTER DATABASE [car_rental_system] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [car_rental_system] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [car_rental_system] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [car_rental_system] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'car_rental_system', N'ON'
GO
ALTER DATABASE [car_rental_system] SET QUERY_STORE = ON
GO
ALTER DATABASE [car_rental_system] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [car_rental_system]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 7/4/2025 3:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[adminId] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[role] [int] NOT NULL,
	[hash] [varbinary](64) NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[adminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 7/4/2025 3:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[bookingId] [int] IDENTITY(1,1) NOT NULL,
	[fkVehicleId] [int] NOT NULL,
	[fkUserId] [int] NOT NULL,
	[bookingNo] [varchar](50) NOT NULL,
	[customerName] [varchar](100) NOT NULL,
	[customerPhone] [varchar](50) NOT NULL,
	[customerEmail] [varchar](50) NOT NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NOT NULL,
	[totalPrice] [decimal](18, 2) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[bookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UniqueBookingNo_Booking] UNIQUE NONCLUSTERED 
(
	[bookingNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 7/4/2025 3:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[imageId] [int] IDENTITY(1,1) NOT NULL,
	[fkVehicleId] [int] NOT NULL,
	[path] [varchar](500) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[imageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/4/2025 3:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[hash] [varbinary](64) NOT NULL,
	[phone] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UniqueUserName_User] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 7/4/2025 3:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[vehicleId] [int] IDENTITY(1,1) NOT NULL,
	[fkVehicleModelId] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[platNo] [varchar](50) NOT NULL,
	[desc] [varchar](max) NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[vehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UniquePlatNo_Vehicle] UNIQUE NONCLUSTERED 
(
	[platNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleModel]    Script Date: 7/4/2025 3:22:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleModel](
	[vehicleModelId] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[createDate] [datetime] NOT NULL,
	[updateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_VehicleModel] PRIMARY KEY CLUSTERED 
(
	[vehicleModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([fkUserId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_User]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Vehicle] FOREIGN KEY([fkVehicleId])
REFERENCES [dbo].[Vehicle] ([vehicleId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Vehicle]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Vehicle] FOREIGN KEY([fkVehicleId])
REFERENCES [dbo].[Vehicle] ([vehicleId])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Vehicle]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_VehicleModel] FOREIGN KEY([fkVehicleModelId])
REFERENCES [dbo].[VehicleModel] ([vehicleModelId])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_VehicleModel]
GO
USE [master]
GO
ALTER DATABASE [car_rental_system] SET  READ_WRITE 
GO
