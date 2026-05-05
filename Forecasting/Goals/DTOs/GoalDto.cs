namespace Forecasting.Goals.DTOs
{
    public class GoalDto
    {
        public string NameGoal { get; set; } = string.Empty;

        public decimal ProgressGoal { get; set; }

        public string CategoryCodeGoal { get; set; } = string.Empty;

        public decimal BonusGoal { get; set; }

        public string? StatusGoal { get; set; }
    }
}
