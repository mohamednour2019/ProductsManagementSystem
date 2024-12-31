

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Infrastructure.Context.ProductContext;
using System.Linq.Expressions;

namespace ProductManagementSystem.Infrastructure.Base
{
    public abstract class EntityRepository<T> where T : class
    {
        protected Context.ProductContext.AppDatabaseContext AppDbContext { get; }
        private readonly DbSet<T> Set;
        protected IMapper Mapper { get; }

        public EntityRepository(Context.ProductContext.AppDatabaseContext appDbContext,IMapper mapper)
        {
            AppDbContext = appDbContext;
            Mapper = mapper;
            Set = AppDbContext.Set<T>();
        }


        #region Actions
        public void Create(T entity) {
            if (entity is not null) {
                Set.Add(entity);
            }
        }

        public void Update(T entity) {
            if (entity is not null) {
                Set.Update(entity);
            }
        }


        public void Delete(T entity) {
            if (entity is not null) {
                Set.Remove(entity);
            }
        }
        #endregion


        #region Queries
        public Task<List<T>> GetAllAsync()
        {
           return Set.ToListAsync();
        }

        public Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter=null) { 
            if(filter is not null)
            {
                return Set.Where(filter).ToListAsync();
            }
            return Set.ToListAsync();
        }


        public async Task<List<T>> GetWhereWithIncludePropertiesAsync(string indclueProperties="", Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query= Set;
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            query = indclueProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                  .Aggregate(query, (current, includedProperty) => current.Include(includedProperty));
            return await query.ToListAsync();
        }


        public async Task<T> FirstOrDefaultAsyncWithTracking(Expression<Func<T, bool>>? filter = null)
        {
            if (filter is not null)
            {
                return await Set.FirstOrDefaultAsync(filter);
            }
            return await Set.FirstOrDefaultAsync();
        }

        public async Task<T> FirstOrDefaultAsyncWithNoTracking(Expression<Func<T, bool>>? filter = null)
        {
            if (filter is not null)
            {
                return await Set.AsNoTracking().FirstOrDefaultAsync(filter);
            }
            return await Set.FirstOrDefaultAsync();
        }



        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null)
        {
            if(filter is not null)
            {
                return await Set.AnyAsync(filter);  
            }
            return await Set.AnyAsync();
        }


        public async Task<T> FirstOrDefaultWithIncludePropertiesAsync(string includeProperties="", Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query= Set;
            if (filter is not null)
            {
               query = query.Where(filter);
            }
            query= includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query,(current,property)=>current.Include(property));

            return await Set.FirstOrDefaultAsync();
        }
        #endregion



        #region SearchPage
        public async Task<List<T>>SearchPage<Key>(int skipCount,int takeCount,int sortDirection, Expression<Func<T, Key>> sortExpression
            , Expression<Func<T, bool>> filter,string includingProperties="") 
        {
            IQueryable<T> query = null;
            skipCount = (skipCount - 1) * takeCount;
            if(filter is not null)
            {
                query = Set.Where(filter);
            }

            query= includingProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query,(current,property)=>current.Include(property));

            switch (sortDirection)
            {
                case 0:
                    query = query.OrderBy<T, Key>(sortExpression).Skip(skipCount).Take(takeCount);
                    break;
                case 1:
                    query = query.OrderByDescending<T, Key>(sortExpression).Skip(skipCount).Take(takeCount);
                    break;

                default:
                    query = query.Skip(skipCount).Take(takeCount);
                    break;
            }

            return await query.AsNoTracking().ToListAsync();




        }


        public async Task<long> GetTotalCount(Expression<Func<T, bool>> filter) { 
            return await Set.Where(filter).CountAsync();  
        }

        #endregion


    }
}
