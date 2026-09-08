using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastWorkflowService
{
    Task<PropertyAiCustomerSalesLeadForecastWorkflowDto> GetWorkflowAsync(int userId);

    Task<PropertyAiCustomerSalesLeadForecastWorkflowHistoryDto> GetHistoryAsync(int userId);
}
