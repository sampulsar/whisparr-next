using FluentMigrator;
using NzbDrone.Core.Datastore.Migration.Framework;

namespace NzbDrone.Core.Datastore.Migration
{
    [Migration(318)]
    public class add_movie_paged_sort_index : NzbDroneMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            // The movie index pages large libraries ORDER BY SortTitle, ReleaseDate,
            // StudioTitle with an ItemType filter. The single-column ItemType indexes from
            // 024 cover individual sort keys; this composite covers the combined default
            // ordering so selective ItemType filters can scan in order instead of sorting.
            Create.Index("IX_MovieMetadata_ItemType_SortTitle_ReleaseDate_StudioTitle")
                .OnTable("MovieMetadata")
                .OnColumn("ItemType").Ascending()
                .OnColumn("SortTitle").Ascending()
                .OnColumn("ReleaseDate").Ascending()
                .OnColumn("StudioTitle").Ascending();
        }
    }
}
