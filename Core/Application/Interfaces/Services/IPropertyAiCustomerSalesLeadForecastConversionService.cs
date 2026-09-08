using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastConversionService
{
    Task<PropertyAiCustomerSalesLeadForecastConversionDto> GetConversionAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastConversionHistoryDto> GetHistoryAsync(int userId);
}
