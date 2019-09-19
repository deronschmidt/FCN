USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_community_contact')
DROP PROCEDURE sel_community_contact
GO

CREATE PROCEDURE sel_community_contact (@ID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT cc.ID
          ,cc.communityID
          ,community.community_name
          ,cc.contactID
          ,fm.first_name
          ,fm.last_name
          ,cc.created_date
          ,cc.updated_date
    FROM FCN..community_contact AS cc
    JOIN FCN..community as community on (community.ID = cc.communityID)
    JOIN FCN..fcn_member as fm on (fm.ID = cc.contactID)
    WHERE cc.ID = @ID OR @ID IS NULL
END
GO

GRANT EXECUTE ON sel_community_contact TO FCN_WRITER
GO
