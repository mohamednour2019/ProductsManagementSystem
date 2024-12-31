
using FluentValidation;
using ProductManagementSystem.AppService.Product.EditProduct;
using ProductManagementSystem.Domain.Product.Repository;

namespace ProductManagementSystem.AppService.Product.DeleteProduct
{
    public class DeleteProductCommandValidator:AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("there is no product with this id.");
            RuleFor(command => command).Must(CheckExistence).WithMessage("there is no product with this id.");
        }

        private bool CheckExistence(DeleteProductCommand command)
            => _productRepository.CheckExistence(command.Id).Result;
    }
}
