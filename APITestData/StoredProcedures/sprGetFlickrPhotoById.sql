CREATE PROCEDURE [dbo].[sprGetFlickrPhotoById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    SELECT [Id] AS ImageId
          ,[ImageId] AS id
          ,[Owner]
          ,[Secret]
          ,[Server]
          ,[Farm]
          ,[Title]
          ,[Ispublic]
          ,[Isfriend]
          ,[Isfamily]
          ,[SearchName]
          ,[Latitude]
          ,[Longitude]
      FROM [dbo].[FlickrPhoto]
      WHERE Id = @Id 
END