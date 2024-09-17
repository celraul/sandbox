using Cupcake.Domain.Entities;
using System.Linq.Expressions;

namespace Cupcake.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetById(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<List<T>> GetPaginated(Expression<Func<T, bool>> filter, int skip, int take, CancellationToken cancellationToken = default);
    Task<(List<T>, long)> GetPaginatedWithTotal(Expression<Func<T, bool>> filter, int skip, int take, CancellationToken cancellationToken = default);
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> InsertAndReturnAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
