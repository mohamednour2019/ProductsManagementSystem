using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Base.Dto
{
    public class SearchDto<T>
    {
        public T Filter { get; set; }
        public SortedDto Sorting { get; set; } = new SortedDto();
        public PaginatorDto Paginator { get; set; }

        
    }
}
