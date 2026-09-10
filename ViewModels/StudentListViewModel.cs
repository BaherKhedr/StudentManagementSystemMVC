using StudentManagementSystemMVC.Models;

namespace StudentManagementSystemMVC.ViewModels
{
    public class StudentListViewModel
    {
        public List<Student> Students{ get; set; }
        public PaginationViewModel Pagination { get; set; }
        public StudentSearchViewModel Search { get; set; }
    }
}
