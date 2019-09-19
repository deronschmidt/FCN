USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_fcn_activity')
DROP PROCEDURE sel_fcn_activity
GO

CREATE PROCEDURE sel_fcn_activity (@activityID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT a.ID
          ,a.description
          ,a.comments
          ,a.activity_date
          ,a.communityID
          ,c.community_name
          ,a.fcn_memberID
          ,fm.first_name AS provider_first_name
          ,fm.last_name AS provider_last_name
          ,a.service_categoryID
          ,cat.category_name
          ,a.service_subcategoryID
          ,sc.subcategory_name
          ,a.people_served
          ,a.unpaid_time
          ,a.paid_time
          ,a.mileage
          ,a.other_expenses
          ,a.created_date
          ,a.updated_date
    FROM FCN..fcn_activity AS a
    JOIN FCN..community AS c ON (c.ID = a.communityID)
    JOIN FCN..service_category AS cat ON (cat.ID = a.service_categoryID)
    JOIN FCN..service_subcategory AS sc ON (sc.ID = a.service_subcategoryID)
    JOIN FCN..fcn_member AS fm ON (fm.ID = a.fcn_memberID)
    WHERE a.ID  = @activityID OR @activityID IS NULL 
END
GO

GRANT EXECUTE ON sel_fcn_activity TO FCN_WRITER
GO
