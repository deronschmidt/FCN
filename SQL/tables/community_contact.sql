SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.community_contact(
    ID              int IDENTITY(1,1) NOT NULL,    
    communityID     int               NOT NULL,
    contactID       int               NOT NULL,
    created_date    datetime          NOT NULL,
    updated_date    datetime          NOT NULL,
 CONSTRAINT PK_community_contact PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO

ALTER TABLE dbo.community_contact  WITH CHECK ADD  CONSTRAINT FK_community_contact_communityID FOREIGN KEY(communityID)
REFERENCES dbo.community (ID)
GO

ALTER TABLE dbo.community_contact CHECK CONSTRAINT FK_community_contact_communityID
GO

ALTER TABLE dbo.community_contact  WITH CHECK ADD  CONSTRAINT FK_community_contact_contactID FOREIGN KEY(contactID)
REFERENCES dbo.fcn_member (ID)
GO

ALTER TABLE dbo.community_contact CHECK CONSTRAINT FK_community_contact_contactID
GO
