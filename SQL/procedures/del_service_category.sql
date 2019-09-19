USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_service_category')
DROP PROCEDURE del_service_category
GO

CREATE PROCEDURE del_service_category (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..service_category WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..service_category
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_service_category TO FCN_WRITER
GO