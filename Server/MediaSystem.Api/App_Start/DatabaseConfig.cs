namespace MediaSystem.Api
{
    using System.Data.Entity;

    using MediaSystem.Data;
    using MediaSystem.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaSystemContext, Configuration>());
        }
    }
}
