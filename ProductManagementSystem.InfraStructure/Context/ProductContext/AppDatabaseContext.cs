

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using ProductManagementSystem.Domain._SharedKernel;
using ProductManagementSystem.Domain.Product.Entity;

namespace ProductManagementSystem.Infrastructure.Context.ProductContext
{
    public class AppDatabaseContext : DbContext , IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }
        public AppDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDatabaseContext).Assembly);
        }



        #region Transaction
        private readonly IDbContextTransaction _transaction;


        public async Task BeginTransaction()
        {
           await Database.BeginTransactionAsync(isolationLevel:System.Data.IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransaciton()
        {
            try
            {
                await SaveChangesAsync();
                _transaction.Commit();
            }
            catch (Exception ex) {
                 RollbackTransaction();
                Dispose();
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction.Rollback();
            }
            finally {
                Dispose();
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await base.SaveChangesAsync() > default(int);
        }
        #endregion



    }
}
