USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_service_subcategory')
DROP PROCEDURE sel_service_subcategory
GO

CREATE PROCEDURE sel_service_subcategory (@subcategoryID int = null)
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
    WHERE sc.ID = @subcategoryID OR @subcategoryID IS NULL
END
GO

GRANT EXECUTE ON sel_service_subcategory TO FCN_WRITER
GO
