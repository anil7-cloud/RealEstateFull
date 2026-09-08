using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAiCustomerSalesLeadForecastAppointmentHistoryService
    : IPropertyAiCustomerSalesLeadForecastAppointmentHistoryService
{
    public Task<PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto> GetHistoryAsync(
        int userId)
    {
        var result = new PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto
        {
            UserId = userId,
            PropertyId = 0,
            AppointmentDate = DateTime.UtcNow,
            AppointmentType = "Historical Appointment",
            Status = "Completed"
        };

        return Task.FromResult(result);
    }

    public Task<bool> SaveAsync(
        PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return Task.FromResult(true);
    }
}
