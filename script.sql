USE [master]
GO
/****** Object:  Database [BookStore]    Script Date: 2024-04-30 15:00:19 ******/
CREATE DATABASE [BookStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BookStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BookStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BookStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BookStore_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BookStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BookStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookStore] SET RECOVERY FULL 
GO
ALTER DATABASE [BookStore] SET  MULTI_USER 
GO
ALTER DATABASE [BookStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BookStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BookStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BookStore', N'ON'
GO
ALTER DATABASE [BookStore] SET QUERY_STORE = OFF
GO
USE [BookStore]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorId] [int] NOT NULL,
	[Firstname] [nvarchar](30) NULL,
	[Lastname] [nvarchar](30) NULL,
	[Date_Of_Birth] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock_Balance]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock_Balance](
	[StoreId] [int] NOT NULL,
	[ISBN] [bigint] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC,
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book_Specifications]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Specifications](
	[ISBN] [bigint] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[IllustratorId] [int] NOT NULL,
	[SeriesId] [int] NOT NULL,
	[Publication_Date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC,
	[AuthorId] ASC,
	[IllustratorId] ASC,
	[SeriesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vTitles_Per_Author]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vTitles_Per_Author]
AS
SELECT  DISTINCT CONCAT(Firstname, ' ' ,Lastname) AS Name,
		DATEDIFF(year, Date_Of_Birth ,GETDATE()) AS Age,
		COUNT(Stock_Balance.ISBN) AS Books,
		SUM(Stock_Balance.Quantity) AS Total_Inventory
FROM Authors
		JOIN Book_Specifications ON Book_Specifications.AuthorId = Authors.AuthorId
		JOIN Stock_Balance ON Stock_Balance.ISBN = Book_Specifications.ISBN
GROUP BY Authors.Firstname, Authors.Lastname, Authors.Date_Of_Birth
GO
/****** Object:  Table [dbo].[Books]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[ISBN] [bigint] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Language] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Details]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Details](
	[OrderId] [int] NOT NULL,
	[Items] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[Items] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vSold_Per_Book]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vSold_Per_Book]
AS
SELECT DISTINCT Books.Name AS Name_Of_The_Book, COUNT(Items) AS Amount_Sold, Books.Language AS Language
  FROM [Order_Details]
		JOIN Books ON Books.ISBN = Order_Details.Items
  GROUP BY Books.Name, Books.Language
GO
/****** Object:  Table [dbo].[Demographics]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Demographics](
	[DemographicId] [int] NOT NULL,
	[Name] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[DemographicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Illustrators]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Illustrators](
	[IllustratorId] [int] NOT NULL,
	[Firstname] [nvarchar](30) NULL,
	[Lastname] [nvarchar](30) NULL,
	[Date_Of_Birth] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IllustratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MemberId] [int] NOT NULL,
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] NOT NULL,
	[MemberId] [int] NULL,
	[Order_Date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Price_Groups]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price_Groups](
	[Price_GroupId] [int] NOT NULL,
	[Price] [int] NULL,
	[Currency] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Price_GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Series]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Series](
	[SeriesId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Demographic] [int] NULL,
	[Price_GroupId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SeriesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Series_Specifications]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Series_Specifications](
	[SeriesId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[IllustratorId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Release_Date] [date] NULL,
	[Number_Of_Books] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SeriesId] ASC,
	[AuthorId] ASC,
	[IllustratorId] ASC,
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[StoreId] [int] NOT NULL,
	[Name] [nvarchar](30) NULL,
	[Streetname] [nvarchar](30) NULL,
	[Building_Number] [int] NULL,
	[Postal_Code] [int] NULL,
	[City] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierId] [int] NOT NULL,
	[Name] [nvarchar](30) NULL,
	[Contact_Number] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book_Specifications]  WITH CHECK ADD FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([AuthorId])
GO
ALTER TABLE [dbo].[Book_Specifications]  WITH CHECK ADD FOREIGN KEY([IllustratorId])
REFERENCES [dbo].[Illustrators] ([IllustratorId])
GO
ALTER TABLE [dbo].[Book_Specifications]  WITH CHECK ADD FOREIGN KEY([SeriesId])
REFERENCES [dbo].[Series] ([SeriesId])
GO
ALTER TABLE [dbo].[Book_Specifications]  WITH CHECK ADD FOREIGN KEY([ISBN])
REFERENCES [dbo].[Books] ([ISBN])
GO
ALTER TABLE [dbo].[Order_Details]  WITH CHECK ADD FOREIGN KEY([Items])
REFERENCES [dbo].[Books] ([ISBN])
GO
ALTER TABLE [dbo].[Order_Details]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([MemberId])
GO
ALTER TABLE [dbo].[Series]  WITH CHECK ADD FOREIGN KEY([Demographic])
REFERENCES [dbo].[Demographics] ([DemographicId])
GO
ALTER TABLE [dbo].[Series]  WITH CHECK ADD FOREIGN KEY([Price_GroupId])
REFERENCES [dbo].[Price_Groups] ([Price_GroupId])
GO
ALTER TABLE [dbo].[Series_Specifications]  WITH CHECK ADD FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([AuthorId])
GO
ALTER TABLE [dbo].[Series_Specifications]  WITH CHECK ADD FOREIGN KEY([IllustratorId])
REFERENCES [dbo].[Illustrators] ([IllustratorId])
GO
ALTER TABLE [dbo].[Series_Specifications]  WITH CHECK ADD FOREIGN KEY([SeriesId])
REFERENCES [dbo].[Series] ([SeriesId])
GO
ALTER TABLE [dbo].[Series_Specifications]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
GO
ALTER TABLE [dbo].[Stock_Balance]  WITH CHECK ADD FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Stock_Balance]  WITH CHECK ADD FOREIGN KEY([ISBN])
REFERENCES [dbo].[Books] ([ISBN])
GO
/****** Object:  StoredProcedure [dbo].[BookTransfer]    Script Date: 2024-04-30 15:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookTransfer]   
    @ISBN BIGINT, 
    @FromStore INT,
    @ToStore INT, 
    @Quantity INT
AS
BEGIN
   BEGIN TRY
        BEGIN TRANSACTION;

        IF (SELECT Quantity 
            FROM Stock_Balance 
            WHERE ISBN = @ISBN AND StoreId = @FromStore) >= @Quantity
        BEGIN
            UPDATE Stock_Balance
            SET Quantity = Quantity - @Quantity
            WHERE ISBN = @ISBN AND Storeid = @FromStore;
        
            IF EXISTS (SELECT 1 FROM Stock_Balance WHERE ISBN = @ISBN AND StoreId = @ToStore)
            BEGIN
                UPDATE Stock_Balance
                SET Quantity = Quantity + @Quantity
                WHERE ISBN = @ISBN AND Storeid = @ToStore;
            END
            ELSE
            BEGIN
                INSERT INTO Stock_Balance(ISBN, StoreId, Quantity)
                VALUES (@ISBN, @ToStore, @Quantity);
            END;

            PRINT 'Book transfer successful.';
        END
        ELSE
        BEGIN 
            PRINT 'Unsuccessful transfer';
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'An error occurred. Transaction rolled back.';
    END CATCH;
END;
GO
USE [master]
GO
ALTER DATABASE [BookStore] SET  READ_WRITE 
GO
