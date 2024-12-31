using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.AppService.Product.EditProduct
{
    public class EditProductCommandHandler:IRequestHandler<EditProductCommand,ApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;

        public EditProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<bool>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> response = new();

            #region get target product
            Domain.Product.Entity.Product product = await _productRepository.GetProductById(request.Id);
            product.UpdateProduct(request.Name, request.Description, request.Price);
            #endregion

            _productRepository.UpdateProduct(product);

            if (await _productRepository.UnitOfWork.SaveChangesAsync())
            {
                response.CreateSuccessResponse(true, "product has been updated successfully.");
            }
            else
            {
                response.CreateFailedResponse(false, new List<string>() { "sorry, something went wrong please try again lager." });
            }
            return response;
        }
    }
}
