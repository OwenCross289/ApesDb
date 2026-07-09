CREATE TABLE IF NOT EXISTS "public"."GameTypes" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameTypes" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_GameTypes_IgdbId" ON "public"."GameTypes" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameStatuses" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameStatuses" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_GameStatuses_IgdbId" ON "public"."GameStatuses" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."Games" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "Name" character varying(512) NOT NULL,
    "Slug" character varying(512),
    "Summary" text,
    "Storyline" text,
    "FirstReleaseDate" timestamp with time zone,
    "TotalRating" numeric(8,4),
    "TotalRatingCount" bigint,
    "IgdbUrl" character varying(2048),
    "GameTypeId" uuid,
    "GameStatusId" uuid,
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
    CONSTRAINT "FK_Games_GameTypes_GameTypeId" FOREIGN KEY ("GameTypeId") REFERENCES "public"."GameTypes" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Games_GameStatuses_GameStatusId" FOREIGN KEY ("GameStatusId") REFERENCES "public"."GameStatuses" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "CK_Games_TotalRating" CHECK ("TotalRating" IS NULL OR ("TotalRating" >= 0 AND "TotalRating" <= 100)),
    CONSTRAINT "CK_Games_TotalRatingCount" CHECK ("TotalRatingCount" IS NULL OR "TotalRatingCount" >= 0),
    CONSTRAINT "CK_Games_CoverWidth" CHECK ("CoverWidth" IS NULL OR "CoverWidth" > 0),
    CONSTRAINT "CK_Games_CoverHeight" CHECK ("CoverHeight" IS NULL OR "CoverHeight" > 0)
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Games_IgdbId" ON "public"."Games" ("IgdbId");
CREATE INDEX IF NOT EXISTS "IX_Games_GameTypeId" ON "public"."Games" ("GameTypeId");
CREATE INDEX IF NOT EXISTS "IX_Games_GameStatusId" ON "public"."Games" ("GameStatusId");

CREATE TABLE IF NOT EXISTS "public"."PopularGames" (
    "GameId" uuid NOT NULL,
    "Rank" integer NOT NULL,
    "SourceRank" integer NOT NULL,
    "Score" numeric(28,18) NOT NULL,
    "IgdbPopularityTypeId" bigint NOT NULL,
    "CalculatedAt" timestamp with time zone NOT NULL,
    "IgdbUpdatedAt" timestamp with time zone,
    "Checksum" uuid,
    "SyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PopularGames" PRIMARY KEY ("GameId"),
    CONSTRAINT "FK_PopularGames_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_PopularGames_Rank" CHECK ("Rank" BETWEEN 1 AND 1000),
    CONSTRAINT "CK_PopularGames_SourceRank" CHECK ("SourceRank" > 0),
    CONSTRAINT "CK_PopularGames_Score" CHECK ("Score" >= 0)
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_PopularGames_Rank" ON "public"."PopularGames" ("Rank");

CREATE TABLE IF NOT EXISTS "public"."GameRelations" (
    "GameId" uuid NOT NULL,
    "RelatedGameId" uuid NOT NULL,
    "RelationType" character varying(32) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameRelations" PRIMARY KEY ("GameId", "RelatedGameId", "RelationType"),
    CONSTRAINT "FK_GameRelations_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameRelations_Games_RelatedGameId" FOREIGN KEY ("RelatedGameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_GameRelations_DifferentGames" CHECK ("GameId" <> "RelatedGameId"),
    CONSTRAINT "CK_GameRelations_RelationType" CHECK ("RelationType" IN ('Dlc', 'Expansion', 'StandaloneExpansion'))
);

CREATE INDEX IF NOT EXISTS "IX_GameRelations_RelatedGameId" ON "public"."GameRelations" ("RelatedGameId");

CREATE TABLE IF NOT EXISTS "public"."Genres" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Genres_IgdbId" ON "public"."Genres" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."Themes" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Themes_IgdbId" ON "public"."Themes" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameModes" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_GameModes_IgdbId" ON "public"."GameModes" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."PlayerPerspectives" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_PlayerPerspectives_IgdbId" ON "public"."PlayerPerspectives" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameGenres" (
    "GameId" uuid NOT NULL,
    "GenreId" uuid NOT NULL,
    CONSTRAINT "PK_GameGenres" PRIMARY KEY ("GameId", "GenreId"),
    CONSTRAINT "FK_GameGenres_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameGenres_Genres_GenreId" FOREIGN KEY ("GenreId") REFERENCES "public"."Genres" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameGenres_GenreId" ON "public"."GameGenres" ("GenreId");

CREATE TABLE IF NOT EXISTS "public"."GameThemes" (
    "GameId" uuid NOT NULL,
    "ThemeId" uuid NOT NULL,
    CONSTRAINT "PK_GameThemes" PRIMARY KEY ("GameId", "ThemeId"),
    CONSTRAINT "FK_GameThemes_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameThemes_Themes_ThemeId" FOREIGN KEY ("ThemeId") REFERENCES "public"."Themes" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameThemes_ThemeId" ON "public"."GameThemes" ("ThemeId");

CREATE TABLE IF NOT EXISTS "public"."GameGameModes" (
    "GameId" uuid NOT NULL,
    "GameModeId" uuid NOT NULL,
    CONSTRAINT "PK_GameGameModes" PRIMARY KEY ("GameId", "GameModeId"),
    CONSTRAINT "FK_GameGameModes_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameGameModes_GameModes_GameModeId" FOREIGN KEY ("GameModeId") REFERENCES "public"."GameModes" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameGameModes_GameModeId" ON "public"."GameGameModes" ("GameModeId");

CREATE TABLE IF NOT EXISTS "public"."GamePlayerPerspectives" (
    "GameId" uuid NOT NULL,
    "PlayerPerspectiveId" uuid NOT NULL,
    CONSTRAINT "PK_GamePlayerPerspectives" PRIMARY KEY ("GameId", "PlayerPerspectiveId"),
    CONSTRAINT "FK_GamePlayerPerspectives_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GamePlayerPerspectives_PlayerPerspective" FOREIGN KEY ("PlayerPerspectiveId") REFERENCES "public"."PlayerPerspectives" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GamePlayerPerspectives_PlayerPerspectiveId" ON "public"."GamePlayerPerspectives" ("PlayerPerspectiveId");

CREATE TABLE IF NOT EXISTS "public"."Platforms" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Abbreviation" character varying(128),
    "AlternativeName" character varying(256),
    "Slug" character varying(256),
    "Summary" text,
    "IgdbUrl" character varying(2048),
    "IgdbPlatformTypeId" bigint,
    "PlatformTypeName" character varying(256),
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
    CONSTRAINT "CK_Platforms_Generation" CHECK ("Generation" IS NULL OR "Generation" >= 0),
    CONSTRAINT "CK_Platforms_LogoWidth" CHECK ("LogoWidth" IS NULL OR "LogoWidth" > 0),
    CONSTRAINT "CK_Platforms_LogoHeight" CHECK ("LogoHeight" IS NULL OR "LogoHeight" > 0)
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Platforms_IgdbId" ON "public"."Platforms" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GamePlatforms" (
    "GameId" uuid NOT NULL,
    "PlatformId" uuid NOT NULL,
    CONSTRAINT "PK_GamePlatforms" PRIMARY KEY ("GameId", "PlatformId"),
    CONSTRAINT "FK_GamePlatforms_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GamePlatforms_Platforms_PlatformId" FOREIGN KEY ("PlatformId") REFERENCES "public"."Platforms" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GamePlatforms_PlatformId" ON "public"."GamePlatforms" ("PlatformId");

CREATE TABLE IF NOT EXISTS "public"."PlatformLinks" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "PlatformId" uuid NOT NULL,
    "IgdbWebsiteTypeId" bigint,
    "WebsiteTypeName" character varying(256),
    "Url" character varying(2048) NOT NULL,
    "Trusted" boolean NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_PlatformLinks" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PlatformLinks_Platforms_PlatformId" FOREIGN KEY ("PlatformId") REFERENCES "public"."Platforms" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_PlatformLinks_IgdbId" ON "public"."PlatformLinks" ("IgdbId");
CREATE INDEX IF NOT EXISTS "IX_PlatformLinks_PlatformId" ON "public"."PlatformLinks" ("PlatformId");

CREATE TABLE IF NOT EXISTS "public"."ExternalGameSources" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_ExternalGameSources" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_ExternalGameSources_IgdbId" ON "public"."ExternalGameSources" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameExternalIdentifiers" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "GameId" uuid NOT NULL,
    "ExternalGameSourceId" uuid NOT NULL,
    "PlatformId" uuid,
    "ExternalId" character varying(512) NOT NULL,
    "Name" character varying(512),
    "Url" character varying(2048),
    "Year" integer,
    "Checksum" uuid,
    "IgdbUpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "LastSyncedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_GameExternalIdentifiers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_GameExternalIdentifiers_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameExternalIdentifiers_ExternalSource" FOREIGN KEY ("ExternalGameSourceId") REFERENCES "public"."ExternalGameSources" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_GameExternalIdentifiers_Platforms_PlatformId" FOREIGN KEY ("PlatformId") REFERENCES "public"."Platforms" ("Id") ON DELETE SET NULL,
    CONSTRAINT "CK_GameExternalIdentifiers_Year" CHECK ("Year" IS NULL OR "Year" BETWEEN 0 AND 9999)
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_GameExternalIdentifiers_IgdbId" ON "public"."GameExternalIdentifiers" ("IgdbId");
CREATE INDEX IF NOT EXISTS "IX_GameExternalIdentifiers_GameId" ON "public"."GameExternalIdentifiers" ("GameId");
CREATE INDEX IF NOT EXISTS "IX_GameExternalIdentifiers_PlatformId" ON "public"."GameExternalIdentifiers" ("PlatformId");
CREATE INDEX IF NOT EXISTS "IX_GameExternalIdentifiers_ExternalGameSourceId_ExternalId" ON "public"."GameExternalIdentifiers" ("ExternalGameSourceId", "ExternalId");

CREATE TABLE IF NOT EXISTS "public"."Companies" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Companies_IgdbId" ON "public"."Companies" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameCompanies" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
    "GameId" uuid NOT NULL,
    "CompanyId" uuid NOT NULL,
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
    CONSTRAINT "FK_GameCompanies_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameCompanies_Companies_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES "public"."Companies" ("Id") ON DELETE CASCADE,
    CONSTRAINT "CK_GameCompanies_Role" CHECK ("Developer" OR "Publisher" OR "Porting" OR "Supporting")
);

CREATE UNIQUE INDEX IF NOT EXISTS "IX_GameCompanies_IgdbId" ON "public"."GameCompanies" ("IgdbId");
CREATE INDEX IF NOT EXISTS "IX_GameCompanies_GameId" ON "public"."GameCompanies" ("GameId");
CREATE INDEX IF NOT EXISTS "IX_GameCompanies_CompanyId" ON "public"."GameCompanies" ("CompanyId");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_GameCompanies_GameId_CompanyId" ON "public"."GameCompanies" ("GameId", "CompanyId");

CREATE TABLE IF NOT EXISTS "public"."Collections" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Collections_IgdbId" ON "public"."Collections" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameCollections" (
    "GameId" uuid NOT NULL,
    "CollectionId" uuid NOT NULL,
    CONSTRAINT "PK_GameCollections" PRIMARY KEY ("GameId", "CollectionId"),
    CONSTRAINT "FK_GameCollections_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameCollections_Collections_CollectionId" FOREIGN KEY ("CollectionId") REFERENCES "public"."Collections" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameCollections_CollectionId" ON "public"."GameCollections" ("CollectionId");

CREATE TABLE IF NOT EXISTS "public"."Franchises" (
    "Id" uuid NOT NULL DEFAULT uuidv7(),
    "IgdbId" bigint NOT NULL,
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

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Franchises_IgdbId" ON "public"."Franchises" ("IgdbId");

CREATE TABLE IF NOT EXISTS "public"."GameFranchises" (
    "GameId" uuid NOT NULL,
    "FranchiseId" uuid NOT NULL,
    CONSTRAINT "PK_GameFranchises" PRIMARY KEY ("GameId", "FranchiseId"),
    CONSTRAINT "FK_GameFranchises_Games_GameId" FOREIGN KEY ("GameId") REFERENCES "public"."Games" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GameFranchises_Franchises_FranchiseId" FOREIGN KEY ("FranchiseId") REFERENCES "public"."Franchises" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_GameFranchises_FranchiseId" ON "public"."GameFranchises" ("FranchiseId");
