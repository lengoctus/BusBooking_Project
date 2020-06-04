USE [master]
GO
/****** Object:  Database [BusBooking]    Script Date: 04-Jun-20 00:11:17 ******/
CREATE DATABASE [BusBooking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BusBooking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BusBooking.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BusBooking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BusBooking_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
ALTER DATABASE [BusBooking] SET AUTO_CLOSE OFF 
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
ALTER DATABASE [BusBooking] SET  DISABLE_BROKER 
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
/****** Object:  Table [dbo].[Account]    Script Date: 04-Jun-20 00:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Images] [varchar](50) NULL,
	[Gender] [tinyint] NULL CONSTRAINT [DF_Account_Gender]  DEFAULT ((1)),
	[DayCreate] [datetime] NULL,
	[DayEdited] [datetime] NULL,
	[EditerId] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Account_Stastus]  DEFAULT ((1)),
	[RoleId] [tinyint] NULL,
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
/****** Object:  Table [dbo].[Booking]    Script Date: 04-Jun-20 00:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DayCreate] [datetime] NULL,
	[DayStart] [datetime] NULL,
	[FromCIty] [int] NULL,
	[FromDistrict] [int] NULL,
	[UserId] [int] NULL,
	[QuantityTicket] [int] NULL,
	[Active] [bit] NULL,
	[UserId2] [int] NULL,
	[BusId] [int] NULL,
	[SeatId] [int] NULL,
	[ToCity] [int] NULL,
	[ToDistrict] [int] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bus]    Script Date: 04-Jun-20 00:11:17 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 04-Jun-20 00:11:17 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 04-Jun-20 00:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Role_Status]  DEFAULT ((1)),
	[Active] [bit] NULL CONSTRAINT [DF_Role_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Seat]    Script Date: 04-Jun-20 00:11:17 ******/
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
 CONSTRAINT [PK_Seat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Spacing]    Script Date: 04-Jun-20 00:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spacing](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromSp] [int] NOT NULL,
	[ToSp] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Length] [int] NOT NULL,
	[TimeGo] [int] NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Spacing_Active]  DEFAULT ((1)),
	[Status] [bit] NOT NULL CONSTRAINT [DF_Spacing_Status]  DEFAULT ((1)),
	[BusId] [int] NOT NULL,
	[TimeRun] [int] NOT NULL,
 CONSTRAINT [PK_Spacing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Station]    Script Date: 04-Jun-20 00:11:17 ******/
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
	[City] [int] NULL,
	[District] [int] NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 04-Jun-20 00:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookingId] [int] NOT NULL,
	[SpacingId] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (1, N'ngoctu@aa.com', N'b95495d2b655e0cd832244427261b76a', N'Ngọc Tú', CAST(N'2020-11-11' AS Date), N'Nghệ An', N'123', NULL, 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 1, 1, N'No Description', NULL, 3, N'nv01')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (3, N'ntran@aa.com', N'b95495d2b655e0cd832244427261b76a', N'Ngọc Trân', CAST(N'2020-11-11' AS Date), N'Nghệ An', N'123', NULL, 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 2, 1, N'No Description', NULL, 5, N'nv02')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (9, N'hs@gmail.com', N'b95495d2b655e0cd832244427261b76a', N'Hồng Sơn', CAST(N'2020-11-11' AS Date), N'Nghệ An', N'123', NULL, 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, 2, 1, N'No Description', NULL, 6, N'nv03')
INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId], [Code]) VALUES (13, N'kc@gmail.com', N'dfsdfsdfsf2342342', N'Kiên', CAST(N'2020-11-11' AS Date), N'Nghệ  An', N'1234', NULL, 1, CAST(N'2020-06-03 00:00:00.000' AS DateTime), NULL, NULL, 1, NULL, 1, N'No Description', NULL, 7, N'nv04')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [FromCIty], [FromDistrict], [UserId], [QuantityTicket], [Active], [UserId2], [BusId], [SeatId], [ToCity], [ToDistrict]) VALUES (1, CAST(N'2020-06-01 00:00:00.000' AS DateTime), CAST(N'2020-06-02 00:00:00.000' AS DateTime), 1, 4, 13, 3, 1, NULL, 1, 79, 2, 4)
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [FromCIty], [FromDistrict], [UserId], [QuantityTicket], [Active], [UserId2], [BusId], [SeatId], [ToCity], [ToDistrict]) VALUES (2, CAST(N'2020-05-29 00:00:00.000' AS DateTime), CAST(N'2020-05-30 00:00:00.000' AS DateTime), 2, 5, 13, 2, 1, NULL, 2, 89, 3, 5)
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [FromCIty], [FromDistrict], [UserId], [QuantityTicket], [Active], [UserId2], [BusId], [SeatId], [ToCity], [ToDistrict]) VALUES (3, CAST(N'2020-05-31 00:00:00.000' AS DateTime), CAST(N'2020-06-01 00:00:00.000' AS DateTime), 4, 7, 13, 1, 1, NULL, 5, 122, 7, 21)
INSERT [dbo].[Booking] ([Id], [DayCreate], [DayStart], [FromCIty], [FromDistrict], [UserId], [QuantityTicket], [Active], [UserId2], [BusId], [SeatId], [ToCity], [ToDistrict]) VALUES (4, CAST(N'2020-01-01 00:00:00.000' AS DateTime), CAST(N'2020-01-02 00:00:00.000' AS DateTime), 1, 4, 13, 4, 1, NULL, 3, 107, 3, 6)
SET IDENTITY_INSERT [dbo].[Booking] OFF
SET IDENTITY_INSERT [dbo].[Bus] ON 

INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (1, N'Bus01', 1, 1, N'abc.jpg', 20, 10, 1)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (2, N'Bus02', 1, 1, N'abc.jpg', 20, 5, 9)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (3, N'Bus03', 1, 1, N'abc.jpg', 20, 10, 11)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (4, N'Bus04', 1, 1, N'abc.jpg', 20, 12, 12)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (5, N'Bus05', 1, 1, N'abc.jpg', 40, 12, 12)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (6, N'Bus06', 1, 1, N'abc.jpg', 30, 11, 11)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (7, N'Bus07', 1, 1, N'abc.jpg', 30, 12, 9)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (8, N'Bus08', 1, 1, N'abc.jpg', 40, 13, 11)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (9, N'Bus09', 1, 1, N'abc.jpg', 33, 22, 1)
INSERT [dbo].[Bus] ([Id], [Code], [Active], [Status], [Image], [TotalSeat], [SeatEmpty], [CateId]) VALUES (10, N'Bus10', 1, 1, N'abc.jpg', 40, 22, 12)
SET IDENTITY_INSERT [dbo].[Bus] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (1, N'Cate01', N'Express', 1, 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (9, N'Cate02', N'Luxury', 1, 1, CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (11, N'Cate03', N'Volvo(NAC)', 0, 1, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[Category] ([Id], [Code], [Name], [Active], [Status], [Price]) VALUES (12, N'Cate04', N'Volvo(AC)', 0, 1, CAST(60000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (1, N'A', 1, 1)
INSERT [dbo].[Role] ([Id], [Name], [Status], [Active]) VALUES (2, N'E', 1, 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Seat] ON 

INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (79, 1, N'Seat1.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (80, 1, N'Seat1.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (81, 1, N'Seat1.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (82, 1, N'Seat1.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (83, 1, N'Seat1.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (84, 1, N'Seat1.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (85, 1, N'Seat1.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (86, 1, N'Seat1.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (87, 1, N'Seat1.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (88, 1, N'Seat1.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (89, 2, N'Seat2.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (90, 2, N'Seat2.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (91, 2, N'Seat2.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (92, 2, N'Seat2.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (93, 2, N'Seat2.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (94, 2, N'Seat2.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (95, 2, N'Seat2.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (96, 2, N'Seat2.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (97, 2, N'Seat2.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (98, 2, N'Seat2.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (99, 3, N'Seat3.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (100, 3, N'Seat3.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (101, 3, N'Seat3.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (102, 3, N'Seat3.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (103, 3, N'Seat3.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (104, 3, N'Seat3.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (105, 3, N'Seat3.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (106, 3, N'Seat3.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (107, 3, N'Seat3.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (108, 3, N'Seat3.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (109, 4, N'Seat4.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (110, 4, N'Seat4.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (111, 4, N'Seat4.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (112, 4, N'Seat4.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (113, 4, N'Seat4.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (114, 4, N'Seat4.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (115, 4, N'Seat4.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (116, 4, N'Seat4.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (117, 4, N'Seat4.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (118, 4, N'Seat4.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (119, 5, N'Seat5.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (120, 5, N'Seat5.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (121, 5, N'Seat5.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (122, 5, N'Seat5.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (123, 5, N'Seat5.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (124, 5, N'Seat5.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (125, 5, N'Seat5.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (126, 5, N'Seat5.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (127, 5, N'Seat5.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (128, 5, N'Seat5.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (129, 6, N'Seat6.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (130, 6, N'Seat6.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (131, 6, N'Seat6.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (132, 6, N'Seat6.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (133, 6, N'Seat6.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (134, 6, N'Seat6.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (135, 6, N'Seat6.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (136, 6, N'Seat6.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (137, 6, N'Seat6.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (138, 6, N'Seat6.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (139, 7, N'Seat7.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (140, 7, N'Seat7.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (141, 7, N'Seat7.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (142, 7, N'Seat7.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (143, 7, N'Seat7.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (144, 7, N'Seat7.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (145, 7, N'Seat7.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (146, 7, N'Seat7.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (147, 7, N'Seat7.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (148, 7, N'Seat7.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (149, 8, N'Seat8.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (150, 8, N'Seat8.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (151, 8, N'Seat8.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (152, 8, N'Seat8.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (153, 8, N'Seat8.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (154, 8, N'Seat8.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (155, 8, N'Seat8.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (156, 8, N'Seat8.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (157, 8, N'Seat8.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (158, 8, N'Seat8.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (159, 9, N'Seat9.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (160, 9, N'Seat9.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (161, 9, N'Seat9.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (162, 9, N'Seat9.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (163, 9, N'Seat9.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (164, 9, N'Seat9.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (165, 9, N'Seat9.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (166, 9, N'Seat9.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (167, 9, N'Seat9.9')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (168, 9, N'Seat9.10')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (169, 10, N'Seat10.1')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (170, 10, N'Seat10.2')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (171, 10, N'Seat10.3')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (172, 10, N'Seat10.4')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (173, 10, N'Seat10.5')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (174, 10, N'Seat10.6')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (175, 10, N'Seat10.7')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (176, 10, N'Seat10.8')
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (177, 10, N'Seat10.9')
GO
INSERT [dbo].[Seat] ([Id], [BusId], [Code]) VALUES (178, 10, N'Seat10.10')
SET IDENTITY_INSERT [dbo].[Seat] OFF
SET IDENTITY_INSERT [dbo].[Spacing] ON 

INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (1, 1, 2, CAST(100000 AS Decimal(18, 0)), 1200, 12, 1, 1, 1, 6)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (2, 2, 3, CAST(70000 AS Decimal(18, 0)), 700, 5, 1, 1, 2, 7)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (3, 3, 4, CAST(80000 AS Decimal(18, 0)), 800, 7, 1, 1, 3, 8)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (4, 4, 5, CAST(120000 AS Decimal(18, 0)), 1300, 13, 1, 1, 4, 9)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (5, 1, 2, CAST(100000 AS Decimal(18, 0)), 1200, 6, 1, 1, 5, 6)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (6, 2, 3, CAST(70000 AS Decimal(18, 0)), 700, 5, 1, 1, 6, 7)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (7, 3, 4, CAST(80000 AS Decimal(18, 0)), 800, 12, 1, 1, 7, 8)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (8, 4, 5, CAST(120000 AS Decimal(18, 0)), 1300, 5, 1, 1, 8, 9)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (9, 1, 5, CAST(200000 AS Decimal(18, 0)), 2000, 15, 1, 1, 9, 10)
INSERT [dbo].[Spacing] ([Id], [FromSp], [ToSp], [Price], [Length], [TimeGo], [Active], [Status], [BusId], [TimeRun]) VALUES (10, 2, 5, CAST(150000 AS Decimal(18, 0)), 1700, 14, 1, 1, 10, 9)
SET IDENTITY_INSERT [dbo].[Spacing] OFF
SET IDENTITY_INSERT [dbo].[Station] ON 

INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (3, N'Huế', N'Thuận Hòa - Huế', N'0785557422', 1, 1, 1, 3)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (5, N'Hà Tĩnh', N'Hà Nam', N'0785735211', 1, 1, 2, 6)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (6, N'Nghệ An', N'Vinh', N'0668463123', 1, 1, 3, 7)
INSERT [dbo].[Station] ([Id], [Name], [Location], [Phone], [Active], [Status], [City], [District]) VALUES (7, N'Hà Nội', N'Đống Đa', N'054524966', 1, 1, 4, 4)
SET IDENTITY_INSERT [dbo].[Station] OFF
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([Id], [UserId], [BookingId], [SpacingId], [TotalPrice], [PaymentStatus], [Code]) VALUES (1, 13, 1, 1, CAST(200000 AS Decimal(18, 0)), 1, N'sdhg12')
SET IDENTITY_INSERT [dbo].[Ticket] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [Code_Seat]    Script Date: 04-Jun-20 00:11:17 ******/
ALTER TABLE [dbo].[Seat] ADD  CONSTRAINT [Code_Seat] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Account1] FOREIGN KEY([UserId2])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Account1]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Bus] FOREIGN KEY([BusId])
REFERENCES [dbo].[Bus] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Bus]
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
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Booking]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Spacing] FOREIGN KEY([SpacingId])
REFERENCES [dbo].[Spacing] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Spacing]
GO
USE [master]
GO
ALTER DATABASE [BusBooking] SET  READ_WRITE 
GO
