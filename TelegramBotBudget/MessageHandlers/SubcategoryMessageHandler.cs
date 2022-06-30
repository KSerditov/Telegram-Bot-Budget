
namespace TelegramBotBudget;

public class SubcategoriesMessageHandler : AbstractTelegramMessageHandler
{
    private readonly Category _category;

    public SubcategoriesMessageHandler(Category category)
    {
        _category = category;
    }

    protected override async void HandleThis(ContextAwareMessage messageContext)
    {
        messageContext.sessionsDictionary.TryGetValue(messageContext.message!.Chat.Id, out Spending? spending);
        spending!.SubCategory = messageContext.message!.Text!;

        if (spending.Date == null) spending.Date = DateOnly.FromDateTime(DateTime.Now);

        await TelegramHelper.SendReplyAsync(
            messageContext.message.Chat.Id,
            $"Дата: {spending.Date.ToString()}{Environment.NewLine}Сумма: {spending.Amount}{Environment.NewLine}Категория: {spending.Category} -> {spending.SubCategory}{Environment.NewLine}Верно?",
            TelegramHelper.GetCategoriesReplyKeyboard(new string[2] { "Верно", "Ввести снова" }.ToList())
            );
    }

    protected override bool VerifyValidity(ContextAwareMessage messageContext)
    {
        if (!messageContext.sessionsDictionary.TryGetValue(messageContext.message!.Chat.Id, out Spending? spending)) return false;
        if (String.Empty.Equals(spending.Category)) return false;
        return _category.GetSubCategories(spending.Category).Contains(messageContext.message!.Text!);
    }
}
