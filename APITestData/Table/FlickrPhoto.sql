CREATE TABLE [dbo].[FlickrPhoto]
(
	[Id] BIGINT PRIMARY KEY IDENTITY NOT NULL, 
    [ImageId] NVARCHAR(50) NOT NULL, 
    [Owner] NVARCHAR(50) NOT NULL, 
    [Secret] NVARCHAR(50) NOT NULL, 
    [Server] NVARCHAR(50) NULL, 
    [Farm] INT NULL, 
    [Title] NVARCHAR(300) NULL, 
    [Ispublic] BIT NULL, 
    [Isfriend] BIT NULL, 
    [Isfamily] BIT NULL, 
    [SearchName] NVARCHAR(50) NULL, 
    [UserId] NVARCHAR(200) NULL,
    [Latitude] FLOAT NULL, 
    [Longitude] FLOAT NULL,
    
)
