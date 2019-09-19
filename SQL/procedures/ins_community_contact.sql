USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_community_contact')
DROP PROCEDURE ins_community_contact
GO

CREATE PROCEDURE ins_community_contact (@communityID int
                                       ,@contactID int)
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO FCN..community_contact(communityID,contactID,created_date,updated_date)
    VALUES(@communityID,@contactID,GETDATE(),GETDATE())
    
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID
END
GO

GRANT EXECUTE ON ins_community_contact TO FCN_WRITER
GO