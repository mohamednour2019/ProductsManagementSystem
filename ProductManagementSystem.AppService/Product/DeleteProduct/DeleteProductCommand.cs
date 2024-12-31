using MediatR;
using ProductManagementSystem.Domain.Base.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.AppService.Product.DeleteProduct
{
    public class DeleteProductCommand:IRequest<ApiResponse<bool>>
    {
        public long Id { get; set; }    
    }
}
