using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Repository;


namespace ProductManagementSystem.AppService.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            #region get target product
            Domain.Product.Entity.Product product = await _productRepository.GetProductById(request.Id);
            #endregion

            _productRepository.DeleteProduct(product);

            if (await _productRepository.UnitOfWork.SaveChangesAsync())
            {
                response.CreateSuccessResponse(true, "product has been deleted successfully.");
            }
            else
            {
                response.CreateFailedResponse(false, new List<string>() { "sorry, something went wrong please try again lager." });
            }
            return response;
        }
    }
}
