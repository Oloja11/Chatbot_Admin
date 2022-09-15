
IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_course_by_id') AND type IN (N'P',N'PC'))
Drop procedure get_course_by_id
Go

create procedure get_course_by_id
@id Bigint

as 
select * from tbl_course  where id = @id
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_courses') AND type IN (N'P',N'PC'))
Drop procedure get_courses
Go

create procedure get_courses
as 
select * from tbl_course
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'create_course') AND type IN (N'P',N'PC'))
Drop procedure create_course
Go


create procedure create_course
	
	@level [varchar](50),
	@department [varchar](1000),
	@title [varchar](500),
	@duration [varchar](50)
AS

INSERT INTO tbl_course([level],department,title,duration) 
VALUES(@level,@department,@title,@duration)


SELECT @@IDENTITY AS 'id';
Return @@Error
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'update_course') AND type IN (N'P',N'PC'))
Drop procedure update_course
Go


create procedure update_course
	@id bigint,
	@level [varchar](50),
	@department [varchar](1000),
	@title [varchar](500),
	@duration [varchar](50)
AS
UPDATE tbl_course
SET [level] = @level,
	department = @department,
	title = @title,
	duration = @duration
WHERE id = @id
Go

