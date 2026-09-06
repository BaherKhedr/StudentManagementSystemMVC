using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.Data;
using StudentManagementSystemMVC.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagementSystemMVC.ViewModels;
using System.Security.Cryptography.Pkcs;

namespace StudentManagementSystemMVC.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public int GetTotalStudentsCount()
        {
            return _context.Students.Count();
        }
        public int GetStudentsCount(List<Student> students)
        {
            return students.Count;
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public Student GetById(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            return student;
        }
        public Student GetHighestGrade()
        {
            return _context.Students.OrderByDescending(x => x.Grade).FirstOrDefault();
        }
        public Student GetLowestGrade()
        {
            return _context.Students.OrderBy(x => x.Grade).FirstOrDefault();
        }
        public double GetAverageGrade()
        {
            return (double)_context.Students.Average(x => x.Grade);
        }
        public List<Student> ShowAll()
        {
            return _context.Students.ToList();
        }

        public void Update(Student student)
        {
            var existingstudent = _context.Students.FirstOrDefault(x => x.Id == student.Id);
            existingstudent.Name = student.Name;
            existingstudent.Age = student.Age;
            existingstudent.Grade = student.Grade;
            _context.SaveChanges();
        }

        public IQueryable<Student> Filter(StudentSearchViewModel studentviewModel)
        {
            IQueryable<Student> students = _context.Students;
            if (studentviewModel.Name != null)
            {
                students = students.Where(x => x.Name.Contains(studentviewModel.Name));
            }
            if (studentviewModel.AgeFrom != null)
            {
                students = students.Where(x => x.Age >= studentviewModel.AgeFrom);
            }
            if (studentviewModel.AgeTo != null)
            {
                students = students.Where(x => x.Age <= studentviewModel.AgeTo);
            }
            if (studentviewModel.GradeFrom != null)
            {
                students = students.Where(x => x.Grade >= studentviewModel.GradeFrom);
            }
            if (studentviewModel.GradeTo != null)
            {
                students = students.Where(x => x.Grade <= studentviewModel.GradeTo);
            }
            if (studentviewModel.SortBy == "Id")
            {
                students = students.OrderBy(x => x.Id);
            }
            else if (studentviewModel.SortBy == "Name")
            {
                students = students.OrderBy(x => x.Name);
            }
            else if (studentviewModel.SortBy == "Age")
            {
                students = students.OrderByDescending(x => x.Age);
            }
            else if (studentviewModel.SortBy == "Grade")
            {
                students = students.OrderByDescending(x => x.Grade);
            }

            return students;
        }
        public int GetStudentsCount(StudentSearchViewModel viewModel)
        {
            return Filter(viewModel).Count();
        }

        public List<Student> Pagination(StudentSearchViewModel viewModel)
        {
            IQueryable<Student> students = Filter(viewModel);

            return students.Skip((viewModel.CurrentPage - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
        }
        public List<Student> GetFailedStudents()
        {
            
            List<Student> students = _context.Students.Where(x => x.Grade < 50).ToList();
            return students;
        }
        public List<Student> GetPassedStudents()
        {
            List<Student> students = _context.Students.Where(x => x.Grade >= 50).ToList();

            return students;
        }
    }
}
