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
        public double? GetHighestGrade();
        public double? GetLowestGrade();
        double GetAverageGrade();
        List<Student> GetPassedStudents();
        List<Student> GetFailedStudents();
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
        IQueryable<Student> Filter(StudentSearchViewModel studentviewModel);
        int GetStudentsCount(StudentSearchViewModel viewModel);
        List<Student> Pagination(StudentSearchViewModel viewModel);
        
    }
}
