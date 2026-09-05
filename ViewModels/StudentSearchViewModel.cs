using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemMVC.ViewModels
{
    public class StudentSearchViewModel : IValidatableObject
    {
        [RegularExpression(@"^[a-zA-Z ]+$",
    ErrorMessage = "Please don't include numbers in the student's Name")]
        public string? Name { get; set; }

        [Range(10, 25,
            ErrorMessage = "Student's Age must be between 10 and 25")]
        public int? AgeFrom { get; set; }

        [Range(10, 25,
            ErrorMessage = "Student's Age must be between 10 and 25")]
        public int? AgeTo { get; set; }

        [Range(0, 100,
            ErrorMessage = "Student's Grade must be between 0 and 100")]
        public double? GradeFrom { get; set; }

        [Range(0, 100,
            ErrorMessage = "Student's Grade must be between 0 and 100")]
        public double? GradeTo { get; set; }

        public string? SortBy { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AgeFrom.HasValue && AgeTo.HasValue && AgeFrom > AgeTo)
            {
                yield return new ValidationResult("Age From must be less than or equal to Age To.",
                    new[] { nameof(AgeFrom), nameof(AgeTo) }
                );
            }
            if (GradeFrom.HasValue && GradeTo.HasValue && GradeFrom > GradeTo)
            {
                yield return new ValidationResult("Grade From must be less than or equal to Grade To.",
                    new[] { nameof(GradeFrom), nameof(GradeTo) }
                );
            }
        }
    }
}
