using AutoMapper;

namespace ProductManagementSystem.Infrastructure.AutoMapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            ProductMapping();
        }

        private void ProductMapping()
        {
            CreateMap<Domain.Product.Entity.Product, Domain.Product.Dto.ProductDto>();

        }




    }
}
