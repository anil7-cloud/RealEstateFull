using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Validation;

public class LeadValidator
{
    public bool IsValid(Lead lead)
    {
        return !string.IsNullOrWhiteSpace(lead.Email)
            && !string.IsNullOrWhiteSpace(lead.Message);
    }
}
