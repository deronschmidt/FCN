SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.service_category(
    ID            int IDENTITY(1,1) NOT NULL,
    category_name nvarchar(300)     NOT NULL,
    description   nvarchar(max)     NOT NULL,
    active        smallint          NOT NULL,
    created_date  datetime          NOT NULL,
    updated_date  datetime          NOT NULL,
 CONSTRAINT PK_service_category PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO
