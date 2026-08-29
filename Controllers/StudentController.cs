using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.Interfaces;

namespace StudentManagementSystemMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet]
        public IActionResult ShowAll()
        {
            List<Student> students = _studentRepository.ShowAll();
            return View("ShowAll", students);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
                return NotFound();
            return View("Details", student);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        public IActionResult SaveAdd(Student student)
        {
            if (student.Id.HasValue && ModelState["Id"]?.Errors.Count == 0)
            {
                var studentidDb = _studentRepository.GetById((int)student.Id);
                if (studentidDb != null)
                {
                    ModelState.AddModelError("Not Found Error", "Student with this Id already exists.");
                }
            }

            if (ModelState.IsValid)
            {
                _studentRepository.Add(student);
                TempData["Found"] = "Student Added Successfully!";
                return RedirectToAction("ShowAll");
            }
            return View("Add", student);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentRepository.GetById(id);
            return View("Edit", student);
        }
        [HttpPost]
        public IActionResult SaveEdit(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Update(student);
                return RedirectToAction("ShowAll");
            }

            return View("Edit", student);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deletedstudent = _studentRepository.GetById(id);

            if (deletedstudent == null)
                return NotFound();

            _studentRepository.Delete(deletedstudent);
            return RedirectToAction("ShowAll");
        }

        public IActionResult Search(string name)
        {
            List<Student> Searchedstudents = _studentRepository.GetByName(name);

            if (Searchedstudents.Count == 0)
            {
                TempData["Not Found Error"] = "Student not found.";
                return RedirectToAction("ShowAll");
            }

            return View("StudentList", Searchedstudents);
        }

        public IActionResult Sort(string option)
        {
            List<Student> sortedlist;
            if (option == "Name")
            {
                sortedlist = _studentRepository.SortByName();
                return View("StudentList", sortedlist);
            }
            else if (option == "Age")
            {
                sortedlist = _studentRepository.SortByAge();
                return View("StudentList", sortedlist);
            }
            else if (option == "Grade")
            {
                sortedlist = _studentRepository.SortByGrade();
                return View("StudentList", sortedlist);
            }
            else
            {
                TempData["Error"] = "Please Select an Option.";
                return RedirectToAction("ShowAll");
            }
        }
    }
}
