using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Repositories.Bases;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Extensions;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public abstract class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : Entity
    {
        protected abstract IMongoCollection<TEntity> Collection { get; }

        public async Task CreateAsync(TEntity entity)
        {
            try
            {
                await Collection.InsertOneAsync(entity);
            }
            catch (MongoWriteException ex)
            {
                if (ex.IsUniqueViolation())
                {
                    throw new UniqueKeyException(ex);
                }
                throw new GenericDbException(ex);
            }
            catch (MongoCommandException ex)
            {
                Debug.WriteLine(ex);
                throw new GenericDbException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            }
            catch (MongoWriteException ex)
            {
                if (ex.IsUniqueViolation())
                {
                    throw new UniqueKeyException(ex);
                }
                throw new GenericDbException(ex);
            }
            catch (MongoCommandException ex)
            {
                Debug.WriteLine(ex);
                throw new GenericDbException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, int version)
        {
            try
            {
                var findById = await Collection.FindAsync(x => x.Id == id);
                var user = findById.FirstOrDefault();
                if (user == null)
                {
                    throw new EntityNotFoundDbException();
                }

                var result = await Collection.DeleteOneAsync(x => x.Id == id && x.Version == version);
                if (result.DeletedCount == 0)
                {
                    throw new EntityVersionDbException();
                }
            }
            catch (MongoCommandException ex)
            {
                Debug.WriteLine(ex);
                throw new GenericDbException(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
