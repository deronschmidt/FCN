USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_community')
DROP PROCEDURE upd_community
GO

CREATE PROCEDURE upd_community (@ID int
                               ,@community_name nvarchar(300)
                               ,@affiliation nvarchar(300)
                               ,@address1 nvarchar(300)
                               ,@address2 nvarchar(300)
                               ,@city nvarchar(100)
                               ,@state nvarchar(2)
                               ,@zip_code nvarchar(10)
                               ,@phone nvarchar(12)
                               ,@alt_phone nvarchar(12)
                               ,@email nvarchar(100)
                               ,@website nvarchar(200)
                               ,@active smallint)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE FCN..community
    SET community_name = @community_name
       ,affiliation = @affiliation
       ,address1 = @address1
       ,address2 = @address2
       ,city = @city
       ,state = @state
       ,zip_code = @zip_code
       ,phone = @phone
       ,alt_phone = @alt_phone
       ,email = @email
       ,website = @email
       ,active = @active
       ,updated_date = GETDATE()
    WHERE ID = @ID
END
GO

GRANT EXECUTE ON upd_community TO FCN_WRITER
GO
