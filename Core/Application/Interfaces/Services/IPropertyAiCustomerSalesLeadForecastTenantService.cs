namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastTenantService
{
    Task<object> GetDashboardAsync(int userId);

    Task<object> GetForecastAsync(int userId);
}
