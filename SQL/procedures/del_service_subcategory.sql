USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_service_subcategory')
DROP PROCEDURE del_service_subcategory
GO

CREATE PROCEDURE del_service_subcategory (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..service_subcategory WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..service_subcategory
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_service_subcategory TO FCN_WRITER
GO