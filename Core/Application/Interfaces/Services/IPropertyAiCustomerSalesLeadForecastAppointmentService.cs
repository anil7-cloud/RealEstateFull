using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastAppointmentService
{
    Task<PropertyAiCustomerSalesLeadForecastAppointmentDto> GetAppointmentAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto> GetHistoryAsync(int userId);
}
