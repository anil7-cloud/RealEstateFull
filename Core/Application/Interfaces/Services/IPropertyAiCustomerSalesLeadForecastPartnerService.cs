namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IPropertyAiCustomerSalesLeadForecastPartnerService
{
    Task<object> GetDashboardAsync(int userId);

    Task<object> GetForecastAsync(int userId);
}
