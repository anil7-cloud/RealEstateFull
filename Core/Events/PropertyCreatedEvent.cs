namespace REAL_ESTATE_CLEAN.Core.Events;

public class PropertyCreatedEvent
{
    public int PropertyId { get; set; }
    public string City { get; set; } = "";
    public decimal Price { get; set; }
}
