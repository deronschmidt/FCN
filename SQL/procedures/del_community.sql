USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_community')
DROP PROCEDURE del_community
GO

CREATE PROCEDURE del_community (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..community WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..community
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_community TO FCN_WRITER
GO
