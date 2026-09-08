namespace REAL_ESTATE_CLEAN.Infrastructure.Edge;

public class EdgeRouter
{
    public string GetNearestRegion(string country)
    {
        return country switch
        {
            "TR" => "eu-central-1",
            "US" => "us-east-1",
            _ => "eu-west-1"
        };
    }
}
