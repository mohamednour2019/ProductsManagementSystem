

using MediatR;
using ProductManagementSystem.Domain.Base.Dto;

namespace ProductManagementSystem.AppService.Product.AddProduct
{
    public class AddProductCommand:IRequest<ApiResponse<bool>>
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
