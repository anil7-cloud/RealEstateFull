namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyNotificationSettingsRequestDto
{
    public int UserId { get; set; }

    public bool PriceChangeNotification { get; set; } = true;

    public bool NewPropertyNotification { get; set; } = true;

    public bool AppointmentNotification { get; set; } = true;

    public bool MessageNotification { get; set; } = true;
}
