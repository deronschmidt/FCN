USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_active_service_subcategories')
DROP PROCEDURE sel_active_service_subcategories
GO

CREATE PROCEDURE sel_active_service_subcategories (@categoryID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT sc.ID
          ,sc.subcategory_name
          ,sc.service_categoryID
          ,c.category_name
          ,sc.description
          ,sc.active
          ,sc.created_date
          ,sc.updated_date
    FROM FCN..service_subcategory AS sc
    JOIN FCN..service_category AS c ON (c.ID = sc.service_categoryID)
    WHERE sc.active = 1 AND sc.service_categoryID = @categoryID ORDER BY sc.subcategory_name
END
GO

GRANT EXECUTE ON sel_active_service_subcategories TO FCN_WRITER
GO
