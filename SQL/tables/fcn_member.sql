SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.fcn_member(
    ID            int IDENTITY(1,1) NOT NULL,
    roleID        int               NOT NULL,
    first_name    nvarchar(100)     NOT NULL,
    last_name     nvarchar(100)     NOT NULL,
    address1      nvarchar(300)     NOT NULL,
    address2      nvarchar(300)     NOT NULL,
    city          nvarchar(100)     NOT NULL,
    state         nvarchar(2)       NOT NULL,
    zip_code      nvarchar(10)      NOT NULL,
    email         nvarchar(100)     NOT NULL,
    phone         nvarchar(12)      NOT NULL,
    alt_phone     nvarchar(12)      NOT NULL,
    communityID   int               NOT NULL,
    licensed      smallint          NOT NULL,
    active        smallint          NOT NULL,
    active_date   datetime          NOT NULL,
    inactive_date datetime          NULL,
    administrator smallint          NOT NULL,
    created_date  datetime          NOT NULL,
    updated_date  datetime          NOT NULL,
    password      nvarchar(2000)    NOT NULL,
 CONSTRAINT PK_fcn_member PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO

ALTER TABLE dbo.fcn_member  WITH CHECK ADD  CONSTRAINT FK_fcn_member_community FOREIGN KEY(communityID)
REFERENCES dbo.community (ID)
GO

ALTER TABLE dbo.fcn_member CHECK CONSTRAINT FK_fcn_member_community
GO

ALTER TABLE dbo.fcn_member  WITH CHECK ADD  CONSTRAINT FK_fcn_member_member_role FOREIGN KEY(roleID)
REFERENCES dbo.member_role (ID)
GO

ALTER TABLE dbo.fcn_member CHECK CONSTRAINT FK_fcn_member_member_role
GO
