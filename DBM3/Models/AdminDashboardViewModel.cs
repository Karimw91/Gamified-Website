namespace DBM3.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalLearners { get; set; }
        public int TotalInstructors { get; set; }
        public int TotalCourses { get; set; }
        public List<Learner> Learners { get; set; } // Add this property    }
    }
}
