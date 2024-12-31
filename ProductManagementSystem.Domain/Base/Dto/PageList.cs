using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Base.Dto
{
    public class PageList<T>
    {
        public List<T> DataList { get; private set; }
        public long TotalCount {  get; private set; }

        public PageList()
        {
            
        }

        public void SetResult(List<T>data,long totalCount)
        {
            DataList = data;
            TotalCount = totalCount;
        }
    }
}
