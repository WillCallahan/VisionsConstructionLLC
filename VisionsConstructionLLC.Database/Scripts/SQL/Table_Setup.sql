DROP TABLE [ItemImage];
DROP TABLE [Item];
DROP TABLE [Gallery_Category];
DROP TABLE [Email];

CREATE TABLE [User] (
	[User_Id] INT NOT NULL PRIMARY KEY, 
	[User_Username] VARCHAR(100) NOT NULL, 
	[User_Password] VARCHAR(100) NOT NULL, 
	[User_Active_Status] INT NOT NULL DEFAULT (1)
);

CREATE TABLE [Email] (
	[Email_Id]            INT           IDENTITY (1, 1) NOT NULL,
	[Email_Name]          VARCHAR (40)  NOT NULL,
	[Email_Address]       VARCHAR (50)  NOT NULL,
	[Email_Phone]         VARCHAR (50)  NULL,
	[Email_Message]       VARCHAR (MAX) NULL,
	[Email_Active_Status] INT           DEFAULT ((1)) NOT NULL,
	[Email_Date] DATETIME NOT NULL, 
	PRIMARY KEY CLUSTERED ([Email_Id] ASC)
);

CREATE TABLE [Gallery_Category] (
	[Gallery_Category_Id]            INT          IDENTITY (1, 1) NOT NULL,
	[Gallery_Category_Name]          VARCHAR (50) NULL,
	[Gallery_Category_Active_Status] INT          DEFAULT ((1)) NOT NULL,
	PRIMARY KEY CLUSTERED ([Gallery_Category_Id] ASC)
);

CREATE TABLE [Item] (
	[Item_Id]                  INT             IDENTITY (1, 1) NOT NULL,
	[Item_Gallery_Category_Id] INT             NOT NULL,
	[Item_Description]         VARBINARY (MAX) NULL,
	[Item_Active_Status]       INT             DEFAULT ((1)) NOT NULL,
	[Item_Title]               VARCHAR (50)    NOT NULL,
	[Item_Showcase_Description] VARCHAR(255) NULL, 
	PRIMARY KEY CLUSTERED ([Item_Id] ASC),
	CONSTRAINT [FK_Item_To_Gallery] FOREIGN KEY ([Item_Gallery_Category_Id]) REFERENCES [Gallery_Category] ([Gallery_Category_Id])
);

CREATE TABLE [Item_Image] (
	[Item_Image_Id]            INT             IDENTITY (1, 1) NOT NULL,
	[Item_Image_Image]         VARBINARY (MAX) NULL,
	[Item_Image_Item_Id]       INT             NOT NULL,
	[Item_Image_Active_Status] INT             DEFAULT ((1)) NOT NULL,
	[Item_Image_Meme_Type] VARCHAR(50) NOT NULL, 
	PRIMARY KEY CLUSTERED ([Item_Image_Id] ASC),
	CONSTRAINT [FK_Item_Image_Item] FOREIGN KEY ([Item_Image_Item_Id]) REFERENCES [Item] ([Item_Id])
);