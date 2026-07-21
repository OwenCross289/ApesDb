ALTER TABLE "public"."IgdbSyncRuns"
    DROP CONSTRAINT "CK_IgdbSyncRuns_Status";

ALTER TABLE "public"."IgdbSyncRuns"
    ADD CONSTRAINT "CK_IgdbSyncRuns_Status"
        CHECK ("Status" IN ('Pending', 'Running', 'Failed', 'Succeeded', 'Superseded'));

ALTER TABLE "public"."IgdbSyncRuns"
    DROP CONSTRAINT "CK_IgdbSyncRuns_Completion";

ALTER TABLE "public"."IgdbSyncRuns"
    ADD CONSTRAINT "CK_IgdbSyncRuns_Completion"
        CHECK (
            ("Status" IN ('Succeeded', 'Superseded') AND "CompletedAt" IS NOT NULL)
            OR "Status" NOT IN ('Succeeded', 'Superseded')
        );

DROP INDEX "public"."UX_IgdbSyncRuns_Unfinished";

CREATE UNIQUE INDEX "UX_IgdbSyncRuns_Unfinished"
    ON "public"."IgdbSyncRuns" ("CatalogLock")
    WHERE "Status" NOT IN ('Succeeded', 'Superseded');
