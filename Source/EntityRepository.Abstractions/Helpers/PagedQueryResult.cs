using System.Collections.Generic;
using System.Linq;

namespace EntityRepository.Abstractions.Helpers
{
    /// <summary>
    /// Class wich implements the return paginated queries
    /// </summary>
    public class PagedQueryResult<T>
    {
        /// <summary>
        /// Page required
        /// </summary>
        public int Page { get; }
        /// <summary>
        /// Page size data
        /// </summary>
        public int PageSize { get; }
        /// <summary>
        /// List with result os paginated queries
        /// </summary>
        public IEnumerable<T> Items { get; }
        /// <summary>
        /// Total amount of paginated items
        /// </summary>
        public long TotalCount { get; }
        public PagedQueryResult(int page, int pageSize, long totalCount, IEnumerable<T> items) =>
            (Page, PageSize, TotalCount, Items) = (page, pageSize, totalCount, items ?? Enumerable.Empty<T>());
    }
}
