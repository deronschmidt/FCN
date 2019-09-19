USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_community')
DROP PROCEDURE ins_community
GO

CREATE PROCEDURE ins_community (@community_name nvarchar(300)
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
    INSERT INTO FCN..community(community_name,affiliation,address1,address2,city,state,
                              zip_code,phone,alt_phone,email,website,active,created_date,updated_date)
    VALUES(@community_name,@affiliation,@address1,@address2,@city,@state,
           @zip_code,@phone,@alt_phone,@email,@website,@active,GETDATE(),GETDATE())
           
    SELECT CAST(SCOPE_IDENTITY() AS int) as ID 
END
GO

GRANT EXECUTE ON ins_community TO FCN_WRITER
GO