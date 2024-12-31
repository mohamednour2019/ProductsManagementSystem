

using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Repository;

namespace ProductManagementSystem.AppService.Product.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            Domain.Product.Entity.Product product = new();
            product.CreateProduct(request.Name, request.Description, request.Price);

            _productRepository.AddProduct(product);

            if(await _productRepository.UnitOfWork.SaveChangesAsync())
            {
                response.CreateSuccessResponse(true, "product has been added successfully.");
            }
            else
            {
                response.CreateFailedResponse(false, new List<string>() { "sorry, something went wrong please try again lager." });
            }
            return response;

        }
    }
}
