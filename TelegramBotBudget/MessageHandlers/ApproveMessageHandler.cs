namespace TelegramBotBudget;

public class ApproveMessageHandler : AbstractTelegramMessageHandler
{
    private readonly SpendingWriter _sWriter;
    public ApproveMessageHandler(SpendingWriter sWriter)
    {
        _sWriter = sWriter;
    }
    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        //_logger.LogInformation("Верно");
        var chatId = messageContext.message!.Chat.Id;
        if (!messageContext.sessionsDictionary.TryGetValue(chatId, out Spending? spending)) return;
        if (!(spending!.Amount > 0)) return;
        if (String.Empty.Equals(spending.SubCategory)) return;

        bool writeResult = await _sWriter.WriteSpendingAsync(spending);

        if (writeResult) await TelegramHelper.SendReplyAsync(chatId, "Данные сохранены!");
        if (!writeResult) await TelegramHelper.SendReplyAsync(chatId, "Не удалось сохранить, попробуйте, может, еще разок, а?");
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return messageContext.message!.Text!.Equals("Верно");
    }
}