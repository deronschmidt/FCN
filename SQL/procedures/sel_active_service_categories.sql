USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_active_service_categories')
DROP PROCEDURE sel_active_service_categories
GO

CREATE PROCEDURE sel_active_service_categories 
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
    WHERE active = 1 
    ORDER BY category_name
END
GO

GRANT EXECUTE ON sel_active_service_categories TO FCN_WRITER
GO
