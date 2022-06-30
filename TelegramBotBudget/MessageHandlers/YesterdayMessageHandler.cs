namespace TelegramBotBudget;

public class YesterdayMessageHandler : AbstractTelegramMessageHandler
{
    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        //_logger.LogInformation("Yesterday action");
        var chatId = messageContext.message!.Chat.Id;
        messageContext.sessionsDictionary.TryAdd(chatId, new Spending());
        messageContext.sessionsDictionary.TryGetValue(chatId, out Spending? spending);
        spending!.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        await TelegramHelper.SendReplyAsync(chatId, "Введите сумму");

    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return messageContext.message!.Text!.Equals("Вчера");
    }
}
