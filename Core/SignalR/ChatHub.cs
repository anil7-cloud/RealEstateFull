using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

public class ChatHub : Hub
{
    public async Task SendMessage(
        int receiverId,
        string message)
    {
        var userIdClaim = Context.User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        if (!int.TryParse(userIdClaim, out var senderId))
            throw new HubException("Kullanıcı kimliği doğrulanamadı.");

        if (senderId <= 0)
            throw new HubException("Geçersiz kullanıcı.");

        if (receiverId <= 0)
            throw new HubException("Geçersiz alıcı.");

        if (senderId == receiverId)
            throw new HubException(
                "Kendinize mesaj gönderemezsiniz.");

        if (string.IsNullOrWhiteSpace(message))
            return;

        await Clients.User(receiverId.ToString())
            .SendAsync(
                "ReceiveMessage",
                senderId,
                message.Trim());
    }
}
