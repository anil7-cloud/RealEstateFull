using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastAnalysisService
{
    Task<PropertyAiCustomerSalesLeadForecastAnalysisDto> AnalyzeAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastAnalysisHistoryDto> GetHistoryAsync(int userId);
}
