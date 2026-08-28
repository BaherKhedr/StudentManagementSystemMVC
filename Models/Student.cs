using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemMVC.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Please enter the Student's Id")]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please enter the Student's Name")]
        [RegularExpression(@"^[a-zA-Z ]+$" , ErrorMessage ="Please don't include numbers in the student's Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the Student's Age")]
        [Range(10,25 , ErrorMessage = "Student's Age must be between 10 and 25")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Please enter the Student's Grade")]
        [Range(0, 100 , ErrorMessage = "Student's Grade must be between 0 and 100")]
        public double? Grade { get; set; }
        
    }
}
