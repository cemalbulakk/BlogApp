using BlogApp.Persistence.Paging;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace BlogApp.Persistence.Repositories;

public interface IRepository<T> : IQuery<T> where T : class
{
    T? Get(Expression<Func<T, bool>> predicate);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10,
        bool enableTracking = true);

    int Count(Expression<Func<T, bool>> predicate);

    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
}