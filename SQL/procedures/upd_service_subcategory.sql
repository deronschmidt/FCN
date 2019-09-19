USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_service_subcategory')
DROP PROCEDURE upd_service_subcategory
GO

CREATE PROCEDURE upd_service_subcategory (@ID int
                                         ,@subcategory_name nvarchar(300)
                                         ,@service_categoryID int
                                         ,@description nvarchar(max)
                                         ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..service_subcategory
    SET subcategory_name = @subcategory_name
       ,service_categoryID = @service_categoryID
       ,description = @description
       ,active = @active
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_service_subcategory TO FCN_WRITER
GO
