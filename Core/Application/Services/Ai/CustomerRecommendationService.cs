using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Services.Ai;

public class CustomerRecommendationService : ICustomerRecommendationService
{
    private readonly ICustomerBehaviorAnalysisService _behaviorService;

    public CustomerRecommendationService(
        ICustomerBehaviorAnalysisService behaviorService)
    {
        _behaviorService = behaviorService;
    }


    public string GetRecommendation(CustomerBehaviorDto behavior)
    {
        if (behavior.PurchaseIntent >= 80)
        {
            return "Hemen iletişim kur. Yüksek satın alma ihtimali var.";
        }

        if (behavior.PurchaseIntent >= 50)
        {
            return "Düzenli takip yap ve yeni ilanlar gönder.";
        }

        return "Uzun vadeli müşteri olarak değerlendir.";
    }


    public List<string> GetSuggestedActions(CustomerBehaviorDto behavior)
    {
        var actions = new List<string>();

        if (behavior.InterestScore > 100)
            actions.Add("Özel teklif gönder");

        if (behavior.PurchaseIntent > 70)
            actions.Add("Telefon görüşmesi planla");

        if (behavior.VisitLevel == "Yüksek")
            actions.Add("VIP müşteri olarak işaretle");

        return actions;
    }
}
