IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'get_club_user_by_email') AND type IN (N'P',N'PC'))
Drop procedure get_club_user_by_email
Go

create procedure get_club_user_by_email
@email varchar(50)

As 
select * from ClubUser where email = @email
Go



IF EXISTS(SELECT * 
FROM sys.objects 
WHERE object_id = OBJECT_ID(N'update_user_password') AND type IN (N'P',N'PC'))
Drop procedure update_user_password
Go

create procedure update_user_password
@userId int,
@newPassword varchar(250)

As 
update ClubUser set HashPassword = @newPassword where Id = @userId
Go