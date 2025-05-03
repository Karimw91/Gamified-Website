using System;
using System.Collections.Generic;

namespace DBM3.Models;

public partial class EmotionalfeedbackReview
{
    public int FeedbackId { get; set; }

    public int InstructorId { get; set; }

    public string? Feedback { get; set; }

    public virtual EmotionalFeedback FeedbackNavigation { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;
}
