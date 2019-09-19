SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.community(
    ID                int IDENTITY(1,1) NOT NULL,
    community_name    nvarchar(300)     NOT NULL,
    affiliation       nvarchar(300)     NOT NULL,
    address1          nvarchar(300)     NOT NULL,
    address2          nvarchar(300)     NULL,
    city              nvarchar(100)     NOT NULL,
    state             nvarchar(2)       NOT NULL,
    zip_code          nvarchar(10)      NOT NULL,
    phone             nvarchar(12)      NOT NULL,
    alt_phone         nvarchar(12)      NULL,
    email             nvarchar(100)     NULL,
    website           nvarchar(200)     NULL,
    active            smallint          NOT NULL,
    created_date      datetime          NOT NULL,
    updated_date      datetime          NOT NULL,
 CONSTRAINT PK_community PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO
