DROP TABLE IF EXISTS "Addresses";
DROP TABLE IF EXISTS "Users";
DROP TABLE IF EXISTS "SpaceTypes";
DROP TABLE IF EXISTS "Spaces";
DROP TABLE IF EXISTS "SpaceWithTypes";
DROP TABLE IF EXISTS "Images";
DROP TABLE IF EXISTS "DurationUnits";
DROP TABLE IF EXISTS "Prices";
DROP TABLE IF EXISTS "Bookings";
DROP TABLE IF EXISTS "BookingsCalendar";

CREATE TABLE "Addresses" (
	"AddressID" INTEGER PRIMARY KEY,
    "Street" nvarchar (60) NULL ,
    "City" nvarchar (15) NULL ,
    "Region" nvarchar (15) NULL ,
    "PostalCode" nvarchar (10) NULL ,
    "Country" nvarchar (15) NULL ,
    "Phone" nvarchar (24) NULL
);


CREATE TABLE "Users" (
    "UserID" INTEGER PRIMARY KEY,
    "LastName" nvarchar (30) NOT NULL ,
    "FirstName" nvarchar (30) NOT NULL ,
    "Email" nvarchar(100) NOT NULL,
    "AddressID" INTEGER NULL,
    CONSTRAINT "FK_Address" FOREIGN KEY 
    (
        "AddressID"
    ) REFERENCES "Addresses" (
        "AddressID"
    )
);


CREATE TABLE "SpaceTypes" (
	"Type" nvarchar(6) PRIMARY KEY,
	"Label" nvarchar(30) NOT NULL
	);


CREATE TABLE "Spaces" (
    "SpaceID" INTEGER PRIMARY KEY,
    "AddressID" INTEGER NULL,
    "Name" nvarchar (30) NOT NULL ,
    "OwnerID" INTEGER NOT NULL ,
    "Description" nvarchar(4000) NOT NULL,
    "Phone" nvarchar(24),
    "HasSpaceToRent" "bit" NOT NULL CONSTRAINT "DF_Has_Space_to_Rent" DEFAULT (0),
    CONSTRAINT "FK_Address" FOREIGN KEY 
    (
        "AddressID"
    ) REFERENCES "Addresses" (
        "AddressID"
    ),
    CONSTRAINT "FK_Owner" FOREIGN KEY 
    (
        "OwnerID"
    ) REFERENCES "Users" (
        "UserID"
    )
);


CREATE TABLE "SpaceWithTypes" (
	"TypeID" INTEGER PRIMARY KEY,
	"SpaceID" INTEGER NOT NULL,
	"Type" nvarchar(6) NOT NULL,
    CONSTRAINT "FK_SpaceType" FOREIGN KEY 
    (
        "Type"
    ) REFERENCES "SpaceTypes" (
        "Type"
    )
);

CREATE TABLE "Images" (
	"ImageID" INTEGER NOT NULL,
	"FilePath" nvarchar(260) NOT NULL,
    CONSTRAINT "PK_Images" PRIMARY KEY  
    (
        "ImageID",
        "FilePath"
    )
);

CREATE TABLE "DurationUnits" (
	"Unit" nvarchar(6) NOT NULL,
	"Label" nvarchar(24) NOT NULL
);

CREATE TABLE "Prices" (
	"TypeID" INTEGER NOT NULL,
    "Amount" "money" NULL CONSTRAINT "DF_Space_UnitPrice" DEFAULT (0),
    "DurationUnit" nvarchar(6) NOT NULL,
    CONSTRAINT "FK_SpaceWithType" FOREIGN KEY 
    (
        "TypeID"
    ) REFERENCES "SpaceWithTypes" (
        "TypeID"
    ),
    CONSTRAINT "FK_DurationUnit" FOREIGN KEY 
    (
        "DurationUnit"
    ) REFERENCES "DurationUnit" (
        "Unit"
    )
);

CREATE TABLE "Bookings"(
	"BookingID" INTEGER PRIMARY KEY,
	"UserID" INTEGER NOT NULL,
	"TypeID" INTEGER NOT NULL,
	CONSTRAINT "FK_SpaceWithType" FOREIGN KEY 
    (
        "TypeID"
    ) REFERENCES "SpaceWithTypes" (
        "TypeID"
    ),
    CONSTRAINT "FK_User" FOREIGN KEY 
    (
        "UserID"
    ) REFERENCES "Users" (
        "UserID"
    )
);

CREATE TABLE "BookingsCalendar"(
	"BookingID" INTEGER NOT NULL,
	"StartTime" TEXT NOT NULL,
	"EndTime" TEXT NOT NULL,
	CONSTRAINT "FK_Booking" FOREIGN KEY 
    (
        "BookingID"
    ) REFERENCES "Bookings" (
        "BookingID"
    )
);

CREATE INDEX "PostalCodeAddresses" ON "Addresses"("PostalCode");
CREATE INDEX "CityAddresses" ON "Addresses"("City");


-- Dummy data
INSERT INTO "Addresses"("AddressID","Street","City","Region","PostalCode","Country","Phone") 
VALUES('1','722 Moss Bay Blvd.','Kirkland','WA','98033','USA','(206) 555-3412');
INSERT INTO "Addresses"("AddressID","Street","City","Region","PostalCode","Country","Phone") 
VALUES('2','14 Garrett Hill','London',NULL,'SW1 8JR','UK','(71) 555-4848');
INSERT INTO "Addresses"("AddressID","Street","City","Region","PostalCode","Country","Phone") 
VALUES('3','Coventry House','London',NULL,'EC2 7JR','UK','(71) 555-7773');
INSERT INTO "Addresses"("AddressID","Street","City","Region","PostalCode","Country","Phone") 
VALUES('4','4726 - 11th Ave. N.E.','Seattle','WA','98105','USA','(206) 555-1189');
INSERT INTO "Addresses"("AddressID","Street","City","Region","PostalCode","Country","Phone") 
VALUES('5','4110 Old Redmond Rd.','Redmond','WA','98052','USA','(206) 555-8122');


INSERT INTO "Users"("LastName","FirstName","Email","AddressID")
VALUES('NIETSZCHE','Friedrich','zarathoustra@gmail.com','1');
INSERT INTO "Users"("LastName","FirstName","Email","AddressID")
VALUES('CAESAR','Julius','julescesar@gmail.com','2');
INSERT INTO "Users"("LastName","FirstName","Email","AddressID")
VALUES('SPINOZA','Baruch','ethics@gmail.com','3');
INSERT INTO "Users"("LastName","FirstName","Email","AddressID")
VALUES('WHITMAN','Walt','grassofleaves@gmail.com','4');

INSERT INTO "SpaceTypes"("Type","Label")
VALUES('ring','Boxing ring');
INSERT INTO "SpaceTypes"("Type","Label")
VALUES('oct','Octogon');
INSERT INTO "SpaceTypes"("Type","Label")
VALUES('mat','Mat');
INSERT INTO "SpaceTypes"("Type","Label")
VALUES('five','Futsal');

INSERT INTO "Spaces" ("SpaceID","AddressID","Name","OwnerID","Description","Phone")
VALUES('1','5','UFC GYM Chinatown','3',"This gym got an octogon, boxing bags, and some mat space","0134110712");

INSERT INTO "SpaceWithTypes" ("TypeID","SpaceID","Type")
VALUES('1','1','oct');
INSERT INTO "SpaceWithTypes" ("TypeID","SpaceID","Type")
VALUES('2','1','ring');
INSERT INTO "SpaceWithTypes" ("TypeID","SpaceID","Type")
VALUES('3','1','mat');

INSERT INTO "Images" ("ImageID","FilePath")
VALUES('1','images/ufc_1.jpg');
INSERT INTO "Images" ("ImageID","FilePath")
VALUES('1','images/ufc_2.jpg');

INSERT INTO "DurationUnits"
VALUES('min','Minutes');
INSERT INTO "DurationUnits"
VALUES('hour','Hour');
INSERT INTO "DurationUnits"
VALUES('day','Day');
INSERT INTO "DurationUnits"
VALUES('week','Week');
INSERT INTO "DurationUnits"
VALUES('month','Month');

INSERT INTO "Prices" ("TypeID","Amount","DurationUnit")
VALUES('1','20','hr');
INSERT INTO "Prices" ("TypeID","Amount","DurationUnit")
VALUES('2','30','hr');
INSERT INTO "Prices" ("TypeID","Amount","DurationUnit")
VALUES('3','15','hr');

INSERT INTO "Bookings" ("BookingID","UserID","TypeID")
VALUES('1','1','2');

INSERT INTO "BookingsCalendar" ("BookingID","StartTime","EndTime")
VALUES('1',datetime('now','localtime'),datetime('now','localtime'));

