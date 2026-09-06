using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.Interfaces;
using StudentManagementSystemMVC.ViewModels;

namespace StudentManagementSystemMVC.Controllers;

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
        StudentSearchViewModel searchViewModel = new StudentSearchViewModel();
        PaginationViewModel paginationViewModel = new PaginationViewModel();
        paginationViewModel.TotalItems = _studentRepository.GetTotalStudentsCount();
        List<Student> students = _studentRepository.Pagination(searchViewModel);
        ShowAllViewModel showAllViewModel = new ShowAllViewModel
        {
            Students = students,
            Pagination = paginationViewModel,
            Search = searchViewModel
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
        if (ModelState.IsValid)
        {
            _studentRepository.Add(student);
            TempData["Found"] = "Student Added Successfully!";
            return View("Add");
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
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var deletedstudent = _studentRepository.GetById(id);

        if (deletedstudent == null)
            return NotFound();

        return View("ConfirmDelete", deletedstudent);
    }
    [HttpPost]
    public IActionResult ConfirmDelete(int id)
    {
        var deletedstudent = _studentRepository.GetById(id);

        if (deletedstudent == null)
            return NotFound();

        _studentRepository.Delete(deletedstudent);
        return RedirectToAction("ShowAll");
    }
    public IActionResult Stats()
    {
        return View("Stats");
    }
    public IActionResult Search(StudentSearchViewModel searchViewModel)
    {
        if (ModelState.IsValid)
        {
            List<Student> students = _studentRepository.Pagination(searchViewModel);

            PaginationViewModel paginationViewModel = new PaginationViewModel
            {
                CurrentPage = searchViewModel.CurrentPage,
                PageSize = searchViewModel.PageSize,
                TotalItems = _studentRepository.GetStudentsCount(searchViewModel)
            };

            ShowAllViewModel showAllViewModel = new ShowAllViewModel
            {
                Students = students,
                Pagination = paginationViewModel,
                Search = searchViewModel
            };

            return View("Search", showAllViewModel);
        }

        return RedirectToAction("ShowAll");
    }

    public IActionResult Pagination(StudentSearchViewModel searchViewModel)
    {
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = searchViewModel.CurrentPage,
            PageSize = searchViewModel.PageSize,
            TotalItems = _studentRepository.GetStudentsCount(searchViewModel),
        };
        List<Student> students = _studentRepository.Pagination(searchViewModel);

        ShowAllViewModel showAllViewModel = new ShowAllViewModel
        {
            Students = students,
            Pagination = paginationViewModel,
            Search = searchViewModel
        };

        return PartialView("_Search", showAllViewModel);
    }
    public IActionResult GetHighestGrade()
    {
        Student student = _studentRepository.GetHighestGrade();
        return PartialView("_Student", student);
    }
    public IActionResult GetLowestGrade()
    {
        Student student = _studentRepository.GetLowestGrade();
        return PartialView("_Student", student);
    }
    public IActionResult GetAverageGrade()
    {
        double averageGrade = _studentRepository.GetAverageGrade();
        ViewBag.AverageGrade = averageGrade;
        return PartialView("_Average");
    }
    public IActionResult GetFailedStudents()
    {
        List<Student> students = _studentRepository.GetFailedStudents();
        StudentSearchViewModel studentSearchViewModel = new StudentSearchViewModel
        {
            GradeTo = 50
        };
        List<Student> FilteredStudents = _studentRepository.Pagination(studentSearchViewModel);
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = studentSearchViewModel.CurrentPage,
            PageSize = studentSearchViewModel.PageSize,
            TotalItems = students.Count
        };
        ShowAllViewModel showAllViewModel = new ShowAllViewModel
        {
            Students = FilteredStudents,
            Pagination = paginationViewModel,
            Search = studentSearchViewModel
        };

        return PartialView("_Search", showAllViewModel);
    }
    public IActionResult GetPassedStudents()
    {
        List<Student> students = _studentRepository.GetPassedStudents();
        StudentSearchViewModel studentSearchViewModel = new StudentSearchViewModel
        {
            GradeFrom = 50
        };
        List<Student> FilteredStudents = _studentRepository.Pagination(studentSearchViewModel);
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = studentSearchViewModel.CurrentPage,
            PageSize = studentSearchViewModel.PageSize,
            TotalItems = students.Count
        };
        ShowAllViewModel showAllViewModel = new ShowAllViewModel
        {
            Students = FilteredStudents,
            Pagination = paginationViewModel,
            Search = studentSearchViewModel
        };

        return PartialView("_Search", showAllViewModel);
    }
}
