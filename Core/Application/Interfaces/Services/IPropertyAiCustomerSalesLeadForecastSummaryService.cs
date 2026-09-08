using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastSummaryService
{
    Task<PropertyAiCustomerSalesLeadForecastSummaryDto> GetSummaryAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastSummaryHistoryDto> GetHistoryAsync(int userId);
}
