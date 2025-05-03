namespace DBM3.Models
{
    public class LearnerDashboardViewModel
    {
        public int CompletedCourses { get; set; }
        public string CurrentCourse { get; set; }
        public string UpcomingAssignment { get; set; }
        public List<string> Notifications { get; set; }
        public int LearnerId { get; set; }
    }
}