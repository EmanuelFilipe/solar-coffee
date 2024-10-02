SELECT * FROM "Products";

INSERT INTO "Products" 
("CreatedOn", "UpdatedOn", "Name", "Description", "Price", "IsTaxable", "IsArchived") 
Values 
(NOW(), NOW(), '10 LB Dark Roast', '10 LB bag of Dark Roast coffee beans', 100, true, false),
(NOW(), NOW(), '30 LB Dark Roast', '30 LB bag of Dark Roast coffee beans', 248, true, false),
(NOW(), NOW(), '50 LB Dark Roast', '50 LB bag of Dark Roast coffee beans', 450, true, false),
(NOW(), NOW(), '10 LB Light Roast', '10 LB bag of Light Roast coffee beans', 100, true, false),
(NOW(), NOW(), '30 LB Light Roast', '30 LB bag of Light Roast coffee beans', 280, true, false),
(NOW(), NOW(), '50 LB Light Roast', '50 LB bag of Light Roast coffee beans', 450, true, false);