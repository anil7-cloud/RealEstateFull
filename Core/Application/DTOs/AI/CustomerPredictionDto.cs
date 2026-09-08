namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

public class CustomerPredictionDto
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = "";

    public int Score { get; set; }

    public int Probability { get; set; }

    public string Prediction { get; set; } = "";
}
