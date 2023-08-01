using System.Linq.Expressions;

namespace NavigationModule.Authentication.Models.Filters
{
    public class Pagination<T, TKey>
    {
        public required Expression<Func<T, TKey>> OrderBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool OrderByDescending { get; set; } = true;
    }
}
