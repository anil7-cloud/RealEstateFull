using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastMessageHistoryService
    : IPropertyAiCustomerSalesLeadForecastMessageHistoryService
{
    public Task<PropertyAiCustomerSalesLeadForecastMessageHistoryDto> GetHistoryAsync(
        int userId)
    {
        return Task.FromResult(
            new PropertyAiCustomerSalesLeadForecastMessageHistoryDto
            {
                UserId = userId
            });
    }

    public Task<bool> SaveAsync(
        PropertyAiCustomerSalesLeadForecastMessageHistoryDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return Task.FromResult(true);
    }
}
