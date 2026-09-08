namespace REAL_ESTATE_CLEAN.Infrastructure.Kafka;

public class KafkaProducer
{
    public void Publish(string topic, string message)
    {
        Console.WriteLine($"📡 KAFKA [{topic}] → {message}");
    }
}
