namespace TelegramBotBudget;

public interface ITelegramMessageHandler
{
    public ITelegramMessageHandler SetNext(ITelegramMessageHandler handler);
    public Task<bool> Handle(ContextAwareMessage messageContext);
}