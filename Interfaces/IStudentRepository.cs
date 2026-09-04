using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.ViewModels;

namespace StudentManagementSystemMVC.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> ShowAll();
        Student GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
        List<Student> GetByName(string name);
        List<Student> Search(StudentSearchViewModel studentviewModel);
        int GetTotalStudentsCount();
        List<Student> Pagination(PaginationViewModel viewModel);
    }
}
