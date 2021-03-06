USE [master]
GO
/****** Object:  Database [BusBooking]    Script Date: 17-Jun-20 15:10:12 ******/
CREATE DATABASE [BusBooking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BusBooking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BusBooking.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BusBooking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BusBooking_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BusBooking] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusBooking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusBooking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusBooking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusBooking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusBooking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusBooking] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusBooking] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BusBooking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusBooking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusBooking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusBooking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusBooking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusBooking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusBooking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusBooking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusBooking] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BusBooking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusBooking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusBooking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusBooking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusBooking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusBooking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BusBooking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusBooking] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BusBooking] SET  MULTI_USER 
GO
ALTER DATABASE [BusBooking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusBooking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusBooking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusBooking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BusBooking] SET DELAYED_DURABILITY = DISABLED 
GO
USE [BusBooking]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Images] [nvarchar](50) NULL,
	[Gender] [int] NULL CONSTRAINT [DF_Account_Gender]  DEFAULT ((1)),
	[DayCreate] [datetime] NULL,
	[DayEdited] [datetime] NULL,
	[EditerId] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Account_Stastus]  DEFAULT ((1)),
	[RoleId] [int] NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Account_Active]  DEFAULT ((1)),
	[Description] [nvarchar](max) NULL,
	[ForgotPass] [nvarchar](50) NULL,
	[StationId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DayCreate] [datetime] NOT NULL,
	[DayStart] [datetime] NOT NULL,
	[RouteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[QuantityTicket] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[BusId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bus]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Bus_Active]  DEFAULT ((1)),
	[Status] [bit] NOT NULL CONSTRAINT [DF_Bus_Status]  DEFAULT ((1)),
	[Image] [varchar](50) NOT NULL,
	[TotalSeat] [int] NOT NULL,
	[SeatEmpty] [int] NOT NULL,
	[CateId] [int] NOT NULL,
 CONSTRAINT [PK_Bus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Category_Active]  DEFAULT ((1)),
	[Status] [bit] NOT NULL CONSTRAINT [DF_Category_Status]  DEFAULT ((1)),
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Role_Status]  DEFAULT ((1)),
	[Active] [bit] NULL CONSTRAINT [DF_Role_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Routes]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Routes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationFrom] [int] NOT NULL,
	[StationTo] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Length] [int] NOT NULL,
	[TimeGo] [time](0) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Spacing_Active]  DEFAULT ((1)),
	[Status] [bit] NOT NULL CONSTRAINT [DF_Spacing_Status]  DEFAULT ((1)),
	[BusId] [int] NOT NULL,
	[TimeRun] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Spacing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Seat]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Seat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Seat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Station]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Station](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[City] [int] NOT NULL,
	[District] [int] NOT NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 17-Jun-20 15:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Userid] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (1, N'ngoctu@aa.com', N'b95495d2b655e0cd832244427261b76a', N'Ngọc Tú', CAST(N'2020-11-11' AS Date), N'Nghệ An', N'123', N'637276667255833142bg-2.jpg', 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 1, 1, N'No Description', NULL, 17, N'nv01')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (3, N'ntran@aa.com', N'b95495d2b655e0cd832244427261b76a', N'Ngọc Trân', CAST(N'2020-11-11' AS Date), N'Nghệ An', N'123', N'637276667255833142bg-2.jpg', 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 2, 1, N'No Description', NULL, 18, N'nv02')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (9, N'hs@gmail.com', N'b95495d2b655e0cd832244427261b76a', N'Hồng Sơn', CAST(N'2020-11-11' AS Date), N'Nghệ An', N'123', N'637276667255833142bg-2.jpg', 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 2, 1, N'No Description', NULL, 20, N'nv03')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (13, N'kc@gmail.com', N'dfsdfsdfsf2342342', N'Kiên', CAST(N'2020-11-11' AS Date), N'Nghệ  An', N'1234', N'637276667255833142bg-2.jpg', 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 2, 1, N'No Description', NULL, 22, N'nv04')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Bus] ON 

INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (1, N'Bus01', 1, 1, N'abc.jpg', 20, 242, 1)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (2, N'Bus02', 1, 1, N'abc.jpg', 20, 5, 9)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (3, N'Bus03', 1, 1, N'abc.jpg', 20, 10, 11)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (4, N'Bus04', 1, 1, N'abc.jpg', 20, 12, 12)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (5, N'Bus05', 1, 1, N'abc.jpg', 40, 12, 12)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (6, N'Bus06', 1, 1, N'abc.jpg', 30, 11, 11)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (7, N'Bus07', 1, 1, N'abc.jpg', 30, 12, 9)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (8, N'Bus08', 1, 1, N'abc.jpg', 40, 13, 11)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (9, N'Bus09', 1, 1, N'abc.jpg', 33, 22, 1)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (10, N'Bus10', 1, 1, N'abc.jpg', 40, 22, 12)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (12, N'dfgsd', 1, 1, N'637279888320598959GearVN_Lâu dài hùng vi_ (2).jpg', 453, 453, 11)
SET IDENTITY_INSERT [dbo].[Bus] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (1, N'Cate01', N'Express', 1, 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (9, N'Cate02', N'Luxury', 1, 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (11, N'Cate03', N'Volvo(NAC)', 1, 1, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (12, N'Cate04', N'Volvo(AC)', 1, 1, CAST(60000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (1, N'A', 1, 1)
INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (2, N'E', 1, 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Routes] ON 

INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (11, 17, 18, CAST(50000 AS Decimal(18, 0)), 500, CAST(N'07:00:00' AS Time), 0, 1, 1, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (12, 17, 18, CAST(50000 AS Decimal(18, 0)), 500, CAST(N'06:00:00' AS Time), 1, 1, 2, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (14, 17, 18, CAST(50000 AS Decimal(18, 0)), 500, CAST(N'08:00:00' AS Time), 1, 1, 3, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (15, 17, 18, CAST(50000 AS Decimal(18, 0)), 500, CAST(N'09:00:00' AS Time), 1, 1, 4, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (16, 17, 20, CAST(300000 AS Decimal(18, 0)), 2452, CAST(N'07:00:00' AS Time), 1, 1, 6, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (17, 17, 20, CAST(300000 AS Decimal(18, 0)), 2452, CAST(N'19:48:00' AS Time), 1, 1, 1, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (18, 20, 22, CAST(300000 AS Decimal(18, 0)), 200, CAST(N'12:49:00' AS Time), 1, 1, 7, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (19, 20, 22, CAST(300000 AS Decimal(18, 0)), 200, CAST(N'12:49:00' AS Time), 1, 1, 8, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (20, 21, 22, CAST(300000 AS Decimal(18, 0)), 2452, CAST(N'12:59:00' AS Time), 1, 1, 9, N'6h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (21, 19, 22, CAST(120000 AS Decimal(18, 0)), 500, CAST(N'13:25:00' AS Time), 1, 1, 7, N'12h')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (22, 19, 17, CAST(126000 AS Decimal(18, 0)), 2452, CAST(N'08:23:00' AS Time), 1, 1, 1, N'6h00')
SET IDENTITY_INSERT [dbo].[Routes] OFF
SET IDENTITY_INSERT [dbo].[Seat] ON 

INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (79, 1, N'Seat1.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (80, 1, N'Seat1.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (81, 1, N'Seat1.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (82, 1, N'Seat1.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (83, 1, N'Seat1.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (84, 1, N'Seat1.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (85, 1, N'Seat1.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (86, 1, N'Seat1.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (87, 1, N'Seat1.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (88, 1, N'Seat1.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (89, 2, N'Seat2.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (90, 2, N'Seat2.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (91, 2, N'Seat2.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (92, 2, N'Seat2.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (93, 2, N'Seat2.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (94, 2, N'Seat2.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (95, 2, N'Seat2.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (96, 2, N'Seat2.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (97, 2, N'Seat2.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (98, 2, N'Seat2.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (99, 3, N'Seat3.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (100, 3, N'Seat3.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (101, 3, N'Seat3.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (102, 3, N'Seat3.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (103, 3, N'Seat3.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (104, 3, N'Seat3.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (105, 3, N'Seat3.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (106, 3, N'Seat3.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (107, 3, N'Seat3.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (108, 3, N'Seat3.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (109, 4, N'Seat4.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (110, 4, N'Seat4.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (111, 4, N'Seat4.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (112, 4, N'Seat4.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (113, 4, N'Seat4.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (114, 4, N'Seat4.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (115, 4, N'Seat4.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (116, 4, N'Seat4.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (117, 4, N'Seat4.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (118, 4, N'Seat4.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (119, 5, N'Seat5.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (120, 5, N'Seat5.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (121, 5, N'Seat5.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (122, 5, N'Seat5.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (123, 5, N'Seat5.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (124, 5, N'Seat5.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (125, 5, N'Seat5.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (126, 5, N'Seat5.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (127, 5, N'Seat5.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (128, 5, N'Seat5.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (129, 6, N'Seat6.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (130, 6, N'Seat6.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (131, 6, N'Seat6.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (132, 6, N'Seat6.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (133, 6, N'Seat6.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (134, 6, N'Seat6.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (135, 6, N'Seat6.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (136, 6, N'Seat6.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (137, 6, N'Seat6.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (138, 6, N'Seat6.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (139, 7, N'Seat7.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (140, 7, N'Seat7.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (141, 7, N'Seat7.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (142, 7, N'Seat7.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (143, 7, N'Seat7.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (144, 7, N'Seat7.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (145, 7, N'Seat7.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (146, 7, N'Seat7.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (147, 7, N'Seat7.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (148, 7, N'Seat7.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (149, 8, N'Seat8.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (150, 8, N'Seat8.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (151, 8, N'Seat8.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (152, 8, N'Seat8.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (153, 8, N'Seat8.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (154, 8, N'Seat8.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (155, 8, N'Seat8.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (156, 8, N'Seat8.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (157, 8, N'Seat8.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (158, 8, N'Seat8.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (159, 9, N'Seat9.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (160, 9, N'Seat9.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (161, 9, N'Seat9.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (162, 9, N'Seat9.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (163, 9, N'Seat9.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (164, 9, N'Seat9.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (165, 9, N'Seat9.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (166, 9, N'Seat9.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (167, 9, N'Seat9.9', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (168, 9, N'Seat9.10', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (169, 10, N'Seat10.1', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (170, 10, N'Seat10.2', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (171, 10, N'Seat10.3', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (172, 10, N'Seat10.4', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (173, 10, N'Seat10.5', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (174, 10, N'Seat10.6', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (175, 10, N'Seat10.7', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (176, 10, N'Seat10.8', NULL)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (177, 10, N'Seat10.9', NULL)
GO
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (178, 10, N'Seat10.10', NULL)
SET IDENTITY_INSERT [dbo].[Seat] OFF
SET IDENTITY_INSERT [dbo].[Station] ON 

INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (17, N'Tiền Giang Station', N'Tiền Giang - Huyện Cái Bè', N'07823', 1, 1, 1, 279)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (18, N'Hưng Yên Station', N'Hưng Yên - Huyện Khoái Châu', N'45323', 1, 1, 2, 240)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (19, N'Hà Nội Station', N'Hà Nội - Huyện Ba Vì', N'23423', 1, 1, 3, 427)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (20, N'HCM Station', N'TP Hồ Chí Minh - Quận 1', N'323423', 1, 1, 4, 9)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (21, N'Cà Mau Station', N'Cà Mau - Huyện Cái Nước', N'523234', 1, 1, 5, 281)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (22, N'Nghệ An Station', N'Nghệ An - Thành Phố Vinh', N'654356', 1, 1, 27, 86)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (23, N'ádasd', N'Tiền Giang - Huyện Cái Bè', N'sdsa', 1, 1, 1, 279)
SET IDENTITY_INSERT [dbo].[Station] OFF
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_DayCreate]  DEFAULT (getdate()) FOR [DayCreate]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Station] FOREIGN KEY([StationId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Station]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Account] FOREIGN KEY([UserId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Account]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Bus] FOREIGN KEY([BusId])
REFERENCES [dbo].[Bus] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Bus]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Routes] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Routes] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Routes]
GO
ALTER TABLE [dbo].[Bus]  WITH CHECK ADD  CONSTRAINT [FK_Bus_Category1] FOREIGN KEY([CateId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Bus] CHECK CONSTRAINT [FK_Bus_Category1]
GO
ALTER TABLE [dbo].[Seat]  WITH CHECK ADD  CONSTRAINT [FK_Seat_Bus] FOREIGN KEY([BusId])
REFERENCES [dbo].[Bus] ([Id])
GO
ALTER TABLE [dbo].[Seat] CHECK CONSTRAINT [FK_Seat_Bus]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Account] FOREIGN KEY([Userid])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Account]
GO
USE [master]
GO
ALTER DATABASE [BusBooking] SET  READ_WRITE 
GO
