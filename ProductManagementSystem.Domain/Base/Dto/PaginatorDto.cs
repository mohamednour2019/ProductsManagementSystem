using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Base.Dto
{
    public class PaginatorDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }   
    }
}
