USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_service_subcategory')
DROP PROCEDURE ins_service_subcategory
GO

CREATE PROCEDURE ins_service_subcategory (@subcategory_name nvarchar(300)
                                         ,@service_categoryID int
                                         ,@description nvarchar(max)
                                         ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO FCN..service_subcategory(subcategory_name,service_categoryID,description,active,created_date,updated_date)
    VALUES(@subcategory_name,@service_categoryID,@description,@active,GETDATE(),GETDATE())
    
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID  
END
GO

GRANT EXECUTE ON ins_service_subcategory TO FCN_WRITER
GO