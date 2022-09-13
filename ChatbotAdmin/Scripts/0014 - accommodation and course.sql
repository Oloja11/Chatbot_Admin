IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'tbl_accommodation') AND type IN (N'U'))

CREATE TABLE [dbo].[tbl_accommodation](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NOT NULL,
	[name] [varchar](250) NOT NULL,
	[description] [varchar](1000) NULL,
	[amount] [varchar](50) NULL,
	[link] [varchar](50) NULL
	) 
GO


IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'tbl_course') AND type IN (N'U'))

CREATE TABLE [dbo].[tbl_course](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[level] [varchar](50) NOT NULL,
	[department] [varchar](1000) NOT NULL,
	[title] [varchar](500) NOT NULL,
	[duration] [varchar](50) NOT NULL
)
GO


if ((select count(*) from tbl_accommodation) = 0)
	begin
		INSERT [tbl_accommodation] ([type], [name], [description], [amount], [link]) VALUES ( N'off-campus', N'Agency- Loc8me', N'Loc8me offers different type of accomodation for singles  mostly', N'84 p/w', N'')

		INSERT [tbl_accommodation] ([type], [name], [description], [amount], [link]) VALUES ( N'on-campus', N'Agency-Lime', N'Lime offers accomodation for both singles and couples with or without children', N'150-200 p/w', NULL)

		INSERT [tbl_accommodation] ([type], [name], [description], [amount], [link]) VALUES ( N'on-campus', N'UH Campus- Westfield Court', N'Ensuite rooms', N'From 154 p/w', NULL)

		INSERT [tbl_accommodation] ([type], [name], [description], [amount], [link]) VALUES ( N'on-campus', N'UH Campus- The court yard', N'Ensuite rooms', N'from 160 p/w', NULL)

		INSERT [tbl_accommodation] ([type], [name], [description], [amount], [link]) VALUES ( N'on-campus', N'UH Campus- TheTaylor', N'Ensuite rooms', N'140 p/w', NULL)
	end

if ((select count(*) from tbl_course) = 0)
	begin
		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'postgraduate', N'Faculty of Arts, Cultures and Education', N'Theatre Arts', N'1 year')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'postgraduate', N'Faculty of Business, Law and Politcs', N'Accounting', N'1 year')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'postgraduate', N'Faculty of Science and Engineering', N'Computer Scince', N'1 year')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'postgraduate', N'Faculty of Health Sciences', N'Nursing', N'1 year')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'undergraduate', N'Faculty of Arts, Cultures and Education', N'Theatre Arts', N'3 years')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'undergraduate', N'Faculty of Business, Law and Politcs', N'Accounting', N'4 years')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'undergraduate', N'Faculty of Science and Engineering', N'Computer Scince', N'5 years')

		INSERT [tbl_course] ([level], [department], [title], [duration]) VALUES ( N'undergraduate', N'Faculty of Health Sciences', N'Nursing', N'6 years')
	end