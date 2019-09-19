USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_fcn_member')
DROP PROCEDURE del_fcn_member
GO

CREATE PROCEDURE del_fcn_member (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..fcn_member WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..fcn_member
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_fcn_member TO FCN_WRITER
GO