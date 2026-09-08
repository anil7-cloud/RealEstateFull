using Microsoft.EntityFrameworkCore;

namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class AppDbContext : DbContext
{

    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyVirtualTour>
        PropertyVirtualTours { get; set; } = null!;

    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyVirtualTourScene>
        PropertyVirtualTourScenes { get; set; } = null!;

    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyVirtualTourHotspot>
        PropertyVirtualTourHotspots { get; set; } = null!;

    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyThreeDimensionalModel>
        PropertyThreeDimensionalModels { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsHealthIncident>
        PropertyMatchSalesAutomationOperationsHealthIncidents { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsHealthSnapshot>
        PropertyMatchSalesAutomationOperationsHealthSnapshots { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation>
        PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach>
        PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAlertHistory>
        PropertyMatchSalesAutomationOperationsReliabilityAlertHistories { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAlert>
        PropertyMatchSalesAutomationOperationsReliabilityAlerts { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistory>
        PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistories { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationOperationsReliabilityHistory>
        PropertyMatchSalesAutomationOperationsReliabilityHistories { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryIncidentEscalationHistory>
        PropertyMatchSalesAutomationRetryIncidentEscalationHistories { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryIncidentEscalation>
        PropertyMatchSalesAutomationRetryIncidentEscalations { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryIncidentHistory>
        PropertyMatchSalesAutomationRetryIncidentHistories { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryIncident>
        PropertyMatchSalesAutomationRetryIncidents { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryAlertHistory>
        PropertyMatchSalesAutomationRetryAlertHistories { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryAlert>
        PropertyMatchSalesAutomationRetryAlerts { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationRetryEvent>
        PropertyMatchSalesAutomationRetryEvents { get; set; } = null!;



    public DbSet<
        REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationBatchJob>
        PropertyMatchSalesAutomationBatchJobs { get; set; } = null!;


    public DbSet<REAL_ESTATE_CLEAN.Core.Domain.Entities.PropertyMatchSalesAutomationAudit>
        PropertyMatchSalesAutomationAudits { get; set; } = null!;



    public DbSet<WorkflowExecutionTemplate> WorkflowExecutionTemplates { get; set; } = null!;

    public DbSet<LeadWorkflowTrigger> LeadWorkflowTriggers { get; set; } = null!;

    public DbSet<LeadWorkflowExecution> LeadWorkflowExecutions { get; set; } = null!;



    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<PropertyMedia> PropertyMedia { get; set; } = null!;

    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<REAL_ESTATE_CLEAN.Core.Persistence.Notification> Notifications { get; set; }
    public DbSet<PropertyView> PropertyViews { get; set; }

    public DbSet<Appointment> Appointments { get; set; } = null!;


    public DbSet<LeadExportJob> LeadExportJobs { get; set; } = null!;
    public DbSet<LeadReportTemplate> LeadReportTemplates { get; set; } = null!;
    public DbSet<LeadReport> LeadReports { get; set; } = null!;
    public DbSet<PriceHistory> PriceHistories { get; set; } = null!;
    public DbSet<CompareItem> CompareItems { get; set; } = null!;
    public DbSet<PropertyShare> PropertyShares { get; set; } = null!;
    public DbSet<RecentlyViewed> RecentlyVieweds { get; set; } = null!;
    public DbSet<SearchHistory> SearchHistories { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    public DbSet<SavedSearch> SavedSearches { get; set; } = null!;
    public DbSet<PropertyReport> PropertyReports { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Agent> Agents { get; set; } = null!;
    public DbSet<Campaign> Campaigns { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<FavoriteFolder> FavoriteFolders { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<Lead> Leads { get; set; } = null!;
    public DbSet<CustomerLead> CustomerLeads { get; set; } = null!;
    public DbSet<MortgageCalculation> MortgageCalculations { get; set; } = null!;
    public DbSet<NotificationPreference> NotificationPreferences { get; set; } = null!;
    public DbSet<PropertyAlert> PropertyAlerts { get; set; } = null!;

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    public DbSet<LeaseAgreement> LeaseAgreements { get; set; } = null!;

    public DbSet<PropertyVisit> PropertyVisits { get; set; } = null!;

    public DbSet<SavedPropertyCollection> SavedPropertyCollections { get; set; } = null!;

    public DbSet<OpenHouse> OpenHouses { get; set; } = null!;

    public DbSet<FloorPlan> FloorPlans { get; set; } = null!;

    public DbSet<VirtualTour> VirtualTours { get; set; } = null!;

    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } = null!;

    public DbSet<EnergyCertificate> EnergyCertificates { get; set; } = null!;

    public DbSet<Neighborhood> Neighborhoods { get; set; } = null!;

    public DbSet<PropertyValuation> PropertyValuations { get; set; } = null!;

    public DbSet<PropertyTaxCalculation> PropertyTaxCalculations { get; set; } = null!;

    public DbSet<PropertyInsurance> PropertyInsurances { get; set; } = null!;

    public DbSet<School> Schools { get; set; } = null!;

    public DbSet<NearbyHospital> NearbyHospitals { get; set; } = null!;

    public DbSet<PublicTransportStop> PublicTransportStops { get; set; } = null!;

    public DbSet<ParkingFacility> ParkingFacilities { get; set; } = null!;

    public DbSet<NearbyMarket> NearbyMarkets { get; set; } = null!;

    public DbSet<NearbyPharmacy> NearbyPharmacies { get; set; } = null!;

    public DbSet<CrimeStatistic> CrimeStatistics { get; set; } = null!;

    public DbSet<PropertyAmenity> PropertyAmenities { get; set; } = null!;

    public DbSet<FavoriteSearch> FavoriteSearches { get; set; } = null!;

    public DbSet<PropertyFraudReport> PropertyFraudReports { get; set; } = null!;

    public DbSet<PropertyOffer> PropertyOffers { get; set; } = null!;

    public DbSet<PropertyPriceHistory> PropertyPriceHistories { get; set; } = null!;

    public DbSet<PropertyComparison> PropertyComparisons { get; set; } = null!;

    public DbSet<PropertyAuction> PropertyAuctions { get; set; } = null!;

    public DbSet<PropertySubscription> PropertySubscriptions { get; set; } = null!;

    public DbSet<PropertyDocument> PropertyDocuments { get; set; } = null!;

    public DbSet<PropertyContract> PropertyContracts { get; set; } = null!;

    public DbSet<PropertyReservation> PropertyReservations { get; set; } = null!;

    public DbSet<PropertyExpense> PropertyExpenses { get; set; } = null!;

    public DbSet<PropertyIncome> PropertyIncomes { get; set; } = null!;

    public DbSet<PropertyMaintenanceSchedule> PropertyMaintenanceSchedules { get; set; } = null!;

    public DbSet<PropertyEnergyCertificate> PropertyEnergyCertificates { get; set; } = null!;

    public DbSet<PropertyInspection> PropertyInspections { get; set; } = null!;

    public DbSet<PropertyTax> PropertyTaxes { get; set; } = null!;

    public DbSet<PropertyUtilityBill> PropertyUtilityBills { get; set; } = null!;

    public DbSet<PropertyRenovation> PropertyRenovations { get; set; } = null!;

    public DbSet<PropertyLease> PropertyLeases { get; set; } = null!;

    public DbSet<PropertyLeasePayment> PropertyLeasePayments { get; set; } = null!;

    public DbSet<PropertyTenant> PropertyTenants { get; set; } = null!;

    public DbSet<PropertyOwner> PropertyOwners { get; set; } = null!;

    public DbSet<PropertyPortfolio> PropertyPortfolios { get; set; } = null!;

    public DbSet<PropertyKeyHandover> PropertyKeyHandovers { get; set; } = null!;

    public DbSet<PropertyViewingFeedback> PropertyViewingFeedbacks { get; set; } = null!;

    public DbSet<PropertyAccessibility> PropertyAccessibilities { get; set; } = null!;

    public DbSet<PropertyPetPolicy> PropertyPetPolicies { get; set; } = null!;

    public DbSet<PropertyPhoto> PropertyPhotos { get; set; } = null!;

    public DbSet<LeadPipeline> LeadPipelines { get; set; } = null!;

    public DbSet<LeadActivity> LeadActivities { get; set; } = null!;

    public DbSet<LeadTask> LeadTasks { get; set; } = null!;

    public DbSet<LeadSms> LeadSms { get; set; } = null!;

    public DbSet<LeadMeeting> LeadMeetings { get; set; } = null!;

    public DbSet<LeadCall> LeadCalls { get; set; } = null!;

    public DbSet<LeadDocument> LeadDocuments { get; set; } = null!;
    public DbSet<LeadEmail> LeadEmails { get; set; } = null!;
    public DbSet<LeadNote> LeadNotes { get; set; } = null!;


    // Lead CRM DbSets
    public DbSet<LeadActivityLog> LeadActivityLogs => Set<LeadActivityLog>();
    public DbSet<LeadAlert> LeadAlerts => Set<LeadAlert>();
    public DbSet<LeadAppointment> LeadAppointments => Set<LeadAppointment>();
    public DbSet<LeadAssignment> LeadAssignments => Set<LeadAssignment>();
    public DbSet<LeadCampaign> LeadCampaigns => Set<LeadCampaign>();
    public DbSet<LeadChecklist> LeadChecklists => Set<LeadChecklist>();
    public DbSet<LeadCommission> LeadCommissions => Set<LeadCommission>();
    public DbSet<LeadContract> LeadContracts => Set<LeadContract>();
    public DbSet<LeadConversion> LeadConversions => Set<LeadConversion>();
    public DbSet<LeadFavorite> LeadFavorites => Set<LeadFavorite>();
    public DbSet<LeadFeedback> LeadFeedbacks => Set<LeadFeedback>();
    public DbSet<LeadFollowUp> LeadFollowUps => Set<LeadFollowUp>();
    public DbSet<LeadInteraction> LeadInteractions => Set<LeadInteraction>();
    public DbSet<LeadInvoice> LeadInvoices => Set<LeadInvoice>();
    public DbSet<LeadLostReason> LeadLostReasons => Set<LeadLostReason>();
    public DbSet<LeadMatch> LeadMatches => Set<LeadMatch>();
    public DbSet<LeadNegotiation> LeadNegotiations => Set<LeadNegotiation>();
    public DbSet<LeadOffer> LeadOffers => Set<LeadOffer>();
    public DbSet<LeadOpportunity> LeadOpportunities => Set<LeadOpportunity>();
    public DbSet<LeadPayment> LeadPayments => Set<LeadPayment>();
    public DbSet<LeadPipelineHistory> LeadPipelineHistories => Set<LeadPipelineHistory>();
    public DbSet<LeadPreference> LeadPreferences => Set<LeadPreference>();
    public DbSet<LeadQualification> LeadQualifications => Set<LeadQualification>();
    public DbSet<LeadRecommendation> LeadRecommendations => Set<LeadRecommendation>();
    public DbSet<LeadReminder> LeadReminders => Set<LeadReminder>();
    public DbSet<LeadRequirement> LeadRequirements => Set<LeadRequirement>();
    public DbSet<LeadScore> LeadScores => Set<LeadScore>();
    public DbSet<LeadSearchHistory> LeadSearchHistories => Set<LeadSearchHistory>();
    public DbSet<LeadSource> LeadSources => Set<LeadSource>();
    public DbSet<LeadStatusHistory> LeadStatusHistories => Set<LeadStatusHistory>();
    public DbSet<LeadTag> LeadTags => Set<LeadTag>();
    public DbSet<LeadTimeline> LeadTimelines => Set<LeadTimeline>();
    public DbSet<LeadVisit> LeadVisits => Set<LeadVisit>();
    public DbSet<LeadWinReason> LeadWinReasons => Set<LeadWinReason>();


    // Yeni Lead CRM tabloları
    public DbSet<LeadTagGroup> LeadTagGroups => Set<LeadTagGroup>();
    public DbSet<LeadSegment> LeadSegments => Set<LeadSegment>();
    public DbSet<LeadSourceDetail> LeadSourceDetails => Set<LeadSourceDetail>();
    public DbSet<LeadConversionFunnel> LeadConversionFunnels => Set<LeadConversionFunnel>();


    // Lead analiz, otomasyon, workflow ve bildirim tabloları
    public DbSet<LeadActivityReport> LeadActivityReports => Set<LeadActivityReport>();
    public DbSet<LeadAiPrediction> LeadAiPredictions => Set<LeadAiPrediction>();
    public DbSet<LeadAnalytics> LeadAnalytics => Set<LeadAnalytics>();
    public DbSet<LeadAutomationRule> LeadAutomationRules => Set<LeadAutomationRule>();
    public DbSet<LeadForecast> LeadForecasts => Set<LeadForecast>();
    public DbSet<LeadKpi> LeadKpis => Set<LeadKpi>();
    public DbSet<LeadNotification> LeadNotifications => Set<LeadNotification>();
    public DbSet<LeadNotificationTemplate> LeadNotificationTemplates => Set<LeadNotificationTemplate>();
    public DbSet<LeadNotificationTemplateVariable> LeadNotificationTemplateVariables => Set<LeadNotificationTemplateVariable>();
    public DbSet<LeadPerformance> LeadPerformances => Set<LeadPerformance>();
    public DbSet<LeadRiskAssessment> LeadRiskAssessments => Set<LeadRiskAssessment>();
    public DbSet<LeadWorkflow> LeadWorkflows => Set<LeadWorkflow>();
    public DbSet<LeadWorkflowStep> LeadWorkflowSteps => Set<LeadWorkflowStep>();

    public DbSet<WorkflowExecutionHistory> WorkflowExecutionHistories => Set<WorkflowExecutionHistory>();
    public DbSet<WorkflowExecutionStep> WorkflowExecutionSteps => Set<WorkflowExecutionStep>();
    public DbSet<WorkflowExecutionVariable> WorkflowExecutionVariables => Set<WorkflowExecutionVariable>();
    public DbSet<WorkflowExecutionSchedule> WorkflowExecutionSchedules => Set<WorkflowExecutionSchedule>();
    public DbSet<WorkflowExecutionLog> WorkflowExecutionLogs => Set<WorkflowExecutionLog>();
    public DbSet<WorkflowExecutionSnapshot> WorkflowExecutionSnapshots => Set<WorkflowExecutionSnapshot>();

    public DbSet<LeadDashboard> LeadDashboards => Set<LeadDashboard>();

    public DbSet<LeadImportJob> LeadImportJobs { get; set; } = null!;
    public DbSet<LeadImportTemplate> LeadImportTemplates { get; set; } = null!;
    public DbSet<LeadAuditLog> LeadAuditLogs { get; set; } = null!;

    // Lead CRM API, bildirim, rapor ve entegrasyon tabloları
    public DbSet<LeadApiClient> LeadApiClients { get; set; } = null!;
    public DbSet<LeadApiKey> LeadApiKeys { get; set; } = null!;
    public DbSet<LeadApiToken> LeadApiTokens { get; set; } = null!;
    public DbSet<LeadApiRequestLog> LeadApiRequestLogs { get; set; } = null!;
    public DbSet<LeadEmailTemplate> LeadEmailTemplates { get; set; } = null!;
    public DbSet<LeadEmailQueue> LeadEmailQueues { get; set; } = null!;
    public DbSet<LeadSmsTemplate> LeadSmsTemplates { get; set; } = null!;
    public DbSet<LeadSmsQueue> LeadSmsQueues { get; set; } = null!;
    public DbSet<LeadNotificationQueue> LeadNotificationQueues { get; set; } = null!;
    public DbSet<LeadPushNotificationTemplate> LeadPushNotificationTemplates { get; set; } = null!;
    public DbSet<LeadPushTemplateVariable> LeadPushTemplateVariables { get; set; } = null!;
    public DbSet<LeadPushNotificationQueue> LeadPushNotificationQueues { get; set; } = null!;
    public DbSet<LeadPushDevice> LeadPushDevices { get; set; } = null!;
    public DbSet<LeadWebhook> LeadWebhooks { get; set; } = null!;
    public DbSet<LeadWebhookHistory> LeadWebhookHistories { get; set; } = null!;
    public DbSet<LeadSystemLog> LeadSystemLogs { get; set; } = null!;



    public DbSet<LeadPushAudience> LeadPushAudiences { get; set; }
    public DbSet<LeadPushAudienceMember> LeadPushAudienceMembers { get; set; }

    public DbSet<LeadPushCampaign> LeadPushCampaigns { get; set; }
    public DbSet<LeadPushCampaignRecipient> LeadPushCampaignRecipients { get; set; }


    public DbSet<LeadPushEvent> LeadPushEvents { get; set; }


    public DbSet<LeadPushProvider> LeadPushProviders { get; set; }
    public DbSet<LeadPushProviderLog> LeadPushProviderLogs { get; set; }

    public DbSet<LeadPushSchedule> LeadPushSchedules { get; set; }

    public DbSet<LeadPushStatistic> LeadPushStatistics { get; set; }

    public DbSet<LeadPushSubscription> LeadPushSubscriptions { get; set; }

    public DbSet<LeadPushTemplateCategory> LeadPushTemplateCategories { get; set; }

    public DbSet<LeadPushTopic> LeadPushTopics { get; set; }



    public DbSet<PropertyCustomerMatch> PropertyCustomerMatches => Set<PropertyCustomerMatch>();

    public DbSet<PropertyCustomerMatchHistory> PropertyCustomerMatchHistories => Set<PropertyCustomerMatchHistory>();
}

