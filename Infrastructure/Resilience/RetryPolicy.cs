namespace REAL_ESTATE_CLEAN.Infrastructure.Resilience;

public class RetryPolicy
{
    public T Execute<T>(Func<T> action)
    {
        int retries = 3;

        while (true)
        {
            try { return action(); }
            catch
            {
                retries--;
                if (retries == 0) throw;
                Console.WriteLine("🔁 retry...");
            }
        }
    }
}
