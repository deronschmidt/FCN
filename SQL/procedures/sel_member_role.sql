USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_member_role')
DROP PROCEDURE sel_member_role
GO

CREATE PROCEDURE sel_member_role (@roleID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT r.ID
          ,r.role_type
          ,r.description
          ,r.active
          ,r.created_date
          ,r.updated_date
    FROM FCN..member_role AS r
    WHERE r.ID = @roleID OR @roleID IS NULL
END
GO

GRANT EXECUTE ON sel_member_role TO FCN_WRITER
GO
