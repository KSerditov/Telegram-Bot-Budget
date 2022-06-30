
namespace TelegramBotBudget;

public class CategoriesMessageHandler : AbstractTelegramMessageHandler
{
    private readonly Category _category;

    public CategoriesMessageHandler(Category category)
    {
        _category = category;
    }

    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        var chatId = messageContext.message!.Chat.Id;
        var category = messageContext.message!.Text!;

        messageContext.sessionsDictionary.TryGetValue(chatId, out Spending? spending);
        spending!.Category = category;

        await TelegramHelper.SendReplyAsync(chatId, "Выберите подкатегорию", TelegramHelper.GetCategoriesReplyKeyboard(_category.GetSubCategories(category)));
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        if (!_category.GetCategories().Contains(messageContext.message!.Text!)) return false;
        if (!messageContext.sessionsDictionary.TryGetValue(messageContext.message.Chat.Id, out Spending? spending)) return false;
        return spending.Amount > 0;
    }
}