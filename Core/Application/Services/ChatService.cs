using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class ChatService
{
    private readonly AppDbContext _context;
    private readonly SpamProtectionService _spamProtectionService;

    public ChatService(
        AppDbContext context,
        SpamProtectionService spamProtectionService)
    {
        _context = context;
        _spamProtectionService = spamProtectionService;
    }

    public async Task SendMessage(int senderId, int receiverId, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        if (!_spamProtectionService.IsMessageAllowed(senderId))
        {
            throw new InvalidOperationException(
                "Çok kısa sürede çok fazla mesaj gönderildi. Lütfen daha sonra tekrar deneyin.");
        }

        var chat = new ChatMessage
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Message = message.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.ChatMessages.Add(chat);
        await _context.SaveChangesAsync();
    }

    public List<ChatMessage> GetConversation(int user1, int user2)
    {
        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .OrderBy(x => x.CreatedAt)
            .ToList();
    }

    public async Task DeleteConversation(int user1, int user2)
    {
        var messages = await _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .ToListAsync();

        _context.ChatMessages.RemoveRange(messages);
        await _context.SaveChangesAsync();
    }

    public int GetUnreadMessageCount(int receiverId)
    {
        return _context.ChatMessages.Count(x =>
            x.ReceiverId == receiverId && !x.IsRead);
    }

    public async Task MarkAsRead(int messageId)
    {
        var message = await _context.ChatMessages.FindAsync(messageId);

        if (message == null)
            return;

        message.IsRead = true;
        await _context.SaveChangesAsync();
    }

    public ChatMessage? GetLastMessage(int user1, int user2)
    {
        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault();
    }

    public bool HasConversation(int user1, int user2)
    {
        return _context.ChatMessages.Any(x =>
            (x.SenderId == user1 && x.ReceiverId == user2) ||
            (x.SenderId == user2 && x.ReceiverId == user1));
    }

    public List<ChatMessage> GetUserChats(int userId)
    {
        return _context.ChatMessages
            .Where(x => x.SenderId == userId || x.ReceiverId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public int GetConversationCount(int user1, int user2)
    {
        return _context.ChatMessages.Count(x =>
            (x.SenderId == user1 && x.ReceiverId == user2) ||
            (x.SenderId == user2 && x.ReceiverId == user1));
    }

    public List<ChatMessage> GetConversationPage(
        int user1,
        int user2,
        int page,
        int pageSize)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public List<ChatMessage> GetMessagesByDate(
        int user1,
        int user2,
        DateTime startDate,
        DateTime endDate)
    {
        return _context.ChatMessages
            .Where(x =>
                ((x.SenderId == user1 && x.ReceiverId == user2) ||
                 (x.SenderId == user2 && x.ReceiverId == user1)) &&
                x.CreatedAt >= startDate &&
                x.CreatedAt <= endDate)
            .OrderBy(x => x.CreatedAt)
            .ToList();
    }

    public List<ChatMessage> SearchMessages(
        int user1,
        int user2,
        string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<ChatMessage>();

        return _context.ChatMessages
            .Where(x =>
                ((x.SenderId == user1 && x.ReceiverId == user2) ||
                 (x.SenderId == user2 && x.ReceiverId == user1)) &&
                x.Message.Contains(keyword))
            .OrderBy(x => x.CreatedAt)
            .ToList();
    }

    public async Task ClearConversation(int user1, int user2)
    {
        await DeleteConversation(user1, user2);
    }

    public async Task<bool> EditMessage(int messageId, string newMessage)
    {
        if (string.IsNullOrWhiteSpace(newMessage))
            return false;

        var message = await _context.ChatMessages.FindAsync(messageId);

        if (message == null)
            return false;

        message.Message = newMessage.Trim();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMessage(int messageId)
    {
        var message = await _context.ChatMessages.FindAsync(messageId);

        if (message == null)
            return false;

        _context.ChatMessages.Remove(message);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<ChatMessage?> GetMessageById(int messageId)
    {
        return await _context.ChatMessages
            .FirstOrDefaultAsync(x => x.Id == messageId);
    }

    public List<ChatMessage> GetSentMessages(int senderId)
    {
        return _context.ChatMessages
            .Where(x => x.SenderId == senderId)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public List<ChatMessage> GetReceivedMessages(int receiverId)
    {
        return _context.ChatMessages
            .Where(x => x.ReceiverId == receiverId)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public DateTime? GetLastMessageDate(int user1, int user2)
    {
        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => (DateTime?)x.CreatedAt)
            .FirstOrDefault();
    }

    public ChatMessage? GetFirstMessage(int user1, int user2)
    {
        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .OrderBy(x => x.CreatedAt)
            .FirstOrDefault();
    }

    public List<ChatMessage> GetLastMessages(
        int user1,
        int user2,
        int count)
    {
        count = Math.Clamp(count, 1, 100);

        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == user1 && x.ReceiverId == user2) ||
                (x.SenderId == user2 && x.ReceiverId == user1))
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .OrderBy(x => x.CreatedAt)
            .ToList();
    }

    public async Task<bool> ArchiveMessage(int messageId)
    {
        var message = await _context.ChatMessages.FindAsync(messageId);

        if (message == null)
            return false;

        message.IsArchived = true;
        await _context.SaveChangesAsync();

        return true;
    }

    public List<ChatMessage> GetArchivedMessages(int userId)
    {
        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == userId || x.ReceiverId == userId) &&
                x.IsArchived)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public async Task<bool> UnarchiveMessage(int messageId)
    {
        var message = await _context.ChatMessages.FindAsync(messageId);

        if (message == null)
            return false;

        message.IsArchived = false;
        await _context.SaveChangesAsync();

        return true;
    }

    public List<ChatMessage> GetActiveMessages(int userId)
    {
        return _context.ChatMessages
            .Where(x =>
                (x.SenderId == userId || x.ReceiverId == userId) &&
                !x.IsArchived)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }
}
