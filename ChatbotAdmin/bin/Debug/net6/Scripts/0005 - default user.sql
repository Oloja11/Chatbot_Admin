
INSERT INTO [dbo].[ClubUser]
           ([FirstName]
           ,[LastName]
           ,[Email]
           ,[PhoneNumber]
           ,[Sex]
           ,[DateOfBirth]
           ,[IsActive]
           ,[HashPassword])
     VALUES
           ('Platform'
           ,'Admin'
           ,'admin@email.com'
           ,'08022112212'
           ,'Male'
           ,'2004-12-12'
           ,1
           ,'QWRtaW4=')

INSERT INTO [dbo].[UserRole]
           ([UserId]
           ,[RoleId])
     VALUES
           (1
           ,1)
GO


