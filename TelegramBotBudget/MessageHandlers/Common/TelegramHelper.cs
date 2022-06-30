using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotBudget;

public static class TelegramHelper
{
    private static ITelegramBotClient _botClient = null!;

    public static void Init(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public static async Task InitiateSpendingInput(Message message)
    {
        Message sentKeyMessage = await SendReplyAsync(
            message.Chat.Id,
            "Выберите день в меню ниже",
            GetDaysReplyKeyboard());
    }

    public static ReplyKeyboardMarkup GetDaysReplyKeyboard()
    {
        return new ReplyKeyboardMarkup(new KeyboardButton[] { "Сегодня", "Вчера", "Ввести день" })
        {
            ResizeKeyboard = true
        };
    }

    public static ReplyKeyboardMarkup GetCategoriesReplyKeyboard(List<string> names)
    {
        List<KeyboardButton[]> buttons = new List<KeyboardButton[]>();
        List<KeyboardButton> row = new List<KeyboardButton>();
        for (var i = 0; i < names.Count; i++)
        {
            row.Add(new KeyboardButton(names[i]));
            if (i + 1 % 4 != 0) continue;
            buttons.Add(row.ToArray());
            row = new List<KeyboardButton>();
        }
        buttons.Add(row.ToArray());

        ReplyKeyboardMarkup replyKeyboardMarkup = new(buttons.ToArray())
        {
            OneTimeKeyboard = true,
            ResizeKeyboard = true
        };

        return replyKeyboardMarkup;
    }

    public static async Task<Message> SendReplyAsync(long chatId, string text, IReplyMarkup replyMarkup)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            replyMarkup: replyMarkup);
    }

    public static async Task<Message> SendReplyAsync(long chatId, string text)
    {
        return await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text);
    }
}