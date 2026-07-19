CREATE TABLE IF NOT EXISTS "public"."AllowedUsers" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "Email" character varying(256) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_AllowedUsers" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_AllowedUsers_Email_Normalized" CHECK (
        "Email" <> ''
        AND "Email" = btrim("Email")
        AND "Email" = lower("Email")
    )
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_AllowedUsers_Email" ON "public"."AllowedUsers" ("Email");
