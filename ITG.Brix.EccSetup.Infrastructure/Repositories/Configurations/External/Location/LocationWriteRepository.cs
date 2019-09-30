using AutoMapper;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Extensions;
using ITG.Brix.EccSetup.Infrastructure.Internal;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External
{
    public class LocationWriteRepository : ILocationWriteRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public LocationWriteRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IMongoCollection<LocationClass> Collection => _dataContext.Database.GetCollection<LocationClass>(Consts.Collections.LocationCollectionName);

        public async Task CreateAsync(Location entity)
        {
            try
            {
                var locationClass = _mapper.Map<LocationClass>(entity);

                await Collection.InsertOneAsync(locationClass);
            }
            catch (MongoWriteException ex)
            {
                if (ex.IsUniqueViolation())
                {
                    throw Error.UniqueKey(ex);
                }
                throw Error.GenericDb(ex);
            }
            catch (MongoCommandException ex)
            {
                Debug.WriteLine(ex);
                throw Error.GenericDb(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public Task DeleteAsync(Guid id, int version)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
