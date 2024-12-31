

using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Dto;

namespace ProductManagementSystem.AppService.Product.GetProduct
{
    public class GetProductQuery:IRequest<ApiResponse<ProductDto>>
    {
        public long Id { get; set; }    
    }
}
