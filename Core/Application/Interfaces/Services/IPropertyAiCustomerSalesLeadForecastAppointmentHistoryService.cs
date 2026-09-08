using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastAppointmentHistoryService
{
    Task<PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto> GetHistoryAsync(int userId);

    Task<bool> SaveAsync(PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto dto);
}
