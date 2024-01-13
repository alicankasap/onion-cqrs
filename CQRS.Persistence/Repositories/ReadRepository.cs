using CQRS.Application.Interfaces.Repositories;
using CQRS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CQRS.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;

        public ReadRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> Table { get => dbContext.Set<T>(); }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> quaryable = Table;

            if (!enableTracking)
                quaryable = quaryable.AsNoTracking();

            if (include is not null)
                quaryable = include(quaryable);

            if (predicate is not null)
                quaryable = quaryable.Where(predicate);

            if (orderBy is not null)
                return await orderBy(quaryable).ToListAsync();

            return await quaryable.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> quaryable = Table;

            if (!enableTracking)
                quaryable = quaryable.AsNoTracking();

            if (include is not null)
                quaryable = include(quaryable);

            if (predicate is not null)
                quaryable = quaryable.Where(predicate);

            if (orderBy is not null)
                return await orderBy(quaryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await quaryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> quaryable = Table;

            if (!enableTracking)
                quaryable = quaryable.AsNoTracking();

            if (include is not null)
                quaryable = include(quaryable);

            return await quaryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            Table.AsNoTracking();

            if (predicate is not null)
                Table.Where(predicate);

            return await Table.CountAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking)
                Table.AsNoTracking();

            return Table.Where(predicate);
        }
    }
}
