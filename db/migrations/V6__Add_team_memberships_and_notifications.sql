ALTER TABLE "public"."Teams"
    ADD COLUMN IF NOT EXISTS "ProfilePicture" bytea;

ALTER TABLE "public"."Teams"
    DROP COLUMN IF EXISTS "ProfilePictureUrl";

CREATE TABLE IF NOT EXISTS "public"."TeamMemberships" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "TeamId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Status" integer NOT NULL,
    "InvitedByUserId" uuid,
    "InvitedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "AcceptedAt" timestamp with time zone,
    CONSTRAINT "PK_TeamMemberships" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TeamMemberships_Teams_TeamId" FOREIGN KEY ("TeamId")
        REFERENCES "public"."Teams" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TeamMemberships_Users_UserId" FOREIGN KEY ("UserId")
        REFERENCES "public"."Users" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_TeamMemberships_Users_InvitedByUserId" FOREIGN KEY ("InvitedByUserId")
        REFERENCES "public"."Users" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "CK_TeamMemberships_Status" CHECK ("Status" IN (0, 1)),
    CONSTRAINT "CK_TeamMemberships_Acceptance" CHECK (
        ("Status" = 0 AND "AcceptedAt" IS NULL)
        OR ("Status" = 1 AND "AcceptedAt" IS NOT NULL)
    )
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_TeamMemberships_TeamId_UserId"
    ON "public"."TeamMemberships" ("TeamId", "UserId");
CREATE INDEX IF NOT EXISTS "IX_TeamMemberships_UserId_Status"
    ON "public"."TeamMemberships" ("UserId", "Status");

INSERT INTO "public"."TeamMemberships" (
    "Id", "TeamId", "UserId", "Status", "InvitedByUserId", "InvitedAt", "AcceptedAt"
)
SELECT uuidv7(), t."Id", t."OwnerUserId", 1, NULL, t."CreatedAt", t."CreatedAt"
FROM "public"."Teams" t
ON CONFLICT ("TeamId", "UserId") DO NOTHING;

CREATE TABLE IF NOT EXISTS "public"."Notifications" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "UserId" uuid NOT NULL,
    "Type" integer NOT NULL,
    "ResourceId" uuid NOT NULL,
    "IsActionable" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "ReadAt" timestamp with time zone,
    "ResolvedAt" timestamp with time zone,
    CONSTRAINT "PK_Notifications" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Notifications_Users_UserId" FOREIGN KEY ("UserId")
        REFERENCES "public"."Users" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "CK_Notifications_Type" CHECK ("Type" IN (0))
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Notifications_UserId_Type_ResourceId"
    ON "public"."Notifications" ("UserId", "Type", "ResourceId");
CREATE INDEX IF NOT EXISTS "IX_Notifications_UserId_ResolvedAt_CreatedAt"
    ON "public"."Notifications" ("UserId", "ResolvedAt", "CreatedAt");
