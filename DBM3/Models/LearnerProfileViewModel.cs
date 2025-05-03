namespace DBM3.Models
{
    public class LearnerProfileViewModel
    {
        public Learner Learner { get; set; }
        public List<LearningGoalViewModel> Goals { get; set; } = new List<LearningGoalViewModel>();
    }
}
