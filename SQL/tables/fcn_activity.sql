SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.fcn_activity(
    ID                    int IDENTITY(1,1) NOT NULL,
    description           nvarchar(500)     NOT NULL,    
    comments              nvarchar(max)     NOT NULL,
    activity_date         datetime          NOT NULL,
    communityID           int               NOT NULL,
    fcn_memberID          int               NOT NULL,
    service_categoryID    int               NOT NULL,
    service_subcategoryID int               NULL,
    people_served         int               NOT NULL,
    unpaid_time           int               NOT NULL,
    paid_time             int               NOT NULL,
    mileage               int               NOT NULL,
    other_expenses        decimal(19, 4)    NOT NULL,
    created_date          datetime          NOT NULL,
    updated_date          datetime          NOT NULL,
 CONSTRAINT PK_fcn_activity PRIMARY KEY CLUSTERED 
(
    ID ASC
))
GO

ALTER TABLE dbo.fcn_activity  WITH CHECK ADD  CONSTRAINT FK_fcn_activity_community FOREIGN KEY(communityID)
REFERENCES dbo.community (ID)
GO

ALTER TABLE dbo.fcn_activity CHECK CONSTRAINT FK_fcn_activity_community
GO

ALTER TABLE dbo.fcn_activity  WITH CHECK ADD  CONSTRAINT FK_fcn_activity_fcn_member FOREIGN KEY(fcn_memberID)
REFERENCES dbo.fcn_member (ID)
GO

ALTER TABLE dbo.fcn_activity CHECK CONSTRAINT FK_fcn_activity_fcn_member
GO

ALTER TABLE dbo.fcn_activity  WITH CHECK ADD  CONSTRAINT FK_fcn_activity_service_subcategory FOREIGN KEY(service_categoryID)
REFERENCES dbo.service_subcategory (ID)
GO

ALTER TABLE dbo.fcn_activity CHECK CONSTRAINT FK_fcn_activity_service_subcategory
GO

ALTER TABLE dbo.fcn_activity  WITH CHECK ADD  CONSTRAINT FK_fcn_activity_service_subcategory1 FOREIGN KEY(service_subcategoryID)
REFERENCES dbo.service_subcategory (ID)
GO

ALTER TABLE dbo.fcn_activity CHECK CONSTRAINT FK_fcn_activity_service_subcategory1
GO

