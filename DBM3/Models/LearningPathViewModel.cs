namespace DBM3.Models
{
    public class LearningPathViewModel
    {
        public int PathId { get; set; }
        public int? LearnerId { get; set; }

        public int? InstructorId { get; set; } // Simulated property
        public string? CompletionStatus { get; set; }
        public string? Description { get; set; } // Simulated property
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Simulated property
        public string? CustomContent { get; set; } // Add this
        public string? AdaptiveRules { get; set; } // Add this

    }
}
