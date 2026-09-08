namespace REAL_ESTATE_CLEAN.Infrastructure.Deployment;

public class BlueGreenDeployment
{
    public void Deploy(string version)
    {
        Console.WriteLine($"Deploying v{version}");
        Console.WriteLine("Switching traffic...");
    }
}
