using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Models.ViewModels
{
    public class PaginationVM
    {

        public IEnumerable<Product>? products { get; set; }
        public int Count { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int CurrPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
    }
}
