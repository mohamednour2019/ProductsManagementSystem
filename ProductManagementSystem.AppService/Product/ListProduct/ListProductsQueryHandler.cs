using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Dto;
using ProductManagementSystem.Domain.Product.Repository;


namespace ProductManagementSystem.AppService.Product.ListProduct
{
    public class ListProductsQueryHandler : IRequestHandler<SearchProductDto, PageList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public ListProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PageList<ProductDto>> Handle(SearchProductDto request, CancellationToken cancellationToken)
        =>await _productRepository.GetProductListAsync(request);

    }
}
