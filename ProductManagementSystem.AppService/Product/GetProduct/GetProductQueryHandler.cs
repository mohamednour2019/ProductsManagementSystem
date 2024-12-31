
using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Dto;
using ProductManagementSystem.Domain.Product.Repository;

namespace ProductManagementSystem.AppService.Product.GetProduct
{
    public class GetProductQueryHandler:IRequestHandler<GetProductQuery,ApiResponse<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<ProductDto> response = new();
            ProductDto product= await _productRepository.GetProductDtoById(request.Id);

            if(product is not null)
            {
                response.CreateSuccessResponse(product, null);
            }
            else
            {
                response.CreateFailedResponse(null, new List<string>() { "there is no product with this id." });
            }

            return response;
        }
    }
}
