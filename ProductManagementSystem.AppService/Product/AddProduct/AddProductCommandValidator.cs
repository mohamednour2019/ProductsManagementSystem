

using FluentValidation;
using ProductManagementSystem.Domain.Product.Repository;

namespace ProductManagementSystem.AppService.Product.AddProduct
{
    public class AddProductCommandValidator:AbstractValidator<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public AddProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(command => command.Name).NotEmpty().NotNull().WithMessage("you should provide name.")
                .MaximumLength(100).WithMessage("maximum length for name is 100 characters.");

            RuleFor(command => command.Description).NotEmpty().NotNull().WithMessage("you should provide description.")
                           .MaximumLength(100).WithMessage("maximum length for description is 500 characters.");

            RuleFor(command => command.Price).NotEmpty().WithMessage("you should provide price.")
                .GreaterThan(0).WithMessage("you have provided a wrong price.");

            RuleFor(command => command).Must(IsProductHasUniqueName).WithMessage("sorry there is another product with same name.");
        }

        private bool IsProductHasUniqueName(AddProductCommand command)
            => !_productRepository.CheckUniqueName(command.Name,0).Result;

    }
}
