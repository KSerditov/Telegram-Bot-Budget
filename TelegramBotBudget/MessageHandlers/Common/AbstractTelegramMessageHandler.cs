namespace TelegramBotBudget;

public abstract class AbstractTelegramMessageHandler : ITelegramMessageHandler
{
    private ITelegramMessageHandler? _nextHandler;

    public ITelegramMessageHandler SetNext(ITelegramMessageHandler handler)
    {
        this._nextHandler = handler;

        return handler;
    }

    protected abstract bool VerifyValidity(ContextAwareMessage messageContext);

    protected abstract void HandleThis(ContextAwareMessage messageContext);

    public Task<bool> Handle(ContextAwareMessage messageContext)
    {
        if (this.VerifyValidity(messageContext))
        {
            HandleThis(messageContext);
            return Task.FromResult(true);
        }

        if (this._nextHandler != null) return this._nextHandler.Handle(messageContext);

        return Task.FromResult(false);

    }
}