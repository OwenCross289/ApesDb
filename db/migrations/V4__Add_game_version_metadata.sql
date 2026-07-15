ALTER TABLE public."Games"
    ADD COLUMN "VersionParentId" bigint NULL;

CREATE INDEX "IX_Games_VersionParentId"
    ON public."Games" ("VersionParentId");
