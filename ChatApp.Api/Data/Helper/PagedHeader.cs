namespace ChatApp.Api.Data.Helper
{
    public class PagedHeader
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public PagedHeader(int currentPage,int totalpage,int itemsPerPage,int totalItems)
        {
            CurrentPage = currentPage;
            TotalPages = totalpage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;

            
        }
    }
}
