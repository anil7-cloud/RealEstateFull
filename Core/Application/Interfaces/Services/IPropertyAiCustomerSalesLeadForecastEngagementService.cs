using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastEngagementService
{
    Task<PropertyAiCustomerSalesLeadForecastEngagementDto> GetEngagementAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastEngagementHistoryDto> GetHistoryAsync(int userId);
}
