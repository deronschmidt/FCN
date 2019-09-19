USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_member_role')
DROP PROCEDURE del_member_role
GO

CREATE PROCEDURE del_member_role (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..member_role WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..member_role
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_member_role TO FCN_WRITER
GO