using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Infrastructure.Data;

namespace Realeyes.Infrastructure.Repositories
{
    class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly DbSet<TModel> table;
        private readonly RealeyeDbContext dbContext;

        public Repository(RealeyeDbContext realeyeDbContext)
        {
            this.dbContext = realeyeDbContext;
            this.table = dbContext.Set<TModel>();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await table.ToListAsync();
        }
        public async Task<IEnumerable<TModel>> GetAllAsync<TProperty>(Expression<Func<TModel, TProperty>> include = null)
        {
            return await table.Include(include).ToListAsync();
        }
        public async Task<TModel> GetByIdAsync(Guid id)
        {
            return await table.FindAsync(id);
        }
        public async Task InsertAsync(TModel model)
        {
            await table.AddAsync(model);
        }
        public void Update(TModel model)
        {
            table.Attach(model);
            dbContext.Entry(model).State = EntityState.Modified;
        }
      
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
