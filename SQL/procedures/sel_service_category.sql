USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_service_category')
DROP PROCEDURE sel_service_category
GO

CREATE PROCEDURE sel_service_category (@categoryID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT c.ID
          ,c.category_name
          ,c.description
          ,c.active
          ,c.created_date
          ,c.updated_date
    FROM FCN..service_category AS c
    WHERE c.ID = @categoryID OR @categoryID IS NULL
END
GO

GRANT EXECUTE ON sel_service_category TO FCN_WRITER
GO

