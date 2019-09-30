using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Extensions;
using ITG.Brix.EccSetup.Infrastructure.Internal;
using MongoDB.Driver;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories.OperatorActivities
{
    public class OperatorActivityWriteRepository : IOperatorActivityWriteRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public OperatorActivityWriteRepository(DataContext dataContext,
                                               IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IMongoCollection<OperatorActivityClass> Collection => _dataContext.Database.GetCollection<OperatorActivityClass>(Consts.Collections.OperatorActivityCollectionName);

        public async Task CreateAsync(OperatorActivity entity)
        {
            try
            {
                var operatorActivityClass = _mapper.Map<OperatorActivityClass>(entity);

                await Collection.InsertOneAsync(operatorActivityClass);
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

        public Task UpdateAsync(OperatorActivity entity)
        {
            throw new NotImplementedException();
        }
    }
}
