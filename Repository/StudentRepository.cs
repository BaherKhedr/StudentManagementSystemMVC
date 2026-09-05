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

        public List<Student> GetByName(string name)
        {
            List<Student> student = _context.Students.Where(s => s.Name == name).ToList();
            return student;
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
        public List<Student> Search(StudentSearchViewModel studentviewModel)
        {
            return Filter(studentviewModel).ToList();
        }

        public List<Student> Pagination(StudentSearchViewModel viewModel)
        {
            IQueryable<Student> students = Filter(viewModel);
            return students.Skip((viewModel.CurrentPage -1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
        }
    }
}
