using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Realeyes.Application.Interfaces.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync<TProperty>(Expression<Func<TModel, TProperty>> include);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(Guid id);
        Task InsertAsync(TModel model);
        void Update(TModel model);
        Task SaveAsync();
    }
}
