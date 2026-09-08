using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastReportHistoryService
    : IPropertyAiCustomerSalesLeadForecastReportHistoryService
{
    public Task<PropertyAiCustomerSalesLeadForecastReportHistoryDto> GetHistoryAsync(
        int userId)
    {
        var result = new PropertyAiCustomerSalesLeadForecastReportHistoryDto
        {
            UserId = userId,
            ReportTitle = "Historical Sales Forecast Report",
            ReportContent = "Previous AI forecast reports loaded",
            ReportScore = 0
        };

        return Task.FromResult(result);
    }

    public Task<bool> SaveAsync(
        PropertyAiCustomerSalesLeadForecastReportHistoryDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return Task.FromResult(true);
    }
}
