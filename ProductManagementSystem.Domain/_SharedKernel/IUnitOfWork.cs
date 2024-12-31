namespace ProductManagementSystem.Domain._SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveChangesAsync();
        public Task BeginTransaction();
        public Task CommitTransaciton();
        public void RollbackTransaction();
    }
}
