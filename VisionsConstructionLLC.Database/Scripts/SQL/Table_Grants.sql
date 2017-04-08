CREATE SYNONYM [ItemImage] FOR [visions_construction_llc].[dbo].[ItemImage];
CREATE SYNONYM [Item] FOR [visions_construction_llc].[dbo].[Item];
CREATE SYNONYM [Gallery_Category] FOR [visions_construction_llc].[dbo].[Gallery_Category];
CREATE SYNONYM [Email] FOR [visions_construction_llc].[dbo].[Email];
CREATE SYNONYM [User] FOR [visions_construction_llc].[dbo].[User];

GRANT SELECT, INSERT, UPDATE ON [ItemImage] TO visions_construction_llc_user;
GRANT SELECT, INSERT, UPDATE ON [Item] TO visions_construction_llc_user;
GRANT SELECT, INSERT, UPDATE ON [Gallery_Category] TO visions_construction_llc_user;
GRANT SELECT, INSERT, UPDATE ON [Email] TO visions_construction_llc_user;
GRANT SELECT, INSERT, UPDATE ON [User] TO visions_construction_llc_user;