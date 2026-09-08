using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastCommunicationService
{
    Task<PropertyAiCustomerSalesLeadForecastCommunicationDto> GetCommunicationAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastCommunicationHistoryDto> GetHistoryAsync(int userId);
}
