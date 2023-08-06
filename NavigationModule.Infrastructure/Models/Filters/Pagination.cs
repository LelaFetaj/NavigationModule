using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.Infrastructure.Models.Filters
{
    public class Pagination<T, TKey>
    {
        public Expression<Func<T, TKey>> OrderBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 0;
        public bool OrderByDescending { get; set; } = true;
    }
}
