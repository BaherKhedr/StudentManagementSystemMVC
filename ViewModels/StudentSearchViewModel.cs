namespace StudentManagementSystemMVC.ViewModels
{
    public class StudentSearchViewModel
    {
        public string? Name { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public double? GradeFrom { get; set; }
        public double? GradeTo { get; set; }
        public string? SortBy { get; set; }
    }
}
