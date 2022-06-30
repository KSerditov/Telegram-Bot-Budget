namespace TelegramBotBudget;
public class SpendingWriter
{
    private readonly ILogger<SpendingWriter> _logger;
    private readonly GoogleSheetsHelper _googleHelper;

    public SpendingWriter(ILogger<SpendingWriter> logger, GoogleSheetsHelper helper)
    {
        _logger = logger;
        _googleHelper = helper;
    }

    public async Task<bool> WriteSpendingAsync(Spending spending)
    {
        if (spending is null) return false;

        List<IList<Object>> values = new List<IList<object>>();

        IList<Object> obj = new List<Object>();

        try
        {
            obj.Add(spending.Date!.Value.ToString("dd/MM/yyyy"));
            obj.Add(spending.Amount);
            obj.Add(spending.Category ?? String.Empty);
            obj.Add(spending.SubCategory ?? String.Empty);
            obj.Add(spending.Description ?? String.Empty);

            values.Add(obj);

            bool writeResult = await _googleHelper.WriteRowAsync(values);
            return writeResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured while writing Spending");
        }
        return false;
    }

}