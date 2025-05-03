namespace DBM3.Models
{
    public class InstructorDashboardViewModel
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Course> CoursesTaught { get; set; } = new List<Course>(); public List<LearningPathViewModel> LearningPaths { get; set; } // Optional, if needed    }


    }
}
