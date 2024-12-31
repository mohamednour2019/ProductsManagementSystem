using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Base.Dto
{
    public class SortedDto
    {
        public int SortingDirection{  get; set; }
        public string SortingColumn {  get; set; }
    }
}
