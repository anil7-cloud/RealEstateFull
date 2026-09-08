using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastInsightService
{
    Task<PropertyAiCustomerSalesLeadForecastInsightDto> GetInsightAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastInsightHistoryDto> GetHistoryAsync(int userId);
}
