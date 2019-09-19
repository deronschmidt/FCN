USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_fcn_activity')
DROP PROCEDURE del_fcn_activity
GO

CREATE PROCEDURE del_fcn_activity (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..fcn_activity WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..fcn_activity
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_fcn_activity TO FCN_WRITER
GO