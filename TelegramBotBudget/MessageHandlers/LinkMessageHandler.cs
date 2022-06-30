namespace TelegramBotBudget;

public class LinkMessageHandler : AbstractTelegramMessageHandler
{
    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        //_logger.LogInformation("/link action");
        await TelegramHelper.SendReplyAsync(messageContext.message!.Chat.Id, GoogleSheetsHelper.GetGSheetLink().ToString());
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return messageContext.message!.Text!.Equals("/link");
    }
}