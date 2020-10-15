CREATE PROCEDURE [dbo].[sprDeleteFlickrPhotoById]
	@Id INT,
	@UserId NVARCHAR(128)
AS
DECLARE @IsDeleted BIT = 0;
BEGIN
	IF EXISTS(SELECT Id FROM dbo.FlickrPhoto WHERE Id = @Id AND UserId = @UserId)
        BEGIN
           DELETE 
           FROM dbo.FlickrPhoto
           WHERE Id = @Id AND UserId = @UserId

           SET @IsDeleted = 1;
           
 
        END
END
SELECT @IsDeleted;