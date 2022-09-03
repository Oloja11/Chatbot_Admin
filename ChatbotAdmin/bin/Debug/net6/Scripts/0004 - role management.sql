IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'ClubRole') AND type IN (N'U'))

CREATE TABLE ClubRole(
Id BIGINT IDENTITY(1,1) PRIMARY KEY,
Name varchar(50) not null
CONSTRAINT uq_ClubRole_Name UNIQUE(Name)
)
Go


insert into ClubRole values('platform')
insert into ClubRole values('admin')
insert into ClubRole values('user')
GO


IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'UserRole') AND type IN (N'U'))

CREATE TABLE UserRole(
Id BIGINT IDENTITY(1,1) PRIMARY KEY,
UserId bigint not null,
RoleId bigint not null
)
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_roles') AND type IN (N'P',N'PC'))
Drop procedure get_roles
Go

create procedure get_roles
As
select * from ClubRole
Go


IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'assign_user_to_role') AND type IN (N'P',N'PC'))
Drop procedure assign_user_to_role
Go

create procedure assign_user_to_role
@UserId bigint,
@RoleId bigint
As
if not exists(select * from UserRole where UserId=@UserId and RoleId=@RoleId)
insert into UserRole(UserId,RoleId) values (@UserId,@RoleId)

SELECT @@IDENTITY AS 'id';
Return @@Error

Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_user_roles') AND type IN (N'P',N'PC'))
Drop procedure get_user_roles
Go

create procedure get_user_roles
@UserId Bigint 
As
select name from ClubRole r
inner join UserRole u on r.Id = u.RoleId and u.UserId = @UserId
Go