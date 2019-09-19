USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_fcn_member')
DROP PROCEDURE sel_fcn_member
GO

CREATE PROCEDURE sel_fcn_member (@memberID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT fm.ID
          ,fm.roleID
          ,r.role_type
          ,fm.first_name
          ,fm.last_name
          ,fm.address1
          ,fm.address2
          ,fm.city
          ,fm.state
          ,fm.zip_code
          ,fm.email
          ,fm.phone
          ,fm.alt_phone
		  ,fm.communityID
          ,c.community_name
          ,fm.licensed
          ,fm.active
          ,fm.active_date
          ,fm.inactive_date
          ,fm.administrator
		  ,fm.password
          ,fm.created_date
          ,fm.updated_date		  
    FROM FCN..fcn_member AS fm
    JOIN FCN..member_role AS r ON (r.ID = fm.roleID)
	LEFT JOIN FCN..community AS c on (c.ID = fm.communityID)
    WHERE fm.ID = @memberID OR @memberID IS NULL
END
GO

GRANT EXECUTE ON sel_fcn_member TO FCN_WRITER
GO
