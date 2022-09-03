IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'psp_submit_inquiry') AND type IN (N'P',N'PC'))
Drop procedure psp_submit_inquiry
Go

create procedure psp_submit_inquiry
@email varchar(250),
@chat varchar(1000),
@senderName varchar(250)

As 
insert into tbl_inbox (SenderEmail,Content,ReceivedDate,IsRead,SenderName)
values(@email,@chat,GetDate(),0,@senderName)
Go

