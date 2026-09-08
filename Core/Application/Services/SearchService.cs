using REAL_ESTATE_CLEAN.Core.Application.DTO;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SearchService
{
    private readonly PropertyService _propertyService;

    public SearchService(PropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<List<PropertyDto>> Search(string q)
    {
        return await _propertyService.Search(q);
    }
}
