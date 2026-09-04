using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.Interfaces;
using StudentManagementSystemMVC.ViewModels;

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
            PaginationViewModel paginationViewModel = new PaginationViewModel();
            paginationViewModel.TotalItems = _studentRepository.GetTotalStudentsCount();
            List<Student> students = _studentRepository.Pagination(paginationViewModel);
            ShowAllViewModel showAllViewModel = new ShowAllViewModel
            {
                Students = students,
                Pagination = paginationViewModel
            };
            return View("ShowAll", showAllViewModel);
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

        public IActionResult Search(StudentSearchViewModel viewModel)
        {
            var students = _studentRepository.Search(viewModel);

            return View("StudentList" , students);
        }

        public IActionResult Pagination(PaginationViewModel viewModel)
        {
            viewModel.TotalItems = _studentRepository.GetTotalStudentsCount();
            List<Student> students = _studentRepository.Pagination(viewModel);

            ShowAllViewModel showAllViewModel = new ShowAllViewModel
            {
                Students = students,
                Pagination = viewModel
            };

            return PartialView("_PaginationResult", showAllViewModel);
        }
        
    }
}
