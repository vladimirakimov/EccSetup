using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Security.Authentication;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases
{
    public static class RepositoryTestsHelper
    {
        private static string _connectionString = null;
        private static string _dbName = "Brix-EccSetup";
        private static MongoClient _client;

        public static void Init(string collectionName)
        {
            _client = GetMongoClient();
            var databaseExists = DatabaseExists(_dbName);
            if (!databaseExists)
            {
                DatabaseCreate(_dbName);
            }

            var collectionExists = CollectionExists(collectionName);
            if (collectionExists)
            {
                CollectionClear(collectionName);
            }
            else
            {
                CollectionCreate(collectionName);
            }
        }


        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    var config = new ConfigurationBuilder().AddJsonFile("settings.json", optional: false).Build();
                    _connectionString = config["ConnectionString"];
                }

                return _connectionString;
            }
        }

        private static MongoClient GetMongoClient()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);

            return client;
        }

        private static bool DatabaseExists(string databaseName)
        {
            var dbList = _client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString);
            return dbList.Contains(databaseName);
        }

        private static void DatabaseCreate(string databaseName)
        {
            _client.GetDatabase(databaseName);
        }

        private static bool CollectionExists(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);
            var filter = new BsonDocument("name", collectionName);
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collections.Any();
        }

        private static void CollectionCreate(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);

            switch (collectionName)
            {
                case "StorageStatuses":
                    database.GetCollection<StorageStatus>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<StorageStatus>(Builders<StorageStatus>.IndexKeys
                        .Ascending(_ => _.Code)
                        .Ascending(_ => _.Source),
                        new CreateIndexOptions() { Unique = true }));
                    break;
                case "DamageCodes":
                    database.GetCollection<DamageCode>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<DamageCode>(Builders<DamageCode>.IndexKeys
                        .Ascending(_ => _.Code)
                        .Ascending(_ => _.Source),
                        new CreateIndexOptions() { Unique = true }));
                    break;
                case "TypePlannings":
                    database.GetCollection<TypePlanning>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<TypePlanning>(Builders<TypePlanning>.IndexKeys
                        .Ascending(_ => _.Code)
                        .Ascending(_ => _.Source),
                        new CreateIndexOptions() { Unique = true }));
                    break;
                case "ProductionSites":
                    database.GetCollection<ProductionSite>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<ProductionSite>(Builders<ProductionSite>.IndexKeys
                        .Ascending(_ => _.Code)
                        .Ascending(_ => _.Source),
                        new CreateIndexOptions() { Unique = true }));
                    break;
                case "Sites":
                    database.GetCollection<Site>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<Site>(Builders<Site>.IndexKeys
                        .Ascending(_ => _.Code)
                        .Ascending(_ => _.Source), 
                        new CreateIndexOptions() { Unique = true }));
                    break;
                case "TransportTypes":
                    database.GetCollection<TransportType>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<TransportType>(Builders<TransportType>.IndexKeys
                        .Ascending(_ => _.Code)
                        .Ascending(_ => _.Name)
                        .Ascending(_ => _.Source),
                        new CreateIndexOptions() { Unique = true }));
                    break;
                case "Locations":
                    database.GetCollection<LocationClass>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<LocationClass>(Builders<LocationClass>.IndexKeys
                    .Ascending(_ => _.Sc)
                    .Ascending(_ => _.St)
                    .Ascending(_ => _.W)
                    .Ascending(_ => _.G)
                    .Ascending(_ => _.R)
                    .Ascending(_ => _.P),
                    new CreateIndexOptions() { Unique = true }));
                    break;
                case "OperationalDepartments":
                    database.GetCollection<OperationalDepartment>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<OperationalDepartment>(Builders<OperationalDepartment>.IndexKeys.Ascending(_ => _.Code).Ascending(_ => _.Source).Ascending(_ => _.Site), new CreateIndexOptions() { Unique = true }));
                    break;
                case "Customers":
                    database.GetCollection<Customer>(collectionName).Indexes.CreateOneAsync(new CreateIndexModel<Customer>(Builders<Customer>.IndexKeys.Ascending(_ => _.Code).Ascending(_ => _.Source), new CreateIndexOptions() { Unique = true }));
                    break;
                case Consts.Collections.ChecklistCollectionName:
                    database.GetCollection<Checklist>(collectionName).Indexes.CreateOneAsync(Builders<Checklist>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.InformationCollectionName:
                    database.GetCollection<Information>(collectionName).Indexes.CreateOneAsync(Builders<Information>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.InputCollectionName:
                    database.GetCollection<Input>(collectionName).Indexes.CreateOneAsync(Builders<Input>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.InstructionCollectionName:
                    database.GetCollection<Instruction>(collectionName).Indexes.CreateOneAsync(Builders<Instruction>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.RemarkCollectionName:
                    database.GetCollection<Remark>(collectionName).Indexes.CreateOneAsync(Builders<Remark>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.ValidationCollectionName:
                    database.GetCollection<Validation>(collectionName).Indexes.CreateOneAsync(Builders<Validation>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.BusinessUnitCollectionName:
                    database.GetCollection<BusinessUnit>(collectionName).Indexes.CreateOneAsync(Builders<BusinessUnit>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.IconCollectionName:
                    database.GetCollection<Icon>(collectionName).Indexes.CreateOneAsync(Builders<Icon>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.OperationCollectionName:
                    database.GetCollection<Operation>(collectionName).Indexes.CreateOneAsync(Builders<Operation>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.SourceCollectionName:
                    database.GetCollection<Source>(collectionName).Indexes.CreateOneAsync(Builders<Source>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.FlowCollectionName:
                    database.GetCollection<Flow>(collectionName).Indexes.CreateOneAsync(Builders<Flow>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.LayoutCollectionName:
                    database.GetCollection<Layout>(collectionName).Indexes.CreateOneAsync(Builders<Layout>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;

            }

        }

        private static void CollectionClear(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Ne("Id", "0");
            collection.DeleteMany(filter);
        }
    }
}
