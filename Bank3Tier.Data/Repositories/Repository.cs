using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bank3Tier.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank3Tier.Data.Repositories
{
        public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
        {
            protected readonly DbContext Context;

            public Repository(DbContext context)
            {
                this.Context = context;
            }

            public async Task AddAsync(TEntity entity)
            {
                await Context.Set<TEntity>().AddAsync(entity);
            }

            public bool Any(Expression<Func<TEntity, bool>> predicate)
            {
                return Context.Set<TEntity>().Any(predicate);
            }

            public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            {
                return Context.Set<TEntity>().Where(predicate);
            }

            public async Task<IEnumerable<TEntity>> GetAllAsync()
            {
                return await Context.Set<TEntity>().ToListAsync();
            }

            public ValueTask<TEntity> GetByIdAsync(int id)
            {
                return Context.Set<TEntity>().FindAsync(id);
            }

            public void Remove(TEntity entity)
            {
                Context.Set<TEntity>().Remove(entity);
            }

            public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
            }
    }
}
