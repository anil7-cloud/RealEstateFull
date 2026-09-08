namespace REAL_ESTATE_CLEAN.Infrastructure.Release;

public class ReleaseService
{
    public string Version => "1.0.0";

    public string GetEnvironment()
    {
        return "production";
    }

    public bool IsStable()
    {
        return true;
    }
}
