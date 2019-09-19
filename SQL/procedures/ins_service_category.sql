USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_service_category')
DROP PROCEDURE ins_service_category
GO

CREATE PROCEDURE ins_service_category (@category_name nvarchar(300)
                                      ,@description nvarchar(max)
                                      ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO FCN..service_category(category_name,description,active,created_date,updated_date)
    VALUES(@category_name,@description,@active,GETDATE(),GETDATE())
    
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID
END
GO

GRANT EXECUTE ON ins_service_category TO FCN_WRITER
GO