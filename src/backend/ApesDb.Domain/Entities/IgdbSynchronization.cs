using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public enum IgdbSyncRunMode
{
    Bootstrap,
    Incremental,
}

public enum IgdbSyncRunStatus
{
    Pending,
    Running,
    Failed,
    Succeeded,
}

public enum IgdbSyncStageKind
{
    GameTypes,
    GameStatuses,
    Genres,
    Themes,
    GameModes,
    PlayerPerspectives,
    PlatformTypes,
    WebsiteTypes,
    PopularityTypes,
    ExternalGameSources,
    Companies,
    Collections,
    Franchises,
    Platforms,
    PlatformLinks,
    Games,
    GameRelations,
    InvolvedCompanies,
    ExternalGames,
    Popularity,
    Complete,
}

public enum IgdbSyncStageStatus
{
    Pending,
    Running,
    Failed,
    Succeeded,
}

public enum IgdbSyncSkipReason
{
    MissingGame,
    MissingCompany,
    MissingExternalGameSource,
    MissingPlatform,
}

public sealed class IgdbSyncRun
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public IgdbSyncRunMode Mode { get; set; }

    public IgdbSyncRunStatus Status { get; set; }

    public int CatalogVersion { get; set; }

    public DateTime? From { get; set; }

    public DateTime Through { get; set; }

    public long RowsProcessed { get; set; }

    public long RowsSkipped { get; set; }

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

public sealed class IgdbSyncStage
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public Guid RunId { get; set; }

    public IgdbSyncRun Run { get; set; } = null!;

    public IgdbSyncStageKind Kind { get; set; }

    public int Order { get; set; }

    public IgdbSyncStageStatus Status { get; set; }

    public long PageCursor { get; set; } = -1;

    public int PagesProcessed { get; set; }

    public long RowsProcessed { get; set; }

    public long RowsSkipped { get; set; }

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

public sealed class IgdbSyncTouchedRelationParent
{
    public Guid RunId { get; set; }

    public IgdbSyncRun Run { get; set; } = null!;

    public long GameId { get; set; }

    public GameRelationType RelationType { get; set; }
}

public sealed class IgdbSyncPendingGameRelation
{
    public Guid RunId { get; set; }

    public IgdbSyncRun Run { get; set; } = null!;

    public long GameId { get; set; }

    public long RelatedGameId { get; set; }

    public GameRelationType RelationType { get; set; }
}

public sealed class IgdbSyncSkippedRow
{
    public Guid StageId { get; set; }

    public long EntityId { get; set; }

    public IgdbSyncSkipReason Reason { get; set; }

    public long MissingDependencyId { get; set; }

    public DateTime CreatedAt { get; set; }
}

public sealed class IgdbSyncRunConfiguration : IEntityTypeConfiguration<IgdbSyncRun>
{
    public void Configure(EntityTypeBuilder<IgdbSyncRun> run)
    {
        run.ToTable("IgdbSyncRuns");
        run.HasKey(value => value.Id);
        run.Property(value => value.Id).HasDefaultValueSql("uuidv7()").ValueGeneratedOnAdd();
        run.Property(value => value.Mode).HasConversion<string>().HasMaxLength(32);
        run.Property(value => value.Status).HasConversion<string>().HasMaxLength(32);
        run.Property(value => value.CatalogVersion).HasDefaultValue(1).ValueGeneratedOnAdd();
        run.Property(value => value.RowsSkipped).HasDefaultValue(0L);
        run.Property(value => value.CreatedAt).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        run.Property(value => value.UpdatedAt).HasDefaultValueSql("now()");
        run.Property<int>("CatalogLock").HasDefaultValue(1).ValueGeneratedOnAdd();
        run.HasIndex("CatalogLock")
            .IsUnique()
            .HasDatabaseName("UX_IgdbSyncRuns_Unfinished")
            .HasFilter("\"Status\" <> 'Succeeded'");
        run.HasIndex(value => new { value.Status, value.Through });
        run.ToTable(table =>
        {
            table.HasCheckConstraint("CK_IgdbSyncRuns_Mode", "\"Mode\" IN ('Bootstrap', 'Incremental')");
            table.HasCheckConstraint(
                "CK_IgdbSyncRuns_Status",
                "\"Status\" IN ('Pending', 'Running', 'Failed', 'Succeeded')"
            );
            table.HasCheckConstraint("CK_IgdbSyncRuns_CatalogLock", "\"CatalogLock\" = 1");
            table.HasCheckConstraint("CK_IgdbSyncRuns_CatalogVersion", "\"CatalogVersion\" > 0");
            table.HasCheckConstraint(
                "CK_IgdbSyncRuns_Window",
                "(\"Mode\" = 'Bootstrap' AND \"From\" IS NULL) OR "
                    + "(\"Mode\" = 'Incremental' AND \"From\" IS NOT NULL AND \"From\" < \"Through\")"
            );
            table.HasCheckConstraint("CK_IgdbSyncRuns_RowsProcessed", "\"RowsProcessed\" >= 0");
            table.HasCheckConstraint("CK_IgdbSyncRuns_RowsSkipped", "\"RowsSkipped\" >= 0");
            table.HasCheckConstraint(
                "CK_IgdbSyncRuns_Completion",
                "(\"Status\" = 'Succeeded' AND \"CompletedAt\" IS NOT NULL) OR \"Status\" <> 'Succeeded'"
            );
        });
    }
}

public sealed class IgdbSyncStageConfiguration : IEntityTypeConfiguration<IgdbSyncStage>
{
    public void Configure(EntityTypeBuilder<IgdbSyncStage> stage)
    {
        stage.ToTable("IgdbSyncStages");
        stage.HasKey(value => value.Id);
        stage.Property(value => value.Id).HasDefaultValueSql("uuidv7()").ValueGeneratedOnAdd();
        stage.Property(value => value.Kind).HasConversion<string>().HasMaxLength(64);
        stage.Property(value => value.Status).HasConversion<string>().HasMaxLength(32);
        stage.Property(value => value.PageCursor).HasDefaultValue(-1L);
        stage.Property(value => value.RowsSkipped).HasDefaultValue(0L);
        stage.Property(value => value.CreatedAt).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        stage.Property(value => value.UpdatedAt).HasDefaultValueSql("now()");
        stage.HasIndex(value => new { value.RunId, value.Kind }).IsUnique();
        stage.HasIndex(value => new { value.RunId, value.Order }).IsUnique();
        stage.HasIndex(value => value.Status);
        stage.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_IgdbSyncStages_Status",
                "\"Status\" IN ('Pending', 'Running', 'Failed', 'Succeeded')"
            );
            table.HasCheckConstraint("CK_IgdbSyncStages_Order", "\"Order\" >= 0");
            table.HasCheckConstraint("CK_IgdbSyncStages_PageCursor", "\"PageCursor\" >= -1");
            table.HasCheckConstraint("CK_IgdbSyncStages_PagesProcessed", "\"PagesProcessed\" >= 0");
            table.HasCheckConstraint("CK_IgdbSyncStages_RowsProcessed", "\"RowsProcessed\" >= 0");
            table.HasCheckConstraint("CK_IgdbSyncStages_RowsSkipped", "\"RowsSkipped\" >= 0");
            table.HasCheckConstraint(
                "CK_IgdbSyncStages_Completion",
                "(\"Status\" = 'Succeeded' AND \"CompletedAt\" IS NOT NULL) OR \"Status\" <> 'Succeeded'"
            );
        });
        stage
            .HasOne(value => value.Run)
            .WithMany()
            .HasForeignKey(value => value.RunId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class IgdbSyncSkippedRowConfiguration : IEntityTypeConfiguration<IgdbSyncSkippedRow>
{
    public void Configure(EntityTypeBuilder<IgdbSyncSkippedRow> skippedRow)
    {
        skippedRow.ToTable("IgdbSyncSkippedRows");
        skippedRow.HasKey(value => new
        {
            value.StageId,
            value.EntityId,
            value.Reason,
            value.MissingDependencyId,
        });
        skippedRow.Property(value => value.Reason).HasConversion<string>().HasMaxLength(64);
        skippedRow.Property(value => value.CreatedAt).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        skippedRow.HasIndex(value => value.EntityId);
        skippedRow.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_IgdbSyncSkippedRows_Reason",
                "\"Reason\" IN ('MissingGame', 'MissingCompany', 'MissingExternalGameSource', 'MissingPlatform')"
            );
        });
        skippedRow
            .HasOne<IgdbSyncStage>()
            .WithMany()
            .HasForeignKey(value => value.StageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class IgdbSyncTouchedRelationParentConfiguration : IEntityTypeConfiguration<IgdbSyncTouchedRelationParent>
{
    public void Configure(EntityTypeBuilder<IgdbSyncTouchedRelationParent> parent)
    {
        parent.ToTable("IgdbSyncTouchedRelationParents");
        parent.HasKey(value => new
        {
            value.RunId,
            value.GameId,
            value.RelationType,
        });
        parent.HasIndex(value => value.GameId);
        parent.Property(value => value.RelationType).HasConversion<string>().HasMaxLength(32);
        parent.ToTable(table =>
            table.HasCheckConstraint(
                "CK_IgdbSyncTouchedRelationParents_RelationType",
                "\"RelationType\" IN ('Dlc', 'Expansion', 'StandaloneExpansion')"
            )
        );
        parent
            .HasOne(value => value.Run)
            .WithMany()
            .HasForeignKey(value => value.RunId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class IgdbSyncPendingGameRelationConfiguration : IEntityTypeConfiguration<IgdbSyncPendingGameRelation>
{
    public void Configure(EntityTypeBuilder<IgdbSyncPendingGameRelation> relation)
    {
        relation.ToTable("IgdbSyncPendingGameRelations");
        relation.HasKey(value => new
        {
            value.RunId,
            value.GameId,
            value.RelatedGameId,
            value.RelationType,
        });
        relation.HasIndex(value => new { value.RunId, value.RelatedGameId });
        relation.Property(value => value.RelationType).HasConversion<string>().HasMaxLength(32);
        relation.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_IgdbSyncPendingGameRelations_DifferentGames",
                "\"GameId\" <> \"RelatedGameId\""
            );
            table.HasCheckConstraint(
                "CK_IgdbSyncPendingGameRelations_RelationType",
                "\"RelationType\" IN ('Dlc', 'Expansion', 'StandaloneExpansion')"
            );
        });
        relation
            .HasOne(value => value.Run)
            .WithMany()
            .HasForeignKey(value => value.RunId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
