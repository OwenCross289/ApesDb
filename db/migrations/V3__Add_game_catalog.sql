-- IGDB lookup resources use their upstream bigint identifiers directly.
CREATE TABLE IF NOT EXISTS "public"."GameTypes" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameTypes" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."GameStatuses" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameStatuses" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."Genres" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Slug" character varying(256),
    "IgdbUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Genres" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."Themes" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Slug" character varying(256),
    "IgdbUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Themes" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."GameModes" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Slug" character varying(256),
    "IgdbUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameModes" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."PlayerPerspectives" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Slug" character varying(256),
    "IgdbUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PlayerPerspectives" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."PlatformTypes" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PlatformTypes" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."WebsiteTypes" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_WebsiteTypes" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."PopularityTypes" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "ExternalPopularitySourceId" bigint,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PopularityTypes" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."ExternalGameSources" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_ExternalGameSources" PRIMARY KEY ("Id")
);

CREATE INDEX IF NOT EXISTS "IX_PopularityTypes_ExternalPopularitySourceId"
    ON "public"."PopularityTypes" ("ExternalPopularitySourceId");

CREATE TABLE IF NOT EXISTS "public"."Companies" (
    "Id" bigint NOT NULL,
    "Name" character varying(512) NOT NULL,
    "Slug" character varying(512),
    "Description" text,
    "CountryCode" integer,
    "IgdbUrl" character varying(2048),
    "LogoImageId" character varying(128),
    "LogoWidth" integer,
    "LogoHeight" integer,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Companies" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_Companies_LogoWidth" CHECK ("LogoWidth" IS NULL OR "LogoWidth" > 0),
    CONSTRAINT "CK_Companies_LogoHeight" CHECK ("LogoHeight" IS NULL OR "LogoHeight" > 0)
);

CREATE TABLE IF NOT EXISTS "public"."Collections" (
    "Id" bigint NOT NULL,
    "Name" character varying(512) NOT NULL,
    "Slug" character varying(512),
    "IgdbUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Collections" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "public"."Franchises" (
    "Id" bigint NOT NULL,
    "Name" character varying(512) NOT NULL,
    "Slug" character varying(512),
    "IgdbUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Franchises" PRIMARY KEY ("Id")
);

-- Platforms keep their logo data embedded and reference normalized platform types.
CREATE TABLE IF NOT EXISTS "public"."Platforms" (
    "Id" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Abbreviation" character varying(128),
    "AlternativeName" character varying(256),
    "Slug" character varying(256),
    "Summary" text,
    "IgdbUrl" character varying(2048),
    "PlatformTypeId" bigint,
    "Generation" integer,
    "LogoImageId" character varying(128),
    "LogoWidth" integer,
    "LogoHeight" integer,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Platforms" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Platforms_PlatformTypes_PlatformTypeId"
        FOREIGN KEY ("PlatformTypeId") REFERENCES "public"."PlatformTypes" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "CK_Platforms_Generation" CHECK ("Generation" IS NULL OR "Generation" >= 0),
    CONSTRAINT "CK_Platforms_LogoWidth" CHECK ("LogoWidth" IS NULL OR "LogoWidth" > 0),
    CONSTRAINT "CK_Platforms_LogoHeight" CHECK ("LogoHeight" IS NULL OR "LogoHeight" > 0)
);

CREATE INDEX IF NOT EXISTS "IX_Platforms_PlatformTypeId" ON "public"."Platforms" ("PlatformTypeId");

CREATE TABLE IF NOT EXISTS "public"."PlatformLinks" (
    "Id" bigint NOT NULL,
    "PlatformId" bigint NOT NULL,
    "WebsiteTypeId" bigint NOT NULL,
    "Url" character varying(2048) NOT NULL,
    "Trusted" boolean NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PlatformLinks" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PlatformLinks_Platforms_PlatformId"
        FOREIGN KEY ("PlatformId") REFERENCES "public"."Platforms" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PlatformLinks_WebsiteTypes_WebsiteTypeId"
        FOREIGN KEY ("WebsiteTypeId") REFERENCES "public"."WebsiteTypes" ("Id") ON DELETE RESTRICT
);

CREATE INDEX IF NOT EXISTS "IX_PlatformLinks_PlatformId" ON "public"."PlatformLinks" ("PlatformId");
CREATE INDEX IF NOT EXISTS "IX_PlatformLinks_WebsiteTypeId" ON "public"."PlatformLinks" ("WebsiteTypeId");

-- Every IGDB game is stored, with its cover data embedded in the row.
CREATE TABLE IF NOT EXISTS "public"."Games" (
    "Id" bigint NOT NULL,
    "Name" character varying(512) NOT NULL,
    "Slug" character varying(512),
    "Summary" text,
    "Storyline" text,
    "FirstReleaseDate" timestamp with time zone,
    "TotalRating" numeric(8,4),
    "TotalRatingCount" bigint,
    "IgdbUrl" character varying(2048),
    "GameTypeId" bigint,
    "GameStatusId" bigint,
    "CoverImageId" character varying(128),
    "CoverWidth" integer,
    "CoverHeight" integer,
    "CoverSmallUrl" character varying(2048),
    "CoverLargeUrl" character varying(2048),
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Games" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Games_GameTypes_GameTypeId"
        FOREIGN KEY ("GameTypeId") REFERENCES "public"."GameTypes" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Games_GameStatuses_GameStatusId"
        FOREIGN KEY ("GameStatusId") REFERENCES "public"."GameStatuses" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "CK_Games_TotalRating" CHECK ("TotalRating" IS NULL OR ("TotalRating" >= 0 AND "TotalRating" <= 100)),
    CONSTRAINT "CK_Games_TotalRatingCount" CHECK ("TotalRatingCount" IS NULL OR "TotalRatingCount" >= 0),
    CONSTRAINT "CK_Games_CoverWidth" CHECK ("CoverWidth" IS NULL OR "CoverWidth" > 0),
    CONSTRAINT "CK_Games_CoverHeight" CHECK ("CoverHeight" IS NULL OR "CoverHeight" > 0)
);

CREATE INDEX IF NOT EXISTS "IX_Games_GameTypeId" ON "public"."Games" ("GameTypeId");
CREATE INDEX IF NOT EXISTS "IX_Games_GameStatusId" ON "public"."Games" ("GameStatusId");

-- Popularity rows retain the upstream primitive ID while ranks form a replaceable local snapshot.
CREATE TABLE IF NOT EXISTS "public"."PopularGames" (
    "Id" bigint NOT NULL,
    "GameId" bigint NOT NULL,
    "Rank" integer NOT NULL,
    "SourceRank" integer NOT NULL,
    "Score" numeric(28,18) NOT NULL,
    "PopularityTypeId" bigint NOT NULL,
    "CalculatedAt" timestamp with time zone NOT NULL,
    "IgdbUpdatedAt" timestamp with time zone,
    "Checksum" uuid,
    "SyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PopularGames" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PopularGames_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PopularGames_PopularityTypes_PopularityTypeId"
        FOREIGN KEY ("PopularityTypeId") REFERENCES "public"."PopularityTypes" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "CK_PopularGames_Rank" CHECK ("Rank" BETWEEN 1 AND 1000),
    CONSTRAINT "CK_PopularGames_SourceRank" CHECK ("SourceRank" > 0),
    CONSTRAINT "CK_PopularGames_Score" CHECK ("Score" >= 0)
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_PopularGames_GameId" ON "public"."PopularGames" ("GameId");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_PopularGames_Rank" ON "public"."PopularGames" ("Rank");
CREATE INDEX IF NOT EXISTS "IX_PopularGames_PopularityTypeId" ON "public"."PopularGames" ("PopularityTypeId");

CREATE TABLE IF NOT EXISTS "public"."GameRelations" (
    "GameId" bigint NOT NULL,
    "RelatedGameId" bigint NOT NULL,
    "RelationType" character varying(32) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameRelations" PRIMARY KEY ("GameId", "RelatedGameId", "RelationType"),
    CONSTRAINT "FK_GameRelations_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameRelations_Games_RelatedGameId"
        FOREIGN KEY ("RelatedGameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_GameRelations_DifferentGames" CHECK ("GameId" <> "RelatedGameId"),
    CONSTRAINT "CK_GameRelations_RelationType"
        CHECK ("RelationType" IN ('Dlc', 'Expansion', 'StandaloneExpansion'))
);

CREATE INDEX IF NOT EXISTS "IX_GameRelations_RelatedGameId" ON "public"."GameRelations" ("RelatedGameId");

-- Taxonomy, platform, collection, and franchise relations are authoritative composite joins.
CREATE TABLE IF NOT EXISTS "public"."GameGenres" (
    "GameId" bigint NOT NULL,
    "GenreId" bigint NOT NULL,
    CONSTRAINT "PK_GameGenres" PRIMARY KEY ("GameId", "GenreId"),
    CONSTRAINT "FK_GameGenres_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameGenres_Genres_GenreId"
        FOREIGN KEY ("GenreId") REFERENCES "public"."Genres" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameGenres_GenreId" ON "public"."GameGenres" ("GenreId");

CREATE TABLE IF NOT EXISTS "public"."GameThemes" (
    "GameId" bigint NOT NULL,
    "ThemeId" bigint NOT NULL,
    CONSTRAINT "PK_GameThemes" PRIMARY KEY ("GameId", "ThemeId"),
    CONSTRAINT "FK_GameThemes_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameThemes_Themes_ThemeId"
        FOREIGN KEY ("ThemeId") REFERENCES "public"."Themes" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameThemes_ThemeId" ON "public"."GameThemes" ("ThemeId");

CREATE TABLE IF NOT EXISTS "public"."GameGameModes" (
    "GameId" bigint NOT NULL,
    "GameModeId" bigint NOT NULL,
    CONSTRAINT "PK_GameGameModes" PRIMARY KEY ("GameId", "GameModeId"),
    CONSTRAINT "FK_GameGameModes_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameGameModes_GameModes_GameModeId"
        FOREIGN KEY ("GameModeId") REFERENCES "public"."GameModes" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameGameModes_GameModeId" ON "public"."GameGameModes" ("GameModeId");

CREATE TABLE IF NOT EXISTS "public"."GamePlayerPerspectives" (
    "GameId" bigint NOT NULL,
    "PlayerPerspectiveId" bigint NOT NULL,
    CONSTRAINT "PK_GamePlayerPerspectives" PRIMARY KEY ("GameId", "PlayerPerspectiveId"),
    CONSTRAINT "FK_GamePlayerPerspectives_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GamePlayerPerspectives_PlayerPerspective"
        FOREIGN KEY ("PlayerPerspectiveId") REFERENCES "public"."PlayerPerspectives" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GamePlayerPerspectives_PlayerPerspectiveId"
    ON "public"."GamePlayerPerspectives" ("PlayerPerspectiveId");

CREATE TABLE IF NOT EXISTS "public"."GamePlatforms" (
    "GameId" bigint NOT NULL,
    "PlatformId" bigint NOT NULL,
    CONSTRAINT "PK_GamePlatforms" PRIMARY KEY ("GameId", "PlatformId"),
    CONSTRAINT "FK_GamePlatforms_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GamePlatforms_Platforms_PlatformId"
        FOREIGN KEY ("PlatformId") REFERENCES "public"."Platforms" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GamePlatforms_PlatformId" ON "public"."GamePlatforms" ("PlatformId");

CREATE TABLE IF NOT EXISTS "public"."GameCollections" (
    "GameId" bigint NOT NULL,
    "CollectionId" bigint NOT NULL,
    CONSTRAINT "PK_GameCollections" PRIMARY KEY ("GameId", "CollectionId"),
    CONSTRAINT "FK_GameCollections_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameCollections_Collections_CollectionId"
        FOREIGN KEY ("CollectionId") REFERENCES "public"."Collections" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameCollections_CollectionId" ON "public"."GameCollections" ("CollectionId");

CREATE TABLE IF NOT EXISTS "public"."GameFranchises" (
    "GameId" bigint NOT NULL,
    "FranchiseId" bigint NOT NULL,
    CONSTRAINT "PK_GameFranchises" PRIMARY KEY ("GameId", "FranchiseId"),
    CONSTRAINT "FK_GameFranchises_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameFranchises_Franchises_FranchiseId"
        FOREIGN KEY ("FranchiseId") REFERENCES "public"."Franchises" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameFranchises_FranchiseId" ON "public"."GameFranchises" ("FranchiseId");

-- Dependent IGDB resources also use upstream row IDs as their primary keys.
CREATE TABLE IF NOT EXISTS "public"."GameCompanies" (
    "Id" bigint NOT NULL,
    "GameId" bigint NOT NULL,
    "CompanyId" bigint NOT NULL,
    "Developer" boolean NOT NULL,
    "Publisher" boolean NOT NULL,
    "Porting" boolean NOT NULL,
    "Supporting" boolean NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameCompanies" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_GameCompanies_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameCompanies_Companies_CompanyId"
        FOREIGN KEY ("CompanyId") REFERENCES "public"."Companies" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_GameCompanies_Role" CHECK ("Developer" OR "Publisher" OR "Porting" OR "Supporting")
);

CREATE INDEX IF NOT EXISTS "IX_GameCompanies_GameId" ON "public"."GameCompanies" ("GameId");
CREATE INDEX IF NOT EXISTS "IX_GameCompanies_CompanyId" ON "public"."GameCompanies" ("CompanyId");

CREATE TABLE IF NOT EXISTS "public"."ExternalGames" (
    "Id" bigint NOT NULL,
    "GameId" bigint NOT NULL,
    "ExternalGameSourceId" bigint NOT NULL,
    "PlatformId" bigint,
    "ExternalId" character varying(512),
    "Url" character varying(2048),
    "Name" character varying(512),
    "Year" integer,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_ExternalGames" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ExternalGames_Games_GameId"
        FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ExternalGames_ExternalGameSources_ExternalGameSourceId"
        FOREIGN KEY ("ExternalGameSourceId") REFERENCES "public"."ExternalGameSources" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_ExternalGames_Platforms_PlatformId"
        FOREIGN KEY ("PlatformId") REFERENCES "public"."Platforms" ("Id") ON DELETE SET NULL,
    CONSTRAINT "CK_ExternalGames_Year" CHECK ("Year" IS NULL OR "Year" BETWEEN 0 AND 9999)
);

CREATE INDEX IF NOT EXISTS "IX_ExternalGames_GameId" ON "public"."ExternalGames" ("GameId");
CREATE INDEX IF NOT EXISTS "IX_ExternalGames_ExternalGameSourceId"
    ON "public"."ExternalGames" ("ExternalGameSourceId");
CREATE INDEX IF NOT EXISTS "IX_ExternalGames_PlatformId" ON "public"."ExternalGames" ("PlatformId");
CREATE INDEX IF NOT EXISTS "IX_ExternalGames_ExternalGameSourceId_ExternalId"
    ON "public"."ExternalGames" ("ExternalGameSourceId", "ExternalId");

-- Catalog synchronization state is operational data and therefore retains UUID identifiers.
CREATE TABLE IF NOT EXISTS "public"."IgdbSyncRuns" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "CatalogLock" integer NOT NULL DEFAULT 1,
    "Mode" character varying(32) NOT NULL,
    "Status" character varying(32) NOT NULL DEFAULT 'Pending',
    "From" timestamp with time zone,
    "Through" timestamp with time zone NOT NULL,
    "RowsProcessed" bigint NOT NULL DEFAULT 0,
    "RowsSkipped" bigint NOT NULL DEFAULT 0,
    "Error" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "StartedAt" timestamp with time zone,
    "CompletedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_IgdbSyncRuns" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_IgdbSyncRuns_CatalogLock" CHECK ("CatalogLock" = 1),
    CONSTRAINT "CK_IgdbSyncRuns_Mode" CHECK ("Mode" IN ('Bootstrap', 'Incremental')),
    CONSTRAINT "CK_IgdbSyncRuns_Status" CHECK ("Status" IN ('Pending', 'Running', 'Failed', 'Succeeded')),
    CONSTRAINT "CK_IgdbSyncRuns_Window" CHECK (
        ("Mode" = 'Bootstrap' AND "From" IS NULL)
        OR ("Mode" = 'Incremental' AND "From" IS NOT NULL AND "From" < "Through")
    ),
    CONSTRAINT "CK_IgdbSyncRuns_RowsProcessed" CHECK ("RowsProcessed" >= 0),
    CONSTRAINT "CK_IgdbSyncRuns_RowsSkipped" CHECK ("RowsSkipped" >= 0),
    CONSTRAINT "CK_IgdbSyncRuns_Completion"
        CHECK (("Status" = 'Succeeded' AND "CompletedAt" IS NOT NULL) OR "Status" <> 'Succeeded')
);

-- A failed run remains unfinished so it must be resumed with the same captured window.
CREATE UNIQUE INDEX IF NOT EXISTS "UX_IgdbSyncRuns_Unfinished"
    ON "public"."IgdbSyncRuns" ("CatalogLock")
    WHERE "Status" <> 'Succeeded';

CREATE INDEX IF NOT EXISTS "IX_IgdbSyncRuns_Status_Through"
    ON "public"."IgdbSyncRuns" ("Status", "Through");

CREATE TABLE IF NOT EXISTS "public"."IgdbSyncStages" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "RunId" uuid NOT NULL,
    "Kind" character varying(64) NOT NULL,
    "Order" integer NOT NULL,
    "Status" character varying(32) NOT NULL DEFAULT 'Pending',
    "PageCursor" bigint NOT NULL DEFAULT -1,
    "PagesProcessed" integer NOT NULL DEFAULT 0,
    "RowsProcessed" bigint NOT NULL DEFAULT 0,
    "RowsSkipped" bigint NOT NULL DEFAULT 0,
    "Error" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "StartedAt" timestamp with time zone,
    "CompletedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_IgdbSyncStages" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_IgdbSyncStages_IgdbSyncRuns_RunId"
        FOREIGN KEY ("RunId") REFERENCES "public"."IgdbSyncRuns" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_IgdbSyncStages_Order" CHECK ("Order" >= 0),
    CONSTRAINT "CK_IgdbSyncStages_Status" CHECK ("Status" IN ('Pending', 'Running', 'Failed', 'Succeeded')),
    CONSTRAINT "CK_IgdbSyncStages_PageCursor" CHECK ("PageCursor" >= -1),
    CONSTRAINT "CK_IgdbSyncStages_PagesProcessed" CHECK ("PagesProcessed" >= 0),
    CONSTRAINT "CK_IgdbSyncStages_RowsProcessed" CHECK ("RowsProcessed" >= 0),
    CONSTRAINT "CK_IgdbSyncStages_RowsSkipped" CHECK ("RowsSkipped" >= 0),
    CONSTRAINT "CK_IgdbSyncStages_Completion"
        CHECK (("Status" = 'Succeeded' AND "CompletedAt" IS NOT NULL) OR "Status" <> 'Succeeded')
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_IgdbSyncStages_RunId_Kind"
    ON "public"."IgdbSyncStages" ("RunId", "Kind");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_IgdbSyncStages_RunId_Order"
    ON "public"."IgdbSyncStages" ("RunId", "Order");
CREATE INDEX IF NOT EXISTS "IX_IgdbSyncStages_Status" ON "public"."IgdbSyncStages" ("Status");

CREATE TABLE IF NOT EXISTS "public"."IgdbSyncSkippedRows" (
    "StageId" uuid NOT NULL,
    "EntityId" bigint NOT NULL,
    "Reason" character varying(64) NOT NULL,
    "MissingDependencyId" bigint NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_IgdbSyncSkippedRows"
        PRIMARY KEY ("StageId", "EntityId", "Reason", "MissingDependencyId"),
    CONSTRAINT "FK_IgdbSyncSkippedRows_IgdbSyncStages_StageId"
        FOREIGN KEY ("StageId") REFERENCES "public"."IgdbSyncStages" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_IgdbSyncSkippedRows_Reason"
        CHECK ("Reason" IN ('MissingGame', 'MissingCompany', 'MissingExternalGameSource', 'MissingPlatform'))
);

CREATE INDEX IF NOT EXISTS "IX_IgdbSyncSkippedRows_EntityId"
    ON "public"."IgdbSyncSkippedRows" ("EntityId");

-- Relation staging is run-scoped and deliberately has no game FK: related games may
-- not exist until a later committed page of the same catalog scan.
CREATE TABLE IF NOT EXISTS "public"."IgdbSyncTouchedRelationParents" (
    "RunId" uuid NOT NULL,
    "GameId" bigint NOT NULL,
    "RelationType" character varying(32) NOT NULL,
    CONSTRAINT "PK_IgdbSyncTouchedRelationParents" PRIMARY KEY ("RunId", "GameId", "RelationType"),
    CONSTRAINT "FK_IgdbSyncTouchedRelationParents_IgdbSyncRuns_RunId"
        FOREIGN KEY ("RunId") REFERENCES "public"."IgdbSyncRuns" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_IgdbSyncTouchedRelationParents_RelationType"
        CHECK ("RelationType" IN ('Dlc', 'Expansion', 'StandaloneExpansion'))
);

CREATE INDEX IF NOT EXISTS "IX_IgdbSyncTouchedRelationParents_GameId"
    ON "public"."IgdbSyncTouchedRelationParents" ("GameId");

CREATE TABLE IF NOT EXISTS "public"."IgdbSyncPendingGameRelations" (
    "RunId" uuid NOT NULL,
    "GameId" bigint NOT NULL,
    "RelatedGameId" bigint NOT NULL,
    "RelationType" character varying(32) NOT NULL,
    CONSTRAINT "PK_IgdbSyncPendingGameRelations"
        PRIMARY KEY ("RunId", "GameId", "RelatedGameId", "RelationType"),
    CONSTRAINT "FK_IgdbSyncPendingGameRelations_IgdbSyncRuns_RunId"
        FOREIGN KEY ("RunId") REFERENCES "public"."IgdbSyncRuns" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_IgdbSyncPendingGameRelations_DifferentGames" CHECK ("GameId" <> "RelatedGameId"),
    CONSTRAINT "CK_IgdbSyncPendingGameRelations_RelationType"
        CHECK ("RelationType" IN ('Dlc', 'Expansion', 'StandaloneExpansion'))
);

CREATE INDEX IF NOT EXISTS "IX_IgdbSyncPendingGameRelations_RunId_RelatedGameId"
    ON "public"."IgdbSyncPendingGameRelations" ("RunId", "RelatedGameId");
