USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_member_role')
DROP PROCEDURE upd_member_role
GO

CREATE PROCEDURE upd_member_role (@ID int
                                 ,@role_type nvarchar(300)
                                 ,@description nvarchar(max)
                                 ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..member_role
    SET role_type = @role_type
       ,description = @description
       ,active = @active
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_member_role TO FCN_WRITER
GO
