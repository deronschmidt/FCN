USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_community_contact')
DROP PROCEDURE upd_community_contact
GO

CREATE PROCEDURE upd_community_contact (@ID int
                                       ,@communityID int
                                       ,@contactID int)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..community_contact
    SET communityID = @communityID
       ,contactID = @contactID
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_community_contact TO FCN_WRITER
GO