
namespace ProductManagementSystem.Domain.BaseEntity.Entity
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
