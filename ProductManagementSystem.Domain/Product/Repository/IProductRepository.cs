using ProductManagementSystem.Domain._SharedKernel;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Dto;


namespace ProductManagementSystem.Domain.Product.Repository
{
    public interface IProductRepository :IRepository<Product.Entity.Product>
    {
        void AddProduct(Product.Entity.Product product);
        void UpdateProduct(Product.Entity.Product product);
        void DeleteProduct(Product.Entity.Product product);
        Task<Domain.Product.Dto.ProductDto> GetProductDtoById(long id);
        Task<Domain.Product.Entity.Product> GetProductById(long id);
        Task<PageList<ProductDto>> GetProductListAsync(SearchProductDto searchProductDto);
        Task<bool> CheckUniqueName(string name, long id);
        Task<bool> CheckExistence(long id);

    }
}
