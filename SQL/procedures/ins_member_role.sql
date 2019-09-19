USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_member_role')
DROP PROCEDURE ins_member_role
GO

CREATE PROCEDURE ins_member_role (@role_type nvarchar(300)
                                 ,@description nvarchar(max)
                                 ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO FCN..member_role(role_type,description,active,created_date,updated_date)
    VALUES(@role_type,@description,@active,GETDATE(),GETDATE())
    
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID 
END
GO

GRANT EXECUTE ON ins_member_role TO FCN_WRITER
GO