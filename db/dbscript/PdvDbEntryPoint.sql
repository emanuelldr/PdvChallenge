CREATE EXTENSION postgis;

CREATE TABLE "Pdv" (
          id serial NOT NULL,
          "tradingName" text NULL,
          "ownerName" text NULL,
          document text NULL,
          "coverageArea" geometry NULL,
          address geometry NULL,
          CONSTRAINT "PK_Pdv" PRIMARY KEY (id)
      );