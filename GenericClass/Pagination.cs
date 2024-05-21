namespace mvccore.GenericClass
{
    public class Pagination
    {
        public (int iPage, int totalPages, double iPageSize, int totalRecords) CalculateTotalRecordsAndPages<T>(IQueryable<T> query, int page, int pageSize)
        {

            var currentPage = page < 1 ? 1 : page;

            var currentPageSize = pageSize < 1 ? 1 : pageSize;


            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);


            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            return (currentPage, totalPages, pageSize, totalRecords);
        }
    }
}
