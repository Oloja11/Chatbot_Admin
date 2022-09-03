IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'ClubUser') AND type IN (N'U'))

CREATE TABLE ClubUser(
Id BIGINT IDENTITY(1,1) PRIMARY KEY,
FirstName varchar(50) not null,
LastName varchar(50) not null,
Email varchar(50) not null,
PhoneNumber varchar(15) not null,
Sex varchar(10) not null,
DateOfBirth Date not null,
IsActive int not null,
HashPassword varchar(250) not null,
CONSTRAINT uq_ClubUser_Email UNIQUE(Email)
)
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'create_club_user') AND type IN (N'P',N'PC'))
Drop procedure create_club_user
Go

create procedure create_club_user
@FirstName varchar(50),
@LastName varchar(50),
@Email varchar(50),
@PhoneNumber varchar(15),
@Sex varchar(10),
@DateOfBirth Date,
@IsActive int,
@HashPassword varchar(250)

AS 
insert into ClubUser(FirstName,LastName,Email,PhoneNumber,Sex,DateOfBirth,IsActive,HashPassword) 
values(@FirstName,@LastName,@Email,@PhoneNumber,@Sex,@DateOfBirth,@IsActive,@HashPassword)

SELECT @@IDENTITY AS 'id';
Return @@Error

GO


IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_club_user_with_login_details') AND type IN (N'P',N'PC'))
Drop procedure get_club_user_with_login_details
Go

create procedure get_club_user_with_login_details
@Email varchar(50),
@HashPassword varchar(250)

As 
select * from ClubUser where Email=@Email and HashPassword = @HashPassword
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_club_user_with_id') AND type IN (N'P',N'PC'))
Drop procedure get_club_user_with_id
Go

create procedure get_club_user_with_id
@id BIGINT

As 
select * from ClubUser where Id = @id
Go


IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_platform_Users') AND type IN (N'P',N'PC'))
Drop procedure get_platform_Users
Go

create procedure get_platform_Users
As 
select * from ClubUser where Id != 1
Go

