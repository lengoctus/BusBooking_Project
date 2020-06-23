USE [master]
GO
/****** Object:  Database [BusBooking]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 23-Jun-20 21:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DOB] [date] NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Images] [nvarchar](50) NULL,
	[Gender] [int] NOT NULL CONSTRAINT [DF_Account_Gender]  DEFAULT ((1)),
	[DayCreate] [datetime] NULL,
	[DayEdited] [datetime] NULL,
	[EditerId] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Account_Stastus]  DEFAULT ((1)),
	[RoleId] [int] NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Account_Active]  DEFAULT ((1)),
	[Description] [nvarchar](max) NULL,
	[ForgotPass] [nvarchar](50) NULL,
	[StationId] [int] NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 23-Jun-20 21:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DayCreate] [datetime] NOT NULL CONSTRAINT [DF_Booking_DayCreate]  DEFAULT (getdate()),
	[DayStart] [datetime] NOT NULL,
	[RouteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[QuantityTicket] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[BusId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bus]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Routes]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Seat]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Station]    Script Date: 23-Jun-20 21:20:05 ******/
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
/****** Object:  Table [dbo].[Ticket]    Script Date: 23-Jun-20 21:20:05 ******/
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

INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (1, N'ngoctu@aa.com', N'b95495d2b655e0cd832244427261b76a', N'Ngoc Tu', NULL, NULL, N'0123456', N'ngoctu.jpg', 1, NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, N'012654')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (2027, N'empl01@gm.co', N'3d558122fcac9ea5fdc9ee8eccf8f433', N'lengoctu', CAST(N'2020-06-23' AS Date), N'quan12', N'1321311211', N'dui.jpg', 1, CAST(N'2020-06-22 14:15:46.850' AS DateTime), CAST(N'2020-06-23 15:22:23.513' AS DateTime), 1, 1, 2, 1, N'<p>ahihi</p>
', NULL, 22, N'012655')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (2055, N'ntran2@gmail.cc', N'0', N'Nghệ An Station', CAST(N'2020-06-23' AS Date), NULL, N'4523231127', N'dui.jpg', 1, CAST(N'2020-06-23 14:28:11.443' AS DateTime), CAST(N'2020-06-23 16:21:28.053' AS DateTime), 1, 1, 3, 1, N'Age:Middle-age', NULL, 17, N'123')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (2056, N'ntran2@gmail.cct', N'0', N'Nghệ An Station', CAST(N'2020-06-23' AS Date), NULL, N'4523231128', N'dui.jpg', 1, CAST(N'2020-06-23 15:09:53.227' AS DateTime), CAST(N'2020-06-23 16:21:13.573' AS DateTime), 1, 1, 3, 1, N'Age:Middle-age', NULL, 17, N'547')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (2057, N'eesds@gm3ail.com', N'0', N'ngoctuntu3', CAST(N'2020-06-23' AS Date), N'', N'0795631222', N'', 1, CAST(N'2020-06-23 20:50:54.640' AS DateTime), CAST(N'2020-06-23 20:50:54.640' AS DateTime), 0, 1, 3, 1, N'Age:Middle-age', NULL, NULL, N'480')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [RouteId], [UserId], [QuantityTicket], [Active], [BusId], [SeatId], [Code]) VALUES (2044, CAST(N'2020-06-23 14:28:11.853' AS DateTime), CAST(N'2020-06-26 00:00:00.000' AS DateTime), 1032, 2055, 1, 0, 29, 209, N'TVAI8171JO')
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [RouteId], [UserId], [QuantityTicket], [Active], [BusId], [SeatId], [Code]) VALUES (2045, CAST(N'2020-06-23 14:28:37.643' AS DateTime), CAST(N'2020-06-26 00:00:00.000' AS DateTime), 1032, 2055, 1, 0, 29, 210, N'GNNL2779UB')
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [RouteId], [UserId], [QuantityTicket], [Active], [BusId], [SeatId], [Code]) VALUES (2046, CAST(N'2020-06-23 14:30:30.057' AS DateTime), CAST(N'2020-06-28 00:00:00.000' AS DateTime), 1032, 2055, 1, 0, 29, 210, N'NZGO9737IS')
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [RouteId], [UserId], [QuantityTicket], [Active], [BusId], [SeatId], [Code]) VALUES (2048, CAST(N'2020-06-23 15:09:53.583' AS DateTime), CAST(N'2020-06-27 00:00:00.000' AS DateTime), 1029, 2056, 1, 1, 25, 198, N'SXVD6038BT')
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [RouteId], [UserId], [QuantityTicket], [Active], [BusId], [SeatId], [Code]) VALUES (2049, CAST(N'2020-06-23 20:50:55.037' AS DateTime), CAST(N'2020-06-28 00:00:00.000' AS DateTime), 1029, 2057, 1, 0, 25, 198, N'MJZF5343GV')
SET IDENTITY_INSERT [dbo].[Booking] OFF
SET IDENTITY_INSERT [dbo].[Bus] ON 

INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (25, N'00000', 1, 1, N'637284334381545974laudaihungvi2.jpg', 200, 197, 30)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (26, N'00001', 1, 1, N'637284335786564556laudaihungvi1.jpg', 200, 200, 31)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (27, N'00002', 1, 1, N'637284337090320450laudaihungvi2.jpg', 200, 200, 30)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (28, N'00003', 1, 1, N'637284337215962792laudaihungvi2.jpg', 200, 200, 30)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (29, N'00004', 1, 1, N'637284339772086452laudaihungvi1.jpg', 300, 296, 31)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (30, N'00005', 1, 0, N'637284339698694060laudaihungvi1.jpg', 300, 300, 31)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (31, N'00006', 1, 1, N'dui.jpg', 400, 400, 32)
SET IDENTITY_INSERT [dbo].[Bus] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (30, N'Cate01', N'Express', 1, 1, CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (31, N'Cate02', N'Luxury', 1, 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (32, N'Cate03', N'Volvo_NonAC', 1, 1, CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (33, N'Cate04', N'Volvo_AC', 1, 1, CAST(50000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (1, N'A', 1, 1)
INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (2, N'E', 1, 1)
INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (3, N'C', 1, 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Routes] ON 

INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (1029, 17, 18, CAST(45345 AS Decimal(18, 0)), 34, CAST(N'14:52:00' AS Time), 1, 1, 25, N'6')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (1030, 17, 18, CAST(45345 AS Decimal(18, 0)), 34, CAST(N'15:04:00' AS Time), 1, 1, 26, N'3')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (1031, 20, 21, CAST(1000 AS Decimal(18, 0)), 200, CAST(N'13:15:00' AS Time), 1, 1, 30, N'2')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (1032, 17, 18, CAST(45345 AS Decimal(18, 0)), 34, CAST(N'15:04:00' AS Time), 1, 1, 29, N'6')
INSERT [dbo].[Routes] ([Id], [StationFrom], [StationTo], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (1033, 17, 18, CAST(34343 AS Decimal(18, 0)), 31, CAST(N'15:05:00' AS Time), 1, 1, 31, N'2')
SET IDENTITY_INSERT [dbo].[Routes] OFF
SET IDENTITY_INSERT [dbo].[Seat] ON 

INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (198, 25, N'S01', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (199, 25, N'S02', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (200, 25, N'S03', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (201, 26, N'A1', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (202, 26, N'A2', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (203, 26, N'A3', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (204, 30, N'B1', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (205, 30, N'B2', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (206, 30, N'B3', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (207, 30, N'B4', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (208, 30, N'B5', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (209, 29, N'D1', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (210, 29, N'D2', 1)
INSERT [dbo].[Seat] ([Id], [BusId], [Code], [Status]) VALUES (211, 29, N'D3', 1)
SET IDENTITY_INSERT [dbo].[Seat] OFF
SET IDENTITY_INSERT [dbo].[Station] ON 

INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (17, N'Tiền Giang Station', N'Tiền Giang - Huyện Cái Bè', N'07823', 1, 1, 1, 279)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (18, N'Hưng Yên Station', N'Hưng Yên - Huyện Khoái Châu', N'45323', 1, 1, 2, 240)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (19, N'Hà Nội Station', N'Hà Nội - Huyện Ba Vì', N'23423', 1, 1, 3, 427)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (20, N'HCM Station', N'TP Hồ Chí Minh - Quận 1', N'323423', 1, 1, 4, 9)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (21, N'Cà Mau Station', N'Cà Mau - Huyện Cái Nước', N'523234', 1, 1, 5, 281)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (22, N'Nghệ An Station', N'Nghệ An - Thành Phố Vinh', N'654356', 1, 1, 27, 86)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (1025, N'Quảng Ninh Station', N'Quảng Ninh - Huyện Ba Chẽ', N'23423423431', 1, 0, 8, 637)
SET IDENTITY_INSERT [dbo].[Station] OFF
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([Id], [Userid], [TotalPrice], [PaymentStatus], [Code], [BookId]) VALUES (6, 2056, CAST(130690 AS Decimal(18, 0)), 0, N'RYZP7746NM', 2048)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
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
