namespace TelegramBotBudget;

public class DoubleMessageHandler : AbstractTelegramMessageHandler
{
    private readonly Category _category;

    public DoubleMessageHandler(Category category)
    {
        _category = category;
    }

    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        Double.TryParse(messageContext.message!.Text!.Replace(',', '.'), out Double value);
        var chatId = messageContext.message!.Chat.Id;

        messageContext.sessionsDictionary.TryAdd(chatId, new Spending());
        messageContext.sessionsDictionary.TryGetValue(chatId, out Spending? spending);
        spending!.Amount = value;

        await TelegramHelper.SendReplyAsync(chatId, "Выберите категорию", TelegramHelper.GetCategoriesReplyKeyboard(_category.GetCategories()));
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        return Double.TryParse(messageContext.message!.Text!.Replace(',', '.'), out Double _);
    }
}