BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE RelationUserRole
	(
	RowId int NOT NULL IDENTITY(1,1) CONSTRAINT PK_RelationUserRole PRIMARY KEY,
	IdRelationUserRole int NOT NULL,
	IsActive int NOT NULL,
	RefUser int NOT NULL,
	RefRole int NOT NULL,
	RefUserCreatedBy int,
	DateTimeCreatedOn datetime,
	RefUserDeletedBy int,
	DateTimeDeletedOn datetime
	)  ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdRelationUserRole] ON [dbo].[RelationUserRole]
	(
	[IdRelationUserRole] ASC	
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
	SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF,
	ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS= ON) 
	ON [PRIMARY]
GO
COMMIT