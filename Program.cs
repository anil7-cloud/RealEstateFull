using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using REAL_ESTATE_CLEAN.Infrastructure.Auth;
using REAL_ESTATE_CLEAN.Core.Persistence;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Application;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var firebaseCredentialsPath =
    builder.Configuration["Firebase:CredentialsPath"];

if (!string.IsNullOrWhiteSpace(firebaseCredentialsPath) &&
    File.Exists(firebaseCredentialsPath))
{
    FirebaseApp.Create(new AppOptions
    {
        Credential =
            GoogleCredential.FromFile(firebaseCredentialsPath)
    });
}


// DB
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=realestate.db"));

builder.Services.AddScoped<REAL_ESTATE_CLEAN.Infrastructure.Auth.AuthService>();
// SERVICES
builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<PropertyStatisticsService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<AnalyticsService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<SavedSearchService>();
builder.Services.AddScoped<PropertyAlertService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services.IPropertyCustomerMatchService, REAL_ESTATE_CLEAN.Core.Application.Services.PropertyCustomerMatchService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyCustomerMatchHistoryService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchScoringService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerAutoMatchService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerAutomaticMatchingService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerBulkMatchingService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchRecommendationService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchDashboardService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchNotificationService>();



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PaymentService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.AdminService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyDocumentService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.FloorPlanService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.EnergyCertificateService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.NeighborhoodService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyValuationService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyTaxService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.SchoolService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.NearbyHospitalService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PublicTransportService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.ParkingFacilityService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.NearbyPharmacyService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.CrimeStatisticsService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyAmenityService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.FavoriteSearchService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyVisitService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyFraudReportService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.MortgageCalculatorService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyComparisonService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyAuctionService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertySubscriptionService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyContractService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyReservationService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyExpenseService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyIncomeService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyCashFlowService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyMaintenanceScheduleService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyEnergyCertificateService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyInspectionService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyInsuranceService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyUtilityBillService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyLeaseService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyTenantService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyOwnerService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyPortfolioService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyKeyHandoverService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyViewingFeedbackService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyAccessibilityService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyPetPolicyService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.PropertyPhotoService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.LeadPipelineService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.LeadActivityService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.LeadTaskService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.LeadSmsService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.LeadMeetingService>();
builder.Services.AddScoped<REAL_ESTATE_CLEAN.Core.Application.Services.LeadCallService>();

builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationAuditRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationAuditIntegrationService>();

builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationAuditService>();

builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationPersistentAuditService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchJobService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchJobRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRuntimeService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchRuntimeService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationExecutionService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesNextBestActionService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesRecommendationService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesConversionPredictionService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services.IPropertyCustomerMatchService,
    REAL_ESTATE_CLEAN.Core.Application.Services.PropertyCustomerMatchService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesRiskService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesForecastService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchJobRecoveryService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchJobRetryService>();


builder.Services.AddHostedService<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchRetryWorker>();


builder.Services.AddSingleton<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryMetricsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryEventRepository>();


builder.Services.AddHostedService<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryEventCleanupWorker>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryHealthService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryDashboardService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryAlertService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryAlertRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryRiskScoreService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentSlaService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentEscalationService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationRetryIncidentEscalationAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityScoreService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityTrendService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsReliabilityEscalationSummaryService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthScoreService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthHistoryService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthTrendService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthTrendAnomalyService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthIncidentService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationOperationsHealthIncidentAnalyticsService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyVirtualTourRepository>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyVirtualTourService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyVirtualTourViewerService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Services.Ai.PropertyVirtualTourUploadService>();


builder.Services.AddScoped<
    REAL_ESTATE_CLEAN.Core.Application.Services.FavoriteService>();

builder.Services.AddScoped<ProfileService>();
builder.Services.AddSingleton<SpamProtectionService>();
builder.Services.AddScoped<EmailSenderService>();
builder.Services.AddScoped<FcmPushSenderService>();
builder.Services.AddScoped<CurrentUserService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapControllers();

app.MapFallbackToPage("/_Host");


// Recover batch jobs that were left Running by a previous process.
using (var recoveryScope = app.Services.CreateScope())
{
    try
    {
        var recoveryService =
            recoveryScope.ServiceProvider
                .GetRequiredService<
                    REAL_ESTATE_CLEAN.Services.Ai.PropertyCustomerMatchSalesAutomationBatchJobRecoveryService>();

        var recoveryResult =
            await recoveryService
                .RecoverAsync();

        Console.WriteLine(
            $"Batch recovery: Status={recoveryResult.Status}, " +
            $"Found={recoveryResult.RunningJobsFound}, " +
            $"Recovered={recoveryResult.RecoveredJobs}, " +
            $"Failed={recoveryResult.FailedJobs}");
    }
    catch (Exception ex)
    {
        // Recovery failure must not prevent the web application from starting.
        Console.WriteLine(
            $"Batch recovery failed: {ex.Message}");
    }
}

app.Run();
