USE FCN
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sel_community')
DROP PROCEDURE sel_community
GO

CREATE PROCEDURE sel_community (@communityID int = null)
AS
BEGIN
    SET NOCOUNT ON
    SELECT c.ID
          ,c.community_name
          ,c.affiliation
          ,c.address1
          ,c.address2
          ,c.city
          ,c.state
          ,c.zip_code
          ,c.phone
          ,c.alt_phone
          ,c.email
          ,c.website
          ,c.active
          ,c.created_date
          ,c.updated_date
    FROM FCN..community AS c
    WHERE c.ID  = @communityID OR @communityID IS NULL 
END
GO

GRANT EXECUTE ON sel_community TO FCN_WRITER
GO
