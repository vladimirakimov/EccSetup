namespace ITG.Brix.EccSetup.API.Constants
{
    internal static class Consts
    {
        public static class Config
        {
            public const string ConfigurationPackageObject = "Config";

            public static class Environment
            {
                public const string Section = "Environment";
                public const string Param = "ASPNETCORE_ENVIRONMENT";
            }

            public static class Database
            {
                public const string Section = "Database";
                public const string ConnectionString = "ConnectionString";
            }

            internal static class Storage
            {
                internal const string Section = "Storage";
                internal const string ConnectionString = "ConnectionString";
            }
        }
        public static class Configuration
        {
            public const string Id = "ITG.Brix.EccSetup";
            public const string ConnectionString = "ConnectionString";
            public const string StorageConnectionString = "StorageConnectionString";
        }
    }
}
