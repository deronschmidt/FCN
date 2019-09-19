USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_fcn_activity')
DROP PROCEDURE upd_fcn_activity
GO

CREATE PROCEDURE upd_fcn_activity (@ID int
                                  ,@description nvarchar(500)
                                  ,@comments nvarchar(max)
                                  ,@activity_date datetime
                                  ,@communityID int
                                  ,@fcn_memberID int
                                  ,@service_categoryID int
                                  ,@service_subcategoryID int
                                  ,@people_served int
                                  ,@unpaid_time int
                                  ,@paid_time int
                                  ,@mileage int
                                  ,@other_expenses decimal(19,4))
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..fcn_activity
    SET description = @description
       ,comments = @comments
       ,activity_date = @activity_date
       ,communityID = @communityID
       ,fcn_memberID = @fcn_memberID
       ,service_categoryID = @service_categoryID
       ,service_subcategoryID = @service_subcategoryID
       ,people_served = @people_served
       ,unpaid_time = @unpaid_time
       ,paid_time = @paid_time
       ,mileage = @mileage
       ,other_expenses = @other_expenses
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_fcn_activity TO FCN_WRITER
GO