using StudentManagementSystemMVC.Models;

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
        List<Student> SortByName();
        List<Student> SortByAge();
        List<Student> SortByGrade();
    }
}
