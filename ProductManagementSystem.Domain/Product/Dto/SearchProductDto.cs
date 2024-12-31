using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Product.Dto
{
    public class SearchProductDto:SearchDto<ProductDto>,IRequest<PageList<ProductDto>>
    {
    }
}
