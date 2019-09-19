USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_fcn_member')
DROP PROCEDURE upd_fcn_member
GO

CREATE PROCEDURE upd_fcn_member (@ID int
                                ,@roleID int
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
                                ,@administrator int)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..fcn_member
    SET roleID = @roleID
       ,first_name = @first_name
       ,last_name = @last_name
       ,address1 = @address1
       ,address2 = @address2
       ,city = @city
       ,state = @state
       ,zip_code = @zip_code
       ,email = @email
       ,phone = @phone
       ,alt_phone = @alt_phone
       ,communityID = @communityID
       ,licensed = @licensed
       ,active = @active
       ,active_date = @active_date
       ,inactive_date = @inactive_date
       ,administrator = @administrator
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_fcn_member TO FCN_WRITER
GO
