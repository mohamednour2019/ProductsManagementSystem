
using MediatR;
using ProductManagementSystem.Domain.Base.Dto;

namespace ProductManagementSystem.AppService.Product.EditProduct
{
    public class EditProductCommand:IRequest<ApiResponse<bool>>
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
