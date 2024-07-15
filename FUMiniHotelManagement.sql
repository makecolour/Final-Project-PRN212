USE [master]
GO
/****** Object:  Database [FUMiniHotelManagement]    Script Date: 3/30/2024 8:14:58 AM ******/
CREATE DATABASE [FUMiniHotelManagement]

USE [FUMiniHotelManagement]
GO

/****** Object:  Table [dbo].[BookingDetail]    Script Date: 3/30/2024 8:14:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetail](
	[BookingReservationID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[ActualPrice] [money] NULL,
 CONSTRAINT [PK_BookingDetail] PRIMARY KEY CLUSTERED 
(
	[BookingReservationID] ASC,
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingReservation]    Script Date: 3/30/2024 8:14:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingReservation](
	[BookingReservationID] [int] NOT NULL,
	[BookingDate] [date] NULL,
	[TotalPrice] [money] NULL,
	[CustomerID] [int] NOT NULL,
	[BookingStatus] [tinyint] NULL,
 CONSTRAINT [PK_BookingReservation] PRIMARY KEY CLUSTERED 
(
	[BookingReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 3/30/2024 8:14:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(3,1) NOT NULL,
	[CustomerFullName] [nvarchar](50) NULL,
	[Telephone] [nvarchar](12) NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[CustomerBirthday] [date] NULL,
	[CustomerStatus] [tinyint] NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomInformation]    Script Date: 3/30/2024 8:14:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomInformation](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [nvarchar](50) NOT NULL,
	[RoomDetailDescription] [nvarchar](220) NULL,
	[RoomMaxCapacity] [int] NULL,
	[RoomTypeID] [int] NOT NULL,
	[RoomStatus] [tinyint] NULL,
	[RoomPricePerDay] [money] NULL,
 CONSTRAINT [PK_RoomInformation] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 3/30/2024 8:14:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeID] [int] IDENTITY(1,1) NOT NULL,
	[RoomTypeName] [nvarchar](50) NOT NULL,
	[TypeDescription] [nvarchar](250) NULL,
	[TypeNote] [nvarchar](250) NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[RoomTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BookingDetail] ([BookingReservationID], [RoomID], [StartDate], [EndDate], [ActualPrice]) VALUES (1, 3, CAST(N'2024-01-01' AS Date), CAST(N'2024-01-02' AS Date), 199.0000)
GO
INSERT [dbo].[BookingDetail] ([BookingReservationID], [RoomID], [StartDate], [EndDate], [ActualPrice]) VALUES (1, 7, CAST(N'2024-01-01' AS Date), CAST(N'2024-01-02' AS Date), 179.0000)
GO
INSERT [dbo].[BookingDetail] ([BookingReservationID], [RoomID], [StartDate], [EndDate], [ActualPrice]) VALUES (2, 3, CAST(N'2024-01-05' AS Date), CAST(N'2024-01-06' AS Date), 199.0000)
GO
INSERT [dbo].[BookingDetail] ([BookingReservationID], [RoomID], [StartDate], [EndDate], [ActualPrice]) VALUES (2, 5, CAST(N'2024-01-05' AS Date), CAST(N'2024-01-09' AS Date), 219.0000)
GO
INSERT [dbo].[BookingReservation] ([BookingReservationID], [BookingDate], [TotalPrice], [CustomerID], [BookingStatus]) VALUES (1, CAST(N'2023-12-20' AS Date), 378.0000, 3, 1)
GO
INSERT [dbo].[BookingReservation] ([BookingReservationID], [BookingDate], [TotalPrice], [CustomerID], [BookingStatus]) VALUES (2, CAST(N'2023-12-21' AS Date), 1493.0000, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([CustomerID], [CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password]) VALUES (3, N'William Shakespeare', N'0903939393', N'WilliamShakespeare@FUMiniHotel.org', CAST(N'1990-02-02' AS Date), 1, N'123@')
GO
INSERT [dbo].[Customer] ([CustomerID], [CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password]) VALUES (5, N'Elizabeth Taylor', N'0903939377', N'ElizabethTaylor@FUMiniHotel.org', CAST(N'1991-03-03' AS Date), 1, N'144@')
GO
INSERT [dbo].[Customer] ([CustomerID], [CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password]) VALUES (8, N'James Cameron', N'0903946582', N'JamesCameron@FUMiniHotel.org', CAST(N'1992-11-10' AS Date), 1, N'443@')
GO
INSERT [dbo].[Customer] ([CustomerID], [CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password]) VALUES (9, N'Charles Dickens', N'0903955633', N'CharlesDickens@FUMiniHotel.org', CAST(N'1991-12-05' AS Date), 1, N'563@')
GO
INSERT [dbo].[Customer] ([CustomerID], [CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password]) VALUES (10, N'George Orwell', N'0913933493', N'GeorgeOrwell@FUMiniHotel.org', CAST(N'1993-12-24' AS Date), 1, N'177@')
GO
INSERT [dbo].[Customer] ([CustomerID], [CustomerFullName], [Telephone], [EmailAddress], [CustomerBirthday], [CustomerStatus], [Password]) VALUES (11, N'Victoria Beckham', N'0983246773', N'VictoriaBeckham@FUMiniHotel.org', CAST(N'1990-09-09' AS Date), 1, N'654@')
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomInformation] ON 
GO
INSERT [dbo].[RoomInformation] ([RoomID], [RoomNumber], [RoomDetailDescription], [RoomMaxCapacity], [RoomTypeID], [RoomStatus], [RoomPricePerDay]) VALUES (1, N'2364', N'A basic room with essential amenities, suitable for individual travelers or couples.', 3, 1, 1, 149.0000)
GO
INSERT [dbo].[RoomInformation] ([RoomID], [RoomNumber], [RoomDetailDescription], [RoomMaxCapacity], [RoomTypeID], [RoomStatus], [RoomPricePerDay]) VALUES (2, N'3345', N'Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.', 5, 3, 1, 299.0000)
GO
INSERT [dbo].[RoomInformation] ([RoomID], [RoomNumber], [RoomDetailDescription], [RoomMaxCapacity], [RoomTypeID], [RoomStatus], [RoomPricePerDay]) VALUES (3, N'4432', N'A luxurious and spacious room with separate living and sleeping areas, ideal for guests seeking extra comfort and space.', 4, 2, 1, 199.0000)
GO
INSERT [dbo].[RoomInformation] ([RoomID], [RoomNumber], [RoomDetailDescription], [RoomMaxCapacity], [RoomTypeID], [RoomStatus], [RoomPricePerDay]) VALUES (5, N'3342', N'Foor 3, Window in the North West ', 5, 5, 1, 219.0000)
GO
INSERT [dbo].[RoomInformation] ([RoomID], [RoomNumber], [RoomDetailDescription], [RoomMaxCapacity], [RoomTypeID], [RoomStatus], [RoomPricePerDay]) VALUES (7, N'4434', N'Floor 4, main window in the South ', 4, 1, 1, 179.0000)
GO
SET IDENTITY_INSERT [dbo].[RoomInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomType] ON 
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (1, N'Standard room', N'This is typically the most affordable option and provides basic amenities such as a bed, dresser, and TV.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (2, N'Suite', N'Suites usually offer more space and amenities than standard rooms, such as a separate living area, kitchenette, and multiple bathrooms.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (3, N'Deluxe room', N'Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (4, N'Executive room', N'Executive rooms are designed for business travelers and offer perks such as free breakfast, evening drink, and high-speed internet.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (5, N'Family Room', N'A room specifically designed to accommodate families, often with multiple beds and additional space for children.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (6, N'Connecting Room', N'Two or more rooms with a connecting door, providing flexibility for larger groups or families traveling together.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (7, N'Penthouse Suite', N'An extravagant, top-floor suite with exceptional views and exclusive amenities, typically chosen for special occasions or VIP guests.', N'N/A')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName], [TypeDescription], [TypeNote]) VALUES (8, N'Bungalow', N'A standalone cottage-style accommodation, providing privacy and a sense of seclusion often within a resort setting', N'N/A')
GO
SET IDENTITY_INSERT [dbo].[RoomType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__49A147405153121E]    Script Date: 3/30/2024 8:14:58 AM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookingDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetail_BookingReservation] FOREIGN KEY([BookingReservationID])
REFERENCES [dbo].[BookingReservation] ([BookingReservationID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingDetail] CHECK CONSTRAINT [FK_BookingDetail_BookingReservation]
GO
ALTER TABLE [dbo].[BookingDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetail_RoomInformation] FOREIGN KEY([RoomID])
REFERENCES [dbo].[RoomInformation] ([RoomID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingDetail] CHECK CONSTRAINT [FK_BookingDetail_RoomInformation]
GO
ALTER TABLE [dbo].[BookingReservation]  WITH CHECK ADD  CONSTRAINT [FK_BookingReservation_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingReservation] CHECK CONSTRAINT [FK_BookingReservation_Customer]
GO
ALTER TABLE [dbo].[RoomInformation]  WITH CHECK ADD  CONSTRAINT [FK_RoomInformation_RoomType] FOREIGN KEY([RoomTypeID])
REFERENCES [dbo].[RoomType] ([RoomTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomInformation] CHECK CONSTRAINT [FK_RoomInformation_RoomType]
GO
USE [master]
GO
ALTER DATABASE [FUMiniHotelManagement] SET  READ_WRITE 
GO
