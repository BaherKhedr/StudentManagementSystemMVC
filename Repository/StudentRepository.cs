using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.Data;
using StudentManagementSystemMVC.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StudentManagementSystemMVC.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
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

        public Student GetByName(string name)
        {
            Student student = _context.Students.FirstOrDefault(s => s.Name == name);
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
    }
}
