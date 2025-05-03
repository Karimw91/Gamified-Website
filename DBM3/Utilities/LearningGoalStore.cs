using System;
using System.Collections.Generic;
using System.Linq;
using DBM3.Models;
namespace DBM3.Utilities
{
    public static class LearningGoalStore
    {
        private static readonly Dictionary<int, List<LearningGoal>> LearnerGoals = new();

        public static void AddGoal(int learnerId, string description, DateTime deadline)
        {
            if (!LearnerGoals.ContainsKey(learnerId))
            {
                LearnerGoals[learnerId] = new List<LearningGoal>();
            }

            var newGoal = new LearningGoal
            {
                Id = Guid.NewGuid().GetHashCode(), // Unique temporary ID
                Description = description,
                Deadline = deadline,
                Status = "Pending"
            };

            LearnerGoals[learnerId].Add(newGoal);
        }

        public static List<LearningGoal> GetGoals(int learnerId)
        {
            return LearnerGoals.ContainsKey(learnerId) ? LearnerGoals[learnerId] : new List<LearningGoal>();
        }

        public static void RemoveGoal(int learnerId, int goalId)
        {
            if (LearnerGoals.ContainsKey(learnerId))
            {
                var goal = LearnerGoals[learnerId].FirstOrDefault(g => g.Id == goalId);
                if (goal != null)
                {
                    LearnerGoals[learnerId].Remove(goal);
                }
            }
        }
    }

    public class LearningGoal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }
}
