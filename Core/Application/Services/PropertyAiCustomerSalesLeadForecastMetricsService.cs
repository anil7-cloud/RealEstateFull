using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastMetricsService
    : IPropertyAiCustomerSalesLeadForecastMetricsService
{
    public Task<PropertyAiCustomerSalesLeadForecastMetricsDto> GetMetricsAsync(
        int userId)
    {
        return Task.FromResult(
            new PropertyAiCustomerSalesLeadForecastMetricsDto
            {
                UserId = userId,
                TotalLeads = 0,
                ConvertedLeads = 0,
                ConversionRate = 0
            });
    }

    public Task<PropertyAiCustomerSalesLeadForecastMetricsHistoryDto> GetHistoryAsync(
        int userId)
    {
        return Task.FromResult(
            new PropertyAiCustomerSalesLeadForecastMetricsHistoryDto
            {
                UserId = userId,
                TotalLeads = 0,
                ConvertedLeads = 0,
                ConversionRate = 0
            });
    }
}
