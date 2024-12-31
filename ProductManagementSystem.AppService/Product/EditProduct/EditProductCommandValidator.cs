using FluentValidation;
using ProductManagementSystem.AppService.Product.AddProduct;
using ProductManagementSystem.AppService.Product.DeleteProduct;
using ProductManagementSystem.Domain.Product.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.AppService.Product.EditProduct
{
    public class EditProductCommandValidator:AbstractValidator<EditProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public EditProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(command => command.Name).NotEmpty().NotNull().WithMessage("you should provide name.")
                .MaximumLength(100).WithMessage("maximum length for name is 100 characters.");

            RuleFor(command => command.Description).NotEmpty().NotNull().WithMessage("you should provide description.")
                           .MaximumLength(100).WithMessage("maximum length for description is 500 characters.");

            RuleFor(command => command.Price).NotEmpty().WithMessage("you should provide price.")
                .GreaterThan(0).WithMessage("you have provided a wrong price.");

            RuleFor(command => command).Must(IsProductHasUniqueName).WithMessage("sorry there is another product with same name.");

            RuleFor(command => command).Must(CheckExistence).WithMessage("there is no product with this id.");
        }

        private bool IsProductHasUniqueName(EditProductCommand command)
            => !_productRepository.CheckUniqueName(command.Name, command.Id).Result;

        private bool CheckExistence(EditProductCommand command)
           => _productRepository.CheckExistence(command.Id).Result;
    }
}
