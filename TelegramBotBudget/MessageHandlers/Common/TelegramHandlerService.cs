namespace TelegramBotBudget;

public class TelegramHandlerService
{
    private readonly SpendingWriter _sWriter;
    private readonly Category _category;
    private static ITelegramMessageHandler _handler = null!;
    public TelegramHandlerService(SpendingWriter sWriter, InitialHandlerService handler, Category category)
    {
        _sWriter = sWriter;
        _handler = handler;
        _category = category;
        InitHandlersChain();
    }

    private void InitHandlersChain()
    {
        _handler.SetNext(new StartMessageHandler())
            .SetNext(new LinkMessageHandler())
            .SetNext(new TodayMessageHandler())
            .SetNext(new YesterdayMessageHandler())
            .SetNext(new ApproveMessageHandler(_sWriter))
            .SetNext(new DoubleMessageHandler(_category))
            .SetNext(new CategoriesMessageHandler(_category))
            .SetNext(new SubcategoriesMessageHandler(_category));
    }

    public ITelegramMessageHandler GetHandler()
    {
        return _handler;
    }
}