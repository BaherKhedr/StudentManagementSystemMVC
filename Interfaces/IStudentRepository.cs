using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.ViewModels;

namespace StudentManagementSystemMVC.Interfaces
{
    public interface IStudentRepository
    {
        int GetTotalStudentsCount();
        int GetStudentsCount(List<Student> students);
        List<Student> ShowAll();
        Student GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
        List<Student> GetByName(string name);
        IQueryable<Student> Filter(StudentSearchViewModel studentviewModel);
        int GetStudentsCount(StudentSearchViewModel viewModel);
        List<Student> Search(StudentSearchViewModel studentviewModel);
        List<Student> Pagination(StudentSearchViewModel viewModel);
        
    }
}
