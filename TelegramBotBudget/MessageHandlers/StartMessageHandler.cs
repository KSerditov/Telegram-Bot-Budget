namespace TelegramBotBudget;

public class StartMessageHandler : AbstractTelegramMessageHandler
{
    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        messageContext.sessionsDictionary.TryRemove(messageContext.message!.Chat.Id, out Spending? _);
        await TelegramHelper.InitiateSpendingInput(messageContext.message);
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return messageContext.message!.Text!.Equals("/start") || messageContext.message!.Text!.Equals("Ввести снова");
    }
}