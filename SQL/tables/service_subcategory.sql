SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.service_subcategory(
    ID                        int IDENTITY(1,1) NOT NULL,
    subcategory_name          nvarchar(300)     NOT NULL,
    service_categoryID        int               NOT NULL,
    description               nvarchar(max)     NOT NULL,
    active                    smallint          NOT NULL,
    created_date              datetime          NOT NULL,
    updated_date              datetime          NOT NULL,
 CONSTRAINT PK_service_subcategory PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO

ALTER TABLE dbo.service_subcategory  WITH CHECK ADD  CONSTRAINT FK_service_subcategory_service_category FOREIGN KEY(service_categoryID)
REFERENCES dbo.service_category (ID)
GO

ALTER TABLE dbo.service_subcategory CHECK CONSTRAINT FK_service_subcategory_service_category
GO
