public class PredictionWeekGroupDto
{
    public int Week { get; set; }
    public int Year { get; set; }
    public List<PredictionItemDto> Predictions { get; set; } = [];
}