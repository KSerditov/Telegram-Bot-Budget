namespace TelegramBotBudget;

public class TodayMessageHandler : AbstractTelegramMessageHandler
{
    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
            //_logger.LogInformation("Today action");
            messageContext.sessionsDictionary.TryAdd(messageContext.message!.Chat.Id, new Spending());
            messageContext.sessionsDictionary.TryGetValue(messageContext.message!.Chat.Id, out Spending? spending);
            spending!.Date = DateOnly.FromDateTime(DateTime.Now);

            await TelegramHelper.SendReplyAsync(messageContext.message!.Chat.Id, "Введите сумму");
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return messageContext.message!.Text!.Equals("Сегодня");
    }
}