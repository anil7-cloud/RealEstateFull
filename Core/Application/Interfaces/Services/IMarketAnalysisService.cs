using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

public interface IMarketAnalysisService
{
    MarketAnalysisDto AnalyzeMarket(
        string location,
        decimal averagePrice,
        int listingCount,
        int soldCount,
        int viewCount);
}
