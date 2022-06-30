using Telegram.Bot.Types;
using System.Collections.Concurrent;

namespace TelegramBotBudget;

public class ContextAwareMessage
{
    public Message? message { get; set; }
    public ConcurrentDictionary<long, Spending> sessionsDictionary { get; set; } = null!;

}