CREATE PROCEDURE [dbo].[sprAllSaveFlickrPhoto]
	@photos [dbo].[udtFlickrPhoto] readonly,
    @PhotoId INT OUTPUT

AS
BEGIN
	INSERT INTO FlickrPhoto(ImageId, [Owner], [Secret], [Server], Farm, Title, Ispublic, Isfriend, Isfamily)
	SELECT id, [owner], [secret] ,[server], farm , title, ispublic, isfriend, isfamily 
     FROM @photos  
     --WHERE NOT EXISTS ( SELECT 1 
     --                   FROM FlickrPhoto f
     --                   INNER JOIN @photos p  
     --                       ON f.ImageId = p.id
     --                    AND p.[Secret] = p.[secret] )

    SELECT @PhotoId = SCOPE_IDENTITY()

END