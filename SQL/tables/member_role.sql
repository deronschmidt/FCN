SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.member_role(
    ID            int IDENTITY(1,1) NOT NULL,
    role_type     nvarchar(300)     NOT NULL,
    description   nvarchar(max)     NOT NULL,
    active        smallint          NOT NULL,
    created_date  datetime          NOT NULL,
    updated_date  datetime          NOT NULL,
 CONSTRAINT PK_member_role PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO
