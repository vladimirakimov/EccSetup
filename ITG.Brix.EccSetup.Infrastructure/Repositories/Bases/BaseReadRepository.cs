using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public abstract class BaseReadRepository<TEntity> : IBaseReadRepository<TEntity> where TEntity : Entity
    {
        protected abstract IMongoCollection<TEntity> Collection { get; }

        public virtual async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter, int? skip, int? limit)
        {
            IFindFluent<TEntity, TEntity> fluent = null;
            if (filter == null)
            {
                var emptyFilter = Builders<TEntity>.Filter.Empty;
                fluent = Collection.Find(emptyFilter);
            }
            else
            {
                fluent = Collection.Find(filter);
            }

            fluent = fluent.Skip(skip).Limit(limit);

            return await fluent.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            try
            {
                var findById = await Collection.FindAsync(doc => doc.Id == id);
                var user = findById.FirstOrDefault();
                if (user == null)
                {
                    throw new EntityNotFoundDbException();
                }
                return user;
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
