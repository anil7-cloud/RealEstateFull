namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyPetPolicy
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public bool PetsAllowed { get; set; }

    public bool DogsAllowed { get; set; }

    public bool CatsAllowed { get; set; }

    public bool OtherPetsAllowed { get; set; }

    public int? MaximumPetCount { get; set; }

    public decimal PetDepositAmount { get; set; }

    public string Restrictions { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
