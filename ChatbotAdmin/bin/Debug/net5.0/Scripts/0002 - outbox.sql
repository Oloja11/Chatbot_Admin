
IF NOT EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'tbl_outbox') AND type IN (N'U'))

CREATE TABLE tbl_outbox(
Id BIGINT IDENTITY(1,1) PRIMARY KEY,
SenderEmail varchar(250) not null,
Content varchar(1000) null,
ReplyMessage varchar(1000) null,
ReceivedDate Date not null,
ReplyDate Date not null,
SenderName varchar(250) null
)
Go


IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_outbox_message_by_id') AND type IN (N'P',N'PC'))
Drop procedure get_outbox_message_by_id
Go

create procedure get_outbox_message_by_id
@id Bigint

as 
select * from tbl_outbox  where Id = @id
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_outbox_messages') AND type IN (N'P',N'PC'))
Drop procedure get_outbox_messages
Go

create procedure get_outbox_messages
as
select * from tbl_outbox order by id desc
Go

IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'delete_outbox_message') AND type IN (N'P',N'PC'))
Drop procedure delete_outbox_message
Go

create procedure delete_outbox_message
@id Bigint
as 
	delete tbl_outbox  where Id = @id
Go


IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'create_outbox') AND type IN (N'P',N'PC'))
Drop procedure create_outbox
Go


create procedure create_outbox
@From varchar(250),
@SenderName varchar(250),
@Content varchar(1000),
@ReplyMessage varchar(1000),
@ReceivedDate Date
AS

INSERT INTO tbl_outbox(SenderEmail,Content,ReplyMessage,ReceivedDate,ReplyDate,SenderName) 
VALUES(@From,@Content,@ReplyMessage,@ReceivedDate,Getdate(),@SenderName)


SELECT @@IDENTITY AS 'id';
Return @@Error
Go
