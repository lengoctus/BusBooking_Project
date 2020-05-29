USE [master]
GO
/****** Object:  Database [BusBooking]    Script Date: 29-May-20 10:23:12 ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NOT NULL,
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
	[StationId] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Age]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Age](
	[Id] [int] NOT NULL,
	[FromAge] [int] NULL,
	[FromTo] [int] NULL,
	[Percent] [tinyint] NULL,
 CONSTRAINT [PK_Age] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Booking]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] NOT NULL,
	[DayCreate] [datetime] NULL,
	[DayStart] [datetime] NULL,
	[DayEnd] [int] NULL,
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
/****** Object:  Table [dbo].[Bus]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bus](
	[Id] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Active] [bit] NULL,
	[Status] [bit] NULL,
	[Image] [varchar](50) NULL,
	[TotalSeat] [int] NULL,
	[SeatEmpty] [int] NULL,
	[CateId] [int] NULL,
 CONSTRAINT [PK_Bus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[Status] [bit] NULL,
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Seat]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seat](
	[Id] [int] NOT NULL,
	[BusId] [int] NULL,
 CONSTRAINT [PK_Seat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Spacing]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spacing](
	[Id] [int] NOT NULL,
	[FromSp] [int] NULL,
	[ToSp] [int] NULL,
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Spacing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Station]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Station](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 29-May-20 10:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [int] NOT NULL,
	[UserId] [int] NULL,
	[BookingId] [int] NULL,
	[SpacingId] [int] NULL,
	[AgeId] [int] NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[PaymentStatus] [bit] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Email], [Password], [Name], [DOB], [Address], [Phone], [Images], [Gender], [DayCreate], [DayEdited], [EditerId], [Status], [RoleId], [Active], [Description], [ForgotPass], [StationId]) VALUES (1, N'ngoctu', N'123', NULL, NULL, NULL, N'ahi', NULL, 1, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
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
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Age] FOREIGN KEY([AgeId])
REFERENCES [dbo].[Age] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Age]
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
