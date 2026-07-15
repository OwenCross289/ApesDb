ALTER TABLE public."Games"
    ADD COLUMN "VersionParentId" bigint NULL;

CREATE INDEX "IX_Games_VersionParentId"
    ON public."Games" ("VersionParentId");

ALTER TABLE public."IgdbSyncRuns"
    ADD COLUMN "CatalogVersion" integer NOT NULL DEFAULT 1;

ALTER TABLE public."IgdbSyncRuns"
    ADD CONSTRAINT "CK_IgdbSyncRuns_CatalogVersion"
    CHECK ("CatalogVersion" > 0);
