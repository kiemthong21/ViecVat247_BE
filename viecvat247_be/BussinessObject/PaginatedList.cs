namespace BusinessObject
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int Totalsize { get; private set; }


        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = 1;
            if (pageSize != 0)
            {
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            }
            Totalsize = count;
            AddRange(items);

        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
