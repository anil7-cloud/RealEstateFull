using Microsoft.Extensions.DependencyInjection;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IPropertyAiCustomerSalesLeadForecastService,
            PropertyAiCustomerSalesLeadForecastService>();

        services.AddScoped<IPropertyAiCustomerSalesLeadForecastAnalysisService,
            PropertyAiCustomerSalesLeadForecastAnalysisService>();

        services.AddScoped<IPropertyAiCustomerSalesLeadForecastDashboardService,
            PropertyAiCustomerSalesLeadForecastDashboardService>();

        services.AddScoped<IPropertyAiCustomerSalesLeadForecastMetricsService,
            PropertyAiCustomerSalesLeadForecastMetricsService>();

        services.AddScoped<IPropertyAiCustomerSalesLeadForecastCoreService,
            PropertyAiCustomerSalesLeadForecastCoreService>();

        return services;
    }
}
