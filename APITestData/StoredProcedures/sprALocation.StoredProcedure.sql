CREATE PROCEDURE [dbo].[sprLocation]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT [SearchName]
	FROM [dbo].[FlickrPhoto]
END