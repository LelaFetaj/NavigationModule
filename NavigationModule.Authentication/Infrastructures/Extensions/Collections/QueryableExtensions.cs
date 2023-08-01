using NavigationModule.Authentication.Models.Exceptions.Common;
using NavigationModule.Authentication.Models.Filters;

namespace NavigationModule.Authentication.Infrastructures.Extensions.Collections
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> PageBy<T, TKey>(
            this IQueryable<T> query,
            Pagination<T, TKey> pagination)
        {
            const int defaultPageNumber = 1;
            const int defaultPageSize = 10;

            if (query is null || pagination is null)
            {
                throw new InvalidArgumentException(
                    parameterName: nameof(query));
            }

            // It is necessary sort items before it
            query = pagination.OrderByDescending
                ? query.OrderByDescending(pagination.OrderBy)
                : query.OrderBy(pagination.OrderBy);

            if (pagination.PageSize <= 0)
            {
                pagination.PageSize = defaultPageSize;
            }

            // Check if the page number is greater then zero - otherwise use default page number
            if (pagination.Page <= 0)
            {
                pagination.Page = defaultPageNumber;
            }

            return query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}
