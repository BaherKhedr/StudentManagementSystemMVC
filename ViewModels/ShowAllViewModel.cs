using StudentManagementSystemMVC.Models;

namespace StudentManagementSystemMVC.ViewModels
{
    public class ShowAllViewModel
    {
        public List<Student> Students{ get; set; }
        public PaginationViewModel Pagination { get; set; }
        public StudentSearchViewModel Search { get; set; }
    }
}
