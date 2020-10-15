CREATE PROCEDURE [dbo].[sprUpSertFlickrPhoto]
	@id NVARCHAR(50),
    @owner NVARCHAR(50),
    @secret NVARCHAR(50), 
    @server NVARCHAR(50), 
    @farm INT, 
    @title NVARCHAR(300), 
    @ispublic BIT, 
    @isfriend BIT, 
    @isfamily BIT,
    @SearchName NVARCHAR(50),
    @userId NVARCHAR(200),
    @Latitude FLOAT,
    @Longitude FLOAT
AS
BEGIN
SET NOCOUNT ON;
    IF NOT EXISTS(SELECT Id FROM dbo.FlickrPhoto WHERE ImageId = @id AND [Secret] = @secret AND SearchName = @SearchName)
        BEGIN
            IF(@SearchName IS NOT NULL)
                BEGIN
	                INSERT INTO FlickrPhoto(ImageId, [Owner], [Secret], [Server], Farm, Title, Ispublic, Isfriend, Isfamily, SearchName, UserId)
                    VALUES(@id, @owner, @secret , @server, @farm , @title, @ispublic, @isfriend, @isfamily, @SearchName, @userId)
                END
            ELSE
                BEGIN
                    INSERT INTO FlickrPhoto(ImageId, [Owner], [Secret], [Server], Farm, Title, Ispublic, Isfriend, Isfamily, Latitude, Longitude , UserId)
                    VALUES(@id, @owner, @secret , @server, @farm , @title, @ispublic, @isfriend, @isfamily, @Latitude, @Longitude, @userId)
                END
        END
    ELSE
        BEGIN
            UPDATE dbo.FlickrPhoto 
            SET [Owner] = @owner, [Secret] = @secret, [Server] = @server, Farm = @farm, Title = @title, Ispublic = @ispublic, Isfriend = @isfriend, Isfamily = @isfamily, SearchName = @SearchName
            WHERE ImageId = @id AND [Secret] = @secret AND SearchName = @SearchName;
        END
END