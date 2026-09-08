namespace REAL_ESTATE_CLEAN.Infrastructure.Kafka;

public class PartitionRouter
{
    public int GetPartition(int tenantId)
    {
        return tenantId % 10; // simple but scalable partitioning
    }
}
