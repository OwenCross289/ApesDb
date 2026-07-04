CREATE TABLE IF NOT EXISTS "public"."Users" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "Auth0Subject" character varying(256) NOT NULL,
    "Email" character varying(256) NOT NULL,
    "Name" character varying(256) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Users_Auth0Subject" ON "public"."Users" ("Auth0Subject");
CREATE INDEX IF NOT EXISTS "IX_Users_Email" ON "public"."Users" ("Email");
