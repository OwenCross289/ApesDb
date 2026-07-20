CREATE TABLE IF NOT EXISTS "public"."Profiles" (
    "UserId" uuid NOT NULL,
    "AboutMe" character varying(4000),
    "IsPublic" boolean NOT NULL DEFAULT false,
    CONSTRAINT "PK_Profiles" PRIMARY KEY ("UserId"),
    CONSTRAINT "FK_Profiles_Users_UserId" FOREIGN KEY ("UserId")
        REFERENCES "public"."Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_Profiles_IsPublic" ON "public"."Profiles" ("IsPublic");

INSERT INTO "public"."Profiles" ("UserId")
SELECT u."Id"
FROM "public"."Users" u
ON CONFLICT ("UserId") DO NOTHING;
