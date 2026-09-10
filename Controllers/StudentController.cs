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
    public IActionResult MainMenu()
    {
        StudentSearchViewModel searchViewModel = new StudentSearchViewModel();
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            TotalItems = _studentRepository.GetTotalStudentsCount(),
            ActionName = "MainMenu"
        };
        List<Student> students = _studentRepository.Pagination(searchViewModel);
        StudentListViewModel studentListViewModel = new StudentListViewModel
        {
            Students = students,
            Pagination = paginationViewModel,
            Search = searchViewModel
        };
        return View("MainMenu", studentListViewModel);
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        var student = _studentRepository.GetById(id);
        if (student == null)
            return NotFound();
        return PartialView("Details", student);
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View("Add");
    }
    [HttpPost]
    public IActionResult Save(Student student)
    {
        if (ModelState.IsValid)
        {
            _studentRepository.Add(student);
            return PartialView("_Student",student);
        }
        return PartialView("_StudentValidation", student);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _studentRepository.GetById(id);
        return View("Edit", student);
    }
    [HttpPost]
    public IActionResult Update(Student student)
    {
        if (ModelState.IsValid)
        {
            _studentRepository.Update(student);
            return PartialView("_Student", student);
        }

        return PartialView("_StudentValidation", student);
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
        return RedirectToAction("MainMenu");
    }
    public IActionResult Stats()
    {
        return View("Stats");
    }
    public IActionResult Search()
    {
            return View("Search");
    }

    public IActionResult SearchResult(StudentSearchViewModel searchViewModel)
    {
        if(ModelState.IsValid)
        {
            PaginationViewModel paginationViewModel = new PaginationViewModel
            {
                CurrentPage = searchViewModel.CurrentPage,
                PageSize = searchViewModel.PageSize,
                TotalItems = _studentRepository.GetStudentsCount(searchViewModel)
            };
            List<Student> students = _studentRepository.Pagination(searchViewModel);

            StudentListViewModel studentListViewModel = new StudentListViewModel
            {
                Students = students,
                Pagination = paginationViewModel,
                Search = searchViewModel
            };

            return PartialView("_StudentResult", studentListViewModel);
        }
        return PartialView("_SearchValidation" , searchViewModel);
    }
    public IActionResult GetHighestGrade()
    {
        StudentSearchViewModel studentSearchViewModel = new StudentSearchViewModel
        {
            GradeFrom = _studentRepository.GetHighestGrade(),
        };
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = studentSearchViewModel.CurrentPage,
            PageSize = studentSearchViewModel.PageSize,
            TotalItems = _studentRepository.GetStudentsCount(studentSearchViewModel)
        };
        List<Student> PaginatedStudentList = _studentRepository.Pagination(studentSearchViewModel);
        StudentListViewModel studentListViewModel = new StudentListViewModel
        {
            Students = PaginatedStudentList,
            Pagination = paginationViewModel,
            Search = studentSearchViewModel
        };
        return PartialView("_StudentResult", studentListViewModel);
    }
    public IActionResult GetLowestGrade()
    {
        StudentSearchViewModel studentSearchViewModel = new StudentSearchViewModel
        {
            GradeFrom = _studentRepository.GetLowestGrade(),
            GradeTo = _studentRepository.GetLowestGrade(),
        };
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = studentSearchViewModel.CurrentPage,
            PageSize = studentSearchViewModel.PageSize,
            TotalItems = _studentRepository.GetStudentsCount(studentSearchViewModel)
        };
        List<Student> PaginatedStudentList = _studentRepository.Pagination(studentSearchViewModel);
        StudentListViewModel studentListViewModel = new StudentListViewModel
        {
            Students = PaginatedStudentList,
            Pagination = paginationViewModel,
            Search = studentSearchViewModel,

        };
        return PartialView("_StudentResult", studentListViewModel);
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
            GradeTo = 49,
        };
        List<Student> FilteredStudents = _studentRepository.Pagination(studentSearchViewModel);
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = studentSearchViewModel.CurrentPage,
            PageSize = studentSearchViewModel.PageSize,
            TotalItems = students.Count
        };
        StudentListViewModel studentListViewModel = new StudentListViewModel
        {
            Students = FilteredStudents,
            Pagination = paginationViewModel,
            Search = studentSearchViewModel
        };

        return PartialView("_StudentResult", studentListViewModel);
    }
    public IActionResult GetPassedStudents()
    {
        List<Student> students = _studentRepository.GetPassedStudents();
        StudentSearchViewModel studentSearchViewModel = new StudentSearchViewModel
        {
            GradeFrom = 50,
        };
        List<Student> FilteredStudents = _studentRepository.Pagination(studentSearchViewModel);
        PaginationViewModel paginationViewModel = new PaginationViewModel
        {
            CurrentPage = studentSearchViewModel.CurrentPage,
            PageSize = studentSearchViewModel.PageSize,
            TotalItems = students.Count
        };
        StudentListViewModel studentListViewModel = new StudentListViewModel
        {
            Students = FilteredStudents,
            Pagination = paginationViewModel,
            Search = studentSearchViewModel
        };

        return PartialView("_StudentResult", studentListViewModel);
    }
}
