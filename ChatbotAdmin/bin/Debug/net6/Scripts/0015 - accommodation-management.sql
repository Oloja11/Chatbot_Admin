
IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_accommodation_by_id') AND type IN (N'P',N'PC'))
Drop procedure get_accommodation_by_id
Go

create procedure get_accommodation_by_id
@id Bigint

as 
select [id],[type] as AccommodationType,[name],[description],[amount],[link] from tbl_accommodation  where id = @id
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_accommodations') AND type IN (N'P',N'PC'))
Drop procedure get_accommodations
Go

create procedure get_accommodations
as 
select [id],[type] as AccommodationType,[name],[description],[amount],[link] from tbl_accommodation
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'create_accommodation') AND type IN (N'P',N'PC'))
Drop procedure create_accommodation
Go


create procedure create_accommodation
	@type varchar(50),
	@name varchar(250),
	@description varchar(1000),
	@amount varchar(50),
	@link varchar(50)
AS

INSERT INTO tbl_accommodation([type],[name],[description],[amount],[link]) 
VALUES(@type,@name,@description,@amount,@link)


SELECT @@IDENTITY AS 'id';
Return @@Error
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'update_accommodation') AND type IN (N'P',N'PC'))
Drop procedure update_accommodation
Go


create procedure update_accommodation
	@id bigint,
	@type varchar(50),
	@name varchar(250),
	@description varchar(1000),
	@amount varchar(50),
	@link varchar(50)
AS
UPDATE tbl_accommodation
SET [type] = @type,
	[name] = @name,
	[description] = @description,
	[amount] = @amount,
	[link] = @link
WHERE id = @id
Go

