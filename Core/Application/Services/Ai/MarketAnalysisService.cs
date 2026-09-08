using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class MarketAnalysisService : IMarketAnalysisService
{
    public MarketAnalysisDto AnalyzeMarket(
        string location,
        decimal averagePrice,
        int listingCount,
        int soldCount,
        int viewCount)
    {
        var demandRate = 0;

        if (viewCount > 100)
            demandRate += 40;

        if (soldCount > listingCount / 2)
            demandRate += 30;

        if (averagePrice < 5000000)
            demandRate += 30;


        return new MarketAnalysisDto
        {
            Location = location,
            AveragePrice = averagePrice,
            ListingCount = listingCount,
            SoldCount = soldCount,
            DemandRate = demandRate,
            MarketStatus = GetStatus(demandRate)
        };
    }


    private string GetStatus(int rate)
    {
        if (rate >= 80)
            return "Canlı Piyasa";

        if (rate >= 50)
            return "Dengeli Piyasa";

        return "Yavaş Piyasa";
    }
}
