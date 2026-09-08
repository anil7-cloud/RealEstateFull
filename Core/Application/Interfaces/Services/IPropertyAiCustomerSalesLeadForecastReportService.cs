using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastReportService
{
    Task<PropertyAiCustomerSalesLeadForecastReportDto> GetReportAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastReportHistoryDto> GetHistoryAsync(int userId);
}
