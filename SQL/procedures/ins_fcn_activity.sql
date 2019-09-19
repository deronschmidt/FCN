USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_fcn_activity')
DROP PROCEDURE ins_fcn_activity
GO

CREATE PROCEDURE ins_fcn_activity (@description nvarchar(500)
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
    INSERT INTO FCN..fcn_activity(description,comments,activity_date,communityID,fcn_memberID,service_categoryID,service_subcategoryID,people_served,
                                 unpaid_time,paid_time,mileage,other_expenses,created_date,updated_date)
    VALUES(@description,@comments,@activity_date,@communityID,@fcn_memberID,@service_categoryID,@service_subcategoryID,@people_served,
           @unpaid_time,@paid_time,@mileage,@other_expenses,GETDATE(),GETDATE())
           
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID         
END
GO

GRANT EXECUTE ON ins_fcn_activity TO FCN_WRITER
GO