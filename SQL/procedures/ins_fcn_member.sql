USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_fcn_member')
DROP PROCEDURE ins_fcn_member
GO

CREATE PROCEDURE ins_fcn_member (@roleID int
                                ,@first_name nvarchar(100)
                                ,@last_name nvarchar(100)
                                ,@address1 nvarchar(300)
                                ,@address2 nvarchar(300)
                                ,@city nvarchar(100)
                                ,@state nvarchar(2)
                                ,@zip_code nvarchar(10)
                                ,@email nvarchar(100)
                                ,@phone nvarchar(12)
                                ,@alt_phone nvarchar(12)
                                ,@communityID int
                                ,@licensed smallint
                                ,@active smallint
                                ,@active_date datetime
                                ,@inactive_date datetime
                                ,@administrator int
								,@passwd nvarchar(2000))
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO FCN..fcn_member(roleID,first_name,last_name,address1,address2,city,state,zip_code,email,phone,alt_phone,communityID,
                                 licensed,active,active_date,inactive_date,administrator,password,created_date,updated_date)
    VALUES(@roleID,@first_name,@last_name,@address1,@address2,@city,@state,@zip_code,@email,@phone,@alt_phone,@communityID,
           @licensed,@active,@active_date,@inactive_date,@administrator,@passwd,GETDATE(),GETDATE())
           
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID         
END
GO

GRANT EXECUTE ON ins_fcn_member TO FCN_WRITER
GO