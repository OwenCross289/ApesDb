CREATE TABLE IF NOT EXISTS "public"."Teams" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "OwnerUserId" uuid NOT NULL,
    "Name" character varying(256) NOT NULL,
    "ProfilePictureUrl" character varying(2048),
    "Kind" character varying(32) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Teams" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Teams_Users_OwnerUserId" FOREIGN KEY ("OwnerUserId")
        REFERENCES "public"."Users" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_Teams_Kind" CHECK ("Kind" IN ('Solo', 'Group'))
);

CREATE INDEX IF NOT EXISTS "IX_Teams_OwnerUserId" ON "public"."Teams" ("OwnerUserId");
CREATE UNIQUE INDEX IF NOT EXISTS "UX_Teams_OwnerUserId_Solo"
    ON "public"."Teams" ("OwnerUserId") WHERE "Kind" = 'Solo';

-- Backfill: one solo team per existing user.
INSERT INTO "public"."Teams" ("OwnerUserId", "Name", "Kind")
SELECT u."Id",
       CASE WHEN btrim(u."Name") = '' THEN 'Solo Team' ELSE btrim(u."Name") || '''s Team' END,
       'Solo'
FROM "public"."Users" u
WHERE NOT EXISTS (
    SELECT 1 FROM "public"."Teams" t
    WHERE t."OwnerUserId" = u."Id" AND t."Kind" = 'Solo'
);
