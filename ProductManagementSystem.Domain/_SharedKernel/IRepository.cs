namespace ProductManagementSystem.Domain._SharedKernel
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
