USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_community_contact')
DROP PROCEDURE del_community_contact
GO

CREATE PROCEDURE del_community_contact (@ID int)
AS 
BEGIN
    SET NOCOUNT ON
	IF EXISTS (SELECT 1 FROM FCN..community_contact WHERE ID = @ID)
	BEGIN
        DELETE FROM FCN..community_contact
        WHERE ID = @ID
    END
END
GO

GRANT EXECUTE ON del_community_contact TO FCN_WRITER
GO