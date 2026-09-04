namespace StudentManagementSystemMVC.ViewModels
{
    public class PaginationViewModel
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;

        public int TotalItems { get; set; }

        public int TotalPages => (int)(Math.Ceiling((double)TotalItems / PageSize));
    }
}
