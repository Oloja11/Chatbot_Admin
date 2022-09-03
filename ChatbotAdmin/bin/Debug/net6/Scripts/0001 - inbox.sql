IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'tbl_inbox') AND type IN (N'U'))

CREATE TABLE tbl_inbox(
Id BIGINT IDENTITY(1,1) PRIMARY KEY,
SenderEmail varchar(250) not null,
Subject varchar(250) null,
Content varchar(1000) null,
ReceivedDate Date not null,
IsRead int not null,
SenderName varchar(250) null
)
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_inbox_message_by_id') AND type IN (N'P',N'PC'))
Drop procedure get_inbox_message_by_id
Go

create procedure get_inbox_message_by_id
@id Bigint

as 
 
update tbl_inbox set IsRead = 1 where Id = @id
select * from tbl_inbox  where Id = @id
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_inbox_messages') AND type IN (N'P',N'PC'))
Drop procedure get_inbox_messages
Go

create procedure get_inbox_messages
as
select * from tbl_inbox order by id desc
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'delete_inbox_message') AND type IN (N'P',N'PC'))
Drop procedure delete_inbox_message
Go

create procedure delete_inbox_message
@id Bigint
as 
	delete tbl_inbox  where Id = @id
Go

