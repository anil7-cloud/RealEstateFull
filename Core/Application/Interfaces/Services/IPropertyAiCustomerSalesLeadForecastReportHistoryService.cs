using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastReportHistoryService
{
    Task<PropertyAiCustomerSalesLeadForecastReportHistoryDto> GetHistoryAsync(int userId);

    Task<bool> SaveAsync(PropertyAiCustomerSalesLeadForecastReportHistoryDto dto);
}
