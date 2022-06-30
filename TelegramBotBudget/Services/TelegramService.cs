namespace TelegramBotBudget;

using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Collections.Concurrent;

public class TelegramService : BackgroundService
{
    private string token;
    private readonly ILogger<TelegramService> _logger;
    private readonly SpendingWriter _sWriter;
    private readonly Category _category;
    private readonly IConfiguration _config;
    private ConcurrentDictionary<long, Spending> pendingSpendingsDict = new ConcurrentDictionary<long, Spending>();
    private ITelegramBotClient bot;
    private TelegramHandlerService _telegramHandlerService;
    public TelegramService(ILogger<TelegramService> logger, SpendingWriter sWriter, Category category, TelegramHandlerService telegramHandlerService, IConfiguration config)
    {
        _logger = logger;
        _sWriter = sWriter;
        _category = category;
        _telegramHandlerService = telegramHandlerService;
        _config = config;
        token = _config["TelegramToken"];
        bot = new TelegramBotClient(token);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("");

        BotCommand[] cmds = new BotCommand[]{
                new BotCommand(){Command="/start",Description="Начать заново"},
                new BotCommand(){Command="/link",Description="Ссылка на данные"},
                new BotCommand(){Command="/help",Description="Справка"}
            };

        await bot.SetMyCommandsAsync(cmds);

        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };

        await Task.Run(() => bot.StartReceiving(
                                updateHandler: HandleUpdateAsync,
                                errorHandler: HandleErrorAsync,
                                receiverOptions: receiverOptions,
                                cancellationToken: stoppingToken
                            )
        );

        User me = await bot.GetMeAsync();

        _logger.LogInformation("Started bot {0}", me.FirstName);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        string jsonString = JsonSerializer.Serialize(update);
        _logger.LogInformation("New message");
        _logger.LogInformation(jsonString);

        if (update.Type != UpdateType.Message) return;

        if (update.Message is null) return;

        if (update.Message.Text is null) return;

        if (update.Message!.Type != MessageType.Text) return;

        var message = update.Message;
        TelegramHelper.Init(botClient);//need to pass within message context
        ITelegramMessageHandler handler = _telegramHandlerService.GetHandler();
        await handler.Handle(new() { message = message, sessionsDictionary = pendingSpendingsDict });
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogError(Newtonsoft.Json.JsonConvert.SerializeObject(exception)));
    }

}