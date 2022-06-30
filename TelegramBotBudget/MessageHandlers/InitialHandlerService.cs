namespace TelegramBotBudget;

public class InitialHandlerService : AbstractTelegramMessageHandler
{
    protected override void HandleThis(ContextAwareMessage messageContext)
    {
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return false;
    }
}