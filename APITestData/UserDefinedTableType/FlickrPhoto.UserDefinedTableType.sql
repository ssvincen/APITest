CREATE TYPE [dbo].[udtFlickrPhoto] AS TABLE
(
	id NVARCHAR(50), 
    [owner] NVARCHAR(50), 
    [secret] NVARCHAR(50), 
    [server] NVARCHAR(50), 
    farm INT, 
    title NVARCHAR(300), 
    ispublic BIT, 
    isfriend BIT, 
    isfamily BIT 
)
