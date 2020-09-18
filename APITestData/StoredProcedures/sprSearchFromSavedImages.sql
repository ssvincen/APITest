CREATE PROCEDURE [dbo].[sprSearchFromSavedImages]
	@SearchName NVARCHAR(50)
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
      WHERE SearchName = @SearchName
END
