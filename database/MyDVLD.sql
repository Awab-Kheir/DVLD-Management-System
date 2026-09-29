USE [master]
GO
/****** Object:  Database [MyDVLD]    Script Date: 9/29/2026 7:04:40 AM ******/
CREATE DATABASE [MyDVLD];
GO
ALTER DATABASE [MyDVLD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyDVLD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyDVLD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyDVLD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyDVLD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyDVLD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyDVLD] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyDVLD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyDVLD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyDVLD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyDVLD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyDVLD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyDVLD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyDVLD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyDVLD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyDVLD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyDVLD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyDVLD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyDVLD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyDVLD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyDVLD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyDVLD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyDVLD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyDVLD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyDVLD] SET RECOVERY FULL 
GO
ALTER DATABASE [MyDVLD] SET  MULTI_USER 
GO
ALTER DATABASE [MyDVLD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyDVLD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyDVLD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyDVLD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyDVLD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyDVLD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyDVLD', N'ON'
GO
ALTER DATABASE [MyDVLD] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyDVLD] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyDVLD]
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[LicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[LicenseClassID] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IssueReason] [tinyint] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[LicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[NationalNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[SecondName] [nvarchar](20) NOT NULL,
	[ThirdName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Gendor] [tinyint] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[NationalityCountryID] [int] NOT NULL,
	[ImagePath] [nvarchar](250) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Drivers_View]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[Drivers_View] as
SELECT   Drivers.DriverID, Drivers.PersonID, People.NationalNo,
         People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName AS FullName,
         Drivers.CreatedDate, 
         (SELECT COUNT(LicenseID) AS NumberOfActiveLicenses
          FROM Licenses
          WHERE (IsActive = 1) AND (DriverID = Drivers.DriverID)) AS NumberOfActiveLicenses 
FROM         Drivers INNER JOIN
                         People ON Drivers.PersonID = People.PersonID


GO
/****** Object:  Table [dbo].[Applications]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationPersonID] [int] NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[ApplicationTypeID] [int] NOT NULL,
	[ApplicationStatus] [tinyint] NOT NULL,
	[LastStatusDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalDrivingLicenseApplications]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalDrivingLicenseApplications](
	[LocalDrivingLicenseApplicationID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[LicenseClassID] [int] NOT NULL,
 CONSTRAINT [PK_LocalDrivingLicenseApplicationID] PRIMARY KEY CLUSTERED 
(
	[LocalDrivingLicenseApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseClasses]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseClasses](
	[LicenseClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ClassDescription] [nvarchar](500) NOT NULL,
	[MinimumAllowedAge] [tinyint] NOT NULL,
	[DefaultValidityLength] [tinyint] NOT NULL,
	[ClassFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED 
(
	[LicenseClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestAppointments]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAppointments](
	[TestAppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeID] [int] NOT NULL,
	[LocalDrivingLicenseApplicationID] [int] NOT NULL,
	[AppointmentDate] [smalldatetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[RetakeTestApplicationID] [int] NULL,
 CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED 
(
	[TestAppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTypes](
	[TestTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeTitle] [nvarchar](100) NOT NULL,
	[TestTypeDescription] [nvarchar](500) NOT NULL,
	[TestTypeFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_ApplicationTests] PRIMARY KEY CLUSTERED 
(
	[TestTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TestAppointments_View]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create view [dbo].[TestAppointments_View] as
SELECT   TestAppointments.TestAppointmentID, TestAppointments.LocalDrivingLicenseApplicationID, TestTypes.TestTypeTitle, LicenseClasses.ClassName, TestAppointments.AppointmentDate, 
                         TestAppointments.PaidFees, People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName AS FullName,TestAppointments.IsLocked
FROM         TestAppointments INNER JOIN
                         TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID INNER JOIN
                         LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID INNER JOIN
                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN
                         People ON Applications.ApplicationPersonID = People.PersonID INNER JOIN
                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 







GO
/****** Object:  Table [dbo].[Tests]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[TestAppointmentID] [int] NOT NULL,
	[TestResult] [bit] NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LocalDrivingLicenseApplications_View]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[LocalDrivingLicenseApplications_View] As
SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, LicenseClasses.ClassName, People.NationalNo,
                        FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName,
                        Applications.ApplicationDate, 
                        (SELECT COUNT(TestAppointments.TestTypeID)
                        FROM Tests INNER JOIN
                                TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            WHERE (TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AND (Tests.TestResult = 1)) AS PassedTestCount,
                        CASE 
                            WHEN Applications.ApplicationStatus = 1 THEN 'New'
                            WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled'
                            WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
                            END AS Status                           
                        FROM     LocalDrivingLicenseApplications INNER JOIN
                        Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN
                        People ON Applications.ApplicationPersonID = People.PersonID INNER JOIN
                        LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
GO
/****** Object:  Table [dbo].[DetainedLicenses]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetainedLicenses](
	[DetainID] [int] IDENTITY(1,1) NOT NULL,
	[LicenseID] [int] NOT NULL,
	[DetainDate] [smalldatetime] NOT NULL,
	[FineFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsReleased] [bit] NOT NULL,
	[ReleaseDate] [smalldatetime] NULL,
	[ReleasedByUserID] [int] NULL,
	[ReleaseApplicationID] [int] NULL,
 CONSTRAINT [PK_DetainedLicenses] PRIMARY KEY CLUSTERED 
(
	[DetainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DetainedLicenses_View]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[DetainedLicenses_View] as
SELECT   DetainedLicenses.DetainID, DetainedLicenses.LicenseID, DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, 
                         People.NationalNo, People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, ' ') + ' ' + People.LastName AS FullName, DetainedLicenses.ReleaseApplicationID
FROM         DetainedLicenses INNER JOIN
                         Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN
                         Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                         People ON Drivers.PersonID = People.PersonID



GO
/****** Object:  Table [dbo].[ApplicationTypes]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationTypes](
	[ApplicationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeTitle] [nvarchar](150) NOT NULL,
	[ApplicationFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_ApplicationTypes] PRIMARY KEY CLUSTERED 
(
	[ApplicationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternationalLicenses]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternationalLicenses](
	[InternationalLicenseID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[IssuedUsingLocalLicenseID] [int] NOT NULL,
	[IssueDate] [smalldatetime] NOT NULL,
	[ExpirationDate] [smalldatetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
 CONSTRAINT [PK__Internat__40B3EFD4C632BCE7] PRIMARY KEY CLUSTERED 
(
	[InternationalLicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/29/2026 7:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Applications] ON 

INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (3, 19, CAST(N'2026-09-16T20:45:07.753' AS DateTime), 1, 3, CAST(N'2026-09-17T04:12:51.753' AS DateTime), 20.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (6, 11, CAST(N'2026-06-11T13:59:29.733' AS DateTime), 1, 1, CAST(N'2026-06-11T13:59:29.733' AS DateTime), 20.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (7, 11, CAST(N'2026-09-01T13:46:29.830' AS DateTime), 1, 1, CAST(N'2026-09-01T13:46:29.830' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (8, 12, CAST(N'2026-09-01T13:55:22.667' AS DateTime), 1, 3, CAST(N'2026-09-17T03:55:56.940' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (9, 12, CAST(N'2026-09-13T17:09:07.397' AS DateTime), 1, 3, CAST(N'2026-09-19T15:40:49.910' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (10, 12, CAST(N'2026-09-14T23:00:52.093' AS DateTime), 7, 3, CAST(N'2026-09-14T23:00:52.093' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (11, 12, CAST(N'2026-09-16T20:28:18.433' AS DateTime), 7, 3, CAST(N'2026-09-16T20:28:18.433' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (12, 12, CAST(N'2026-09-16T20:28:59.087' AS DateTime), 7, 3, CAST(N'2026-09-16T20:28:59.087' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (13, 12, CAST(N'2026-09-19T15:33:47.080' AS DateTime), 1, 3, CAST(N'2026-09-19T15:35:47.780' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (18, 19, CAST(N'2026-09-23T09:06:56.053' AS DateTime), 6, 3, CAST(N'2026-09-23T09:06:56.053' AS DateTime), 51.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (21, 19, CAST(N'2026-09-25T18:33:29.447' AS DateTime), 2, 3, CAST(N'2026-09-25T18:33:29.447' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (22, 12, CAST(N'2026-09-25T18:49:26.827' AS DateTime), 2, 3, CAST(N'2026-09-25T18:49:26.827' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (23, 19, CAST(N'2026-09-25T20:55:43.423' AS DateTime), 4, 3, CAST(N'2026-09-25T20:55:43.423' AS DateTime), 5.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (24, 19, CAST(N'2026-09-25T20:59:51.197' AS DateTime), 3, 3, CAST(N'2026-09-25T20:59:51.197' AS DateTime), 10.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (29, 19, CAST(N'2026-09-27T15:34:37.470' AS DateTime), 5, 3, CAST(N'2026-09-27T15:34:37.470' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (30, 19, CAST(N'2026-09-27T20:12:06.043' AS DateTime), 5, 3, CAST(N'2026-09-27T20:12:06.043' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (31, 19, CAST(N'2026-09-27T20:17:59.247' AS DateTime), 5, 3, CAST(N'2026-09-27T20:17:59.247' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (32, 19, CAST(N'2026-09-27T20:18:49.240' AS DateTime), 5, 3, CAST(N'2026-09-27T20:18:49.240' AS DateTime), 15.0000, 1)
INSERT [dbo].[Applications] ([ApplicationID], [ApplicationPersonID], [ApplicationDate], [ApplicationTypeID], [ApplicationStatus], [LastStatusDate], [PaidFees], [CreatedByUserID]) VALUES (33, 19, CAST(N'2026-09-27T20:19:35.900' AS DateTime), 5, 3, CAST(N'2026-09-27T20:19:35.900' AS DateTime), 15.0000, 1)
SET IDENTITY_INSERT [dbo].[Applications] OFF
GO
SET IDENTITY_INSERT [dbo].[ApplicationTypes] ON 

INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (1, N'New Local Driving License Service', 15.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (2, N'Renew Driving License Service', 5.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (3, N'Replacement for a Lost Driving License', 10.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (4, N'Replacement fro a Damaged Driving License', 5.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (5, N'Release Detained Driving License', 15.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (6, N'New International License', 51.0000)
INSERT [dbo].[ApplicationTypes] ([ApplicationTypeID], [ApplicationTypeTitle], [ApplicationFees]) VALUES (7, N'Retake Test', 5.0000)
SET IDENTITY_INSERT [dbo].[ApplicationTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (1, N'Afghanistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (2, N'Albania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (3, N'Algeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (4, N'Andorra')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (5, N'Angola')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (6, N'Antigua and Barbuda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (7, N'Argentina')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (8, N'Armenia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (9, N'Austria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (10, N'Azerbaijan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (11, N'Bahrain')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (12, N'Bangladesh')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (13, N'Barbados')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (14, N'Belarus')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (15, N'Belgium')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (16, N'Belize')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (17, N'Benin')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (18, N'Bhutan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (19, N'Bolivia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (20, N'Bosnia and Herzegovina')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (21, N'Botswana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (22, N'Brazil')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (23, N'Brunei')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (24, N'Bulgaria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (25, N'Burkina Faso')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (26, N'Burundi')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (27, N'Cabo Verde')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (28, N'Cambodia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (29, N'Cameroon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (30, N'Canada')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (31, N'Central African Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (32, N'Chad')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (33, N'Channel Islands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (34, N'Chile')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (35, N'China')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (36, N'Colombia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (37, N'Comoros')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (38, N'Congo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (39, N'Costa Rica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (40, N'Côte dIvoire')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (41, N'Croatia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (42, N'Cuba')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (43, N'Cyprus')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (44, N'Czech Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (45, N'Denmark')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (46, N'Djibouti')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (47, N'Dominica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (48, N'Dominican Republic')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (49, N'DR Congo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (50, N'Ecuador')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (51, N'Egypt')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (52, N'El Salvador')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (53, N'Equatorial Guinea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (54, N'Eritrea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (55, N'Estonia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (56, N'Eswatini')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (57, N'Ethiopia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (58, N'Faeroe Islands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (59, N'Finland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (60, N'France')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (61, N'French Guiana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (62, N'Gabon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (63, N'Gambia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (64, N'Georgia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (65, N'Germany')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (66, N'Ghana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (67, N'Gibraltar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (68, N'Greece')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (69, N'Grenada')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (70, N'Guatemala')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (71, N'Guinea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (72, N'Guinea-Bissau')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (73, N'Guyana')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (74, N'Haiti')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (75, N'Holy See')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (76, N'Honduras')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (77, N'Hong Kong')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (78, N'Hungary')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (79, N'Iceland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (80, N'India')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (81, N'Indonesia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (82, N'Iran')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (83, N'Iraq')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (84, N'Ireland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (85, N'Isle of Man')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (86, N'Israel')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (87, N'Italy')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (88, N'Jamaica')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (89, N'Japan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (90, N'Jordan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (91, N'Kazakhstan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (92, N'Kenya')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (93, N'Kuwait')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (94, N'Kyrgyzstan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (95, N'Laos')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (96, N'Latvia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (97, N'Lebanon')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (98, N'Lesotho')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (99, N'Liberia')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (100, N'Libya')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (101, N'Liechtenstein')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (102, N'Lithuania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (103, N'Luxembourg')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (104, N'Macao')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (105, N'Madagascar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (106, N'Malawi')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (107, N'Malaysia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (108, N'Maldives')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (109, N'Mali')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (110, N'Malta')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (111, N'Mauritania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (112, N'Mauritius')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (113, N'Mayotte')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (114, N'Mexico')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (115, N'Moldova')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (116, N'Monaco')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (117, N'Mongolia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (118, N'Montenegro')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (119, N'Morocco')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (120, N'Mozambique')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (121, N'Myanmar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (122, N'Namibia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (123, N'Nepal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (124, N'Netherlands')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (125, N'Nicaragua')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (126, N'Niger')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (127, N'Nigeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (128, N'North Korea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (129, N'North Macedonia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (130, N'Norway')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (131, N'Oman')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (132, N'Pakistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (133, N'Panama')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (134, N'Paraguay')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (135, N'Peru')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (136, N'Philippines')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (137, N'Poland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (138, N'Portugal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (139, N'Qatar')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (140, N'Réunion')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (141, N'Romania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (142, N'Russia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (143, N'Rwanda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (144, N'Saint Helena')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (145, N'Saint Kitts and Nevis')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (146, N'Saint Lucia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (147, N'Saint Vincent and the Grenadines')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (148, N'San Marino')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (149, N'Sao Tome & Principe')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (150, N'Saudi Arabia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (151, N'Senegal')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (152, N'Serbia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (153, N'Seychelles')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (154, N'Sierra Leone')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (155, N'Singapore')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (156, N'Slovakia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (157, N'Slovenia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (158, N'Somalia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (159, N'South Africa')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (160, N'South Korea')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (161, N'South Sudan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (162, N'Spain')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (163, N'Sri Lanka')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (164, N'State of Palestine')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (165, N'Sudan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (166, N'Suriname')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (167, N'Sweden')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (168, N'Switzerland')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (169, N'Syria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (170, N'Taiwan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (171, N'Tajikistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (172, N'Tanzania')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (173, N'Thailand')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (174, N'The Bahamas')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (175, N'Timor-Leste')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (176, N'Togo')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (177, N'Trinidad and Tobago')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (178, N'Tunisia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (179, N'Turkey')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (180, N'Turkmenistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (181, N'Uganda')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (182, N'Ukraine')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (183, N'United Arab Emirates')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (184, N'United Kingdom')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (185, N'United States')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (186, N'Uruguay')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (187, N'Uzbekistan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (188, N'Venezuela')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (189, N'Vietnam')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (190, N'Western Sahara')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (191, N'Yemen')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (192, N'Zambia')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (193, N'Zimbabwe')
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[DetainedLicenses] ON 

INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (3, 14, CAST(N'2026-09-27T08:47:00' AS SmallDateTime), 15.0000, 1, 1, CAST(N'2026-09-27T15:35:00' AS SmallDateTime), 1, 29)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (4, 14, CAST(N'2026-09-27T20:12:00' AS SmallDateTime), 160.0000, 1, 1, CAST(N'2026-09-27T20:12:00' AS SmallDateTime), 1, 30)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (5, 14, CAST(N'2026-09-27T20:18:00' AS SmallDateTime), 170.0000, 1, 1, CAST(N'2026-09-27T20:18:00' AS SmallDateTime), 1, 31)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (6, 14, CAST(N'2026-09-27T20:19:00' AS SmallDateTime), 180.0000, 1, 1, CAST(N'2026-09-27T20:19:00' AS SmallDateTime), 1, 32)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (7, 14, CAST(N'2026-09-27T20:19:00' AS SmallDateTime), 200.0000, 1, 1, CAST(N'2026-09-27T20:20:00' AS SmallDateTime), 1, 33)
INSERT [dbo].[DetainedLicenses] ([DetainID], [LicenseID], [DetainDate], [FineFees], [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) VALUES (8, 14, CAST(N'2026-09-27T20:51:00' AS SmallDateTime), 222.0000, 1, 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DetainedLicenses] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 

INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (3, 16, 1, CAST(N'2026-09-17T00:31:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (8, 12, 1, CAST(N'2026-09-17T03:56:00' AS SmallDateTime))
INSERT [dbo].[Drivers] ([DriverID], [PersonID], [CreatedByUserID], [CreatedDate]) VALUES (9, 19, 1, CAST(N'2026-09-17T04:13:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[InternationalLicenses] ON 

INSERT [dbo].[InternationalLicenses] ([InternationalLicenseID], [ApplicationID], [DriverID], [IssuedUsingLocalLicenseID], [IssueDate], [ExpirationDate], [IsActive], [CreatedByUserID]) VALUES (1, 18, 9, 14, CAST(N'2026-09-23T09:07:00' AS SmallDateTime), CAST(N'2036-09-23T09:07:00' AS SmallDateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[InternationalLicenses] OFF
GO
SET IDENTITY_INSERT [dbo].[LicenseClasses] ON 

INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (1, N'Class 1 - Small Motorcycle', N'It allows the driver to drive small motorcycles, It is suitable for motorcycles with small capacity and limited power.', 18, 5, 15.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (2, N'Class 2 - Heavy Motorcycle License', N'Heavy Motorcycle License (Large Motorcycle License)', 21, 5, 30.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (3, N'Class 3 - Ordinary driving License', N'Ordinary driving license (car licence)', 18, 10, 20.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (4, N'Class 4 - Commercial', N'Commercial driving license (taxi/limousine)', 21, 10, 200.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (5, N'Class 5 - Agricultural', N'Agricultural and work vehicles used in farming or construction, (tractors / tillage machinery)', 21, 10, 50.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (6, N'Class 6 - Small and medium bus', N'Small and medium bus license', 21, 10, 250.0000)
INSERT [dbo].[LicenseClasses] ([LicenseClassID], [ClassName], [ClassDescription], [MinimumAllowedAge], [DefaultValidityLength], [ClassFees]) VALUES (7, N'Class 7 - Truck and heavy vehicle', N'Truck and heavy vehicle license', 21, 10, 300.0000)
SET IDENTITY_INSERT [dbo].[LicenseClasses] OFF
GO
SET IDENTITY_INSERT [dbo].[Licenses] ON 

INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (13, 8, 8, 3, CAST(N'2026-09-17T03:55:56.940' AS DateTime), CAST(N'2036-09-17T03:55:56.940' AS DateTime), NULL, 20.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (14, 3, 9, 3, CAST(N'2026-09-17T04:12:51.753' AS DateTime), CAST(N'2026-09-17T04:12:51.753' AS DateTime), NULL, 20.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (16, 13, 8, 4, CAST(N'2026-09-19T15:35:47.763' AS DateTime), CAST(N'2036-09-19T15:35:47.763' AS DateTime), NULL, 200.0000, 1, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (17, 9, 8, 5, CAST(N'2026-09-19T15:40:49.910' AS DateTime), CAST(N'2026-09-19T15:40:49.910' AS DateTime), NULL, 50.0000, 0, 1, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (22, 21, 9, 3, CAST(N'2026-09-25T18:33:29.447' AS DateTime), CAST(N'2036-09-25T18:33:29.447' AS DateTime), NULL, 20.0000, 0, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (23, 22, 8, 5, CAST(N'2026-09-25T18:49:26.853' AS DateTime), CAST(N'2036-09-25T18:49:26.853' AS DateTime), NULL, 50.0000, 1, 2, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (24, 23, 9, 3, CAST(N'2026-09-25T20:55:43.423' AS DateTime), CAST(N'2036-09-25T20:55:43.423' AS DateTime), NULL, 20.0000, 0, 3, 1)
INSERT [dbo].[Licenses] ([LicenseID], [ApplicationID], [DriverID], [LicenseClassID], [IssueDate], [ExpirationDate], [Notes], [PaidFees], [IsActive], [IssueReason], [CreatedByUserID]) VALUES (25, 24, 9, 3, CAST(N'2026-09-25T20:59:51.197' AS DateTime), CAST(N'2036-09-25T20:59:51.197' AS DateTime), NULL, 20.0000, 1, 4, 1)
SET IDENTITY_INSERT [dbo].[Licenses] OFF
GO
SET IDENTITY_INSERT [dbo].[LocalDrivingLicenseApplications] ON 

INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (1, 3, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (4, 6, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (5, 7, 7)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (6, 8, 3)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (7, 9, 5)
INSERT [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID], [ApplicationID], [LicenseClassID]) VALUES (8, 13, 4)
SET IDENTITY_INSERT [dbo].[LocalDrivingLicenseApplications] OFF
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (4, N'DEMO-4', N'Demo', N'Person', N'Test', N'User4', CAST(N'2001-07-27T00:00:00.000' AS DateTime), 0, N'Demo Address', N'0000004', N'demo4@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (6, N'DEMO-6', N'Demo', N'Person', N'Test', N'User6', CAST(N'2000-07-27T00:00:00.000' AS DateTime), 0, N'Demo Address', N'0000006', N'demo6@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (7, N'DEMO-7', N'Demo', N'Person', N'Test', N'User7', CAST(N'2003-07-27T00:00:00.000' AS DateTime), 0, N'Demo Address', N'0000007', N'demo7@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (11, N'DEMO-11', N'Demo', N'Person', N'Test', N'User11', CAST(N'2008-03-31T14:41:10.327' AS DateTime), 0, N'Demo Address', N'00000011', N'demo11@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (12, N'DEMO-12', N'Demo', N'Person', N'Test', N'User12', CAST(N'2008-03-31T18:53:24.667' AS DateTime), 0, N'Demo Address', N'00000012', N'demo12@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (13, N'DEMO-13', N'Demo', N'Person', N'Test', N'User13', CAST(N'2008-04-02T10:54:13.810' AS DateTime), 0, N'Demo Address', N'00000013', N'demo13@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (14, N'DEMO-14', N'Demo', N'Person', N'Test', N'User14', CAST(N'2008-04-05T13:37:02.157' AS DateTime), 0, N'Demo Address', N'00000014', N'demo14@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (15, N'DEMO-15', N'Demo', N'Person', N'Test', N'User15', CAST(N'2008-04-05T13:54:37.577' AS DateTime), 0, N'Demo Address', N'00000015', N'demo15@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (16, N'DEMO-16', N'Demo', N'Person', N'Test', N'User16', CAST(N'2008-04-13T18:53:12.827' AS DateTime), 0, N'Demo Address', N'00000016', N'demo16@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (17, N'DEMO-17', N'Demo', N'Person', N'Test', N'User17', CAST(N'2008-05-21T07:19:43.427' AS DateTime), 0, N'Demo Address', N'00000017', N'demo17@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (18, N'DEMO-18', N'Demo', N'Person', N'Test', N'User18', CAST(N'2008-05-21T11:49:06.500' AS DateTime), 0, N'Demo Address', N'00000018', N'demo18@example.com', 169, NULL)
INSERT [dbo].[People] ([PersonID], [NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath]) VALUES (19, N'DEMO-19', N'Demo', N'Person', N'Test', N'User19', CAST(N'2008-05-21T16:40:18.550' AS DateTime), 0, N'Demo Address', N'00000019', N'demo19@example.com', 169, NULL)
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[TestAppointments] ON 

INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (3, 1, 1, CAST(N'2026-06-04T18:18:00' AS SmallDateTime), 20.0000, 1, 0, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (7, 2, 1, CAST(N'2026-06-04T18:20:00' AS SmallDateTime), 20.0000, 1, 0, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (8, 3, 4, CAST(N'2026-06-04T18:20:00' AS SmallDateTime), 20.0000, 1, 0, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (10, 1, 5, CAST(N'2026-09-08T22:18:00' AS SmallDateTime), 10.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (13, 1, 6, CAST(N'2026-09-02T22:51:00' AS SmallDateTime), 10.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (18, 1, 7, CAST(N'2026-09-14T20:41:00' AS SmallDateTime), 10.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (20, 1, 6, CAST(N'2026-09-14T23:01:00' AS SmallDateTime), 10.0000, 1, 1, 10)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (21, 2, 6, CAST(N'2026-09-17T20:25:00' AS SmallDateTime), 20.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (22, 3, 6, CAST(N'2026-09-17T20:28:00' AS SmallDateTime), 30.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (23, 3, 6, CAST(N'2026-09-17T20:28:00' AS SmallDateTime), 30.0000, 1, 1, 11)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (24, 3, 6, CAST(N'2026-09-18T20:29:00' AS SmallDateTime), 30.0000, 1, 1, 12)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (25, 2, 5, CAST(N'2026-09-16T23:20:00' AS SmallDateTime), 20.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (26, 3, 1, CAST(N'2026-09-17T04:07:00' AS SmallDateTime), 30.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (27, 2, 7, CAST(N'2026-09-19T15:27:00' AS SmallDateTime), 20.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (28, 3, 7, CAST(N'2026-09-19T15:27:00' AS SmallDateTime), 30.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (29, 1, 8, CAST(N'2026-09-19T15:34:00' AS SmallDateTime), 10.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (30, 2, 8, CAST(N'2026-09-19T15:34:00' AS SmallDateTime), 20.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (31, 3, 8, CAST(N'2026-09-19T15:34:00' AS SmallDateTime), 30.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (32, 1, 4, CAST(N'2026-09-21T02:12:00' AS SmallDateTime), 10.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (33, 2, 4, CAST(N'2026-09-21T02:48:00' AS SmallDateTime), 20.0000, 1, 1, NULL)
INSERT [dbo].[TestAppointments] ([TestAppointmentID], [TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees], [CreatedByUserID], [IsLocked], [RetakeTestApplicationID]) VALUES (34, 3, 5, CAST(N'2026-09-21T04:11:00' AS SmallDateTime), 30.0000, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[TestAppointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1, 3, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (2, 7, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (3, 8, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (6, 10, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1007, 13, 0, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1008, 20, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1009, 21, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1010, 22, 0, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1011, 23, 0, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1012, 24, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1013, 26, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1014, 18, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1015, 27, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1016, 28, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1017, 29, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1018, 30, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1019, 31, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1020, 25, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1021, 32, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1022, 33, 1, N'', 1)
INSERT [dbo].[Tests] ([TestID], [TestAppointmentID], [TestResult], [Notes], [CreatedByUserID]) VALUES (1023, 34, 0, N'', 1)
SET IDENTITY_INSERT [dbo].[Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[TestTypes] ON 

INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (1, N'Vision Test', N'This assesses the applicant''s visual acuity to ensure they have sufficient vision to drive safely.', 10.0000)
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (2, N'Written (Theory) Test', N'Thsi test assesses the applicant''s knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the rules of the road and can apply them in various driving scenarios.', 20.0000)
INSERT [dbo].[TestTypes] ([TestTypeID], [TestTypeTitle], [TestTypeDescription], [TestTypeFees]) VALUES (3, N'Practical (Street) Test', N'This test evaluates the applicant''s driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving Performance.', 30.0000)
SET IDENTITY_INSERT [dbo].[TestTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (1, 4, N'demo_admin', N'demo123', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (3, 7, N'Ali', N'123456', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (4, 6, N'Omar', N'123456', 0)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (8, 14, N'Maher', N'123456', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (9, 15, N'Fadi', N'123456', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (10, 17, N'Faris', N'1234', 1)
INSERT [dbo].[Users] ([UserID], [PersonID], [UserName], [Password], [IsActive]) VALUES (11, 18, N'Sami', N'1234', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD FOREIGN KEY([ApplicationPersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD FOREIGN KEY([ApplicationTypeID])
REFERENCES [dbo].[ApplicationTypes] ([ApplicationTypeID])
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD FOREIGN KEY([LicenseID])
REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD FOREIGN KEY([ReleasedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD FOREIGN KEY([ReleaseApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_People]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Users]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK__Internati__Appli__42ACE4D4] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK__Internati__Appli__42ACE4D4]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK__Internati__Crate__4589517F] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK__Internati__Crate__4589517F]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK__Internati__Drive__43A1090D] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK__Internati__Drive__43A1090D]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK__Internati__Issue__44952D46] FOREIGN KEY([IssuedUsingLocalLicenseID])
REFERENCES [dbo].[Licenses] ([LicenseID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK__Internati__Issue__44952D46]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([LicenseClassID])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK__LocalDriv__Appli__74AE54BC] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK__LocalDriv__Appli__74AE54BC]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD FOREIGN KEY([LicenseClassID])
REFERENCES [dbo].[LicenseClasses] ([LicenseClassID])
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Countries] FOREIGN KEY([NationalityCountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Countries]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD FOREIGN KEY([LocalDrivingLicenseApplicationID])
REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD FOREIGN KEY([RetakeTestApplicationID])
REFERENCES [dbo].[Applications] ([ApplicationID])
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD FOREIGN KEY([TestTypeID])
REFERENCES [dbo].[TestTypes] ([TestTypeID])
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD FOREIGN KEY([TestAppointmentID])
REFERENCES [dbo].[TestAppointments] ([TestAppointmentID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_People]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-New 2-Cancelled 3-Completed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'ApplicationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4-Replacement for Lost.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Licenses', @level2type=N'COLUMN',@level2name=N'IssueReason'
GO
USE [master]
GO
ALTER DATABASE [MyDVLD] SET  READ_WRITE 
GO


