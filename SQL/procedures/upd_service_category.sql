USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_service_category')
DROP PROCEDURE upd_service_category
GO

CREATE PROCEDURE upd_service_category (@ID int
                                      ,@category_name nvarchar(300)
                                      ,@description nvarchar(max)
                                      ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..service_category
    SET category_name = @category_name
       ,description = @description
       ,active = @active
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_service_category TO FCN_WRITER
GO
