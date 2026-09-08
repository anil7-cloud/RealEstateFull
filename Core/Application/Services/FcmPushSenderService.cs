namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class FcmPushSenderService
{
    public async Task<string> SendAsync(
        string deviceToken,
        string title,
        string body)
    {
        if (string.IsNullOrWhiteSpace(deviceToken))
            throw new ArgumentException(
                "Device token boş olamaz.",
                nameof(deviceToken));

        if (string.IsNullOrWhiteSpace(title))
            title = "REAL ESTATE";

        if (string.IsNullOrWhiteSpace(body))
            body = "Yeni bir bildiriminiz var.";

        var app =
            FirebaseAdmin.FirebaseApp.DefaultInstance;

        if (app is null)
            throw new InvalidOperationException(
                "Firebase başlatılmamış. Firebase kimlik bilgilerini kontrol edin.");

        var message =
            new FirebaseAdmin.Messaging.Message
            {
                Token = deviceToken.Trim(),

                Notification =
                    new FirebaseAdmin.Messaging.Notification
                    {
                        Title = title.Trim(),
                        Body = body.Trim()
                    }
            };

        return await FirebaseAdmin.Messaging.FirebaseMessaging
            .GetMessaging(app)
            .SendAsync(message);
    }
}
