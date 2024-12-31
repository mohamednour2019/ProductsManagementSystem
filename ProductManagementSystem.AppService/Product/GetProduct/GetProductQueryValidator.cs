using FluentValidation;
using ProductManagementSystem.AppService.Product.DeleteProduct;
using ProductManagementSystem.Domain.Product.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.AppService.Product.GetProduct
{
    public class GetProductQueryValidator:AbstractValidator<GetProductQuery>
    {
        private readonly IProductRepository _productRepository;
        public GetProductQueryValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("there is no product with this id.");
            RuleFor(command => command).Must(CheckExistence).WithMessage("there is no product with this id.");
        }

        private bool CheckExistence(GetProductQuery command)
            => _productRepository.CheckExistence(command.Id).Result;
    }
}
