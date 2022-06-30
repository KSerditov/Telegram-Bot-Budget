namespace TelegramBotBudget;
public class Spending
{
    private DateOnly? _date;
    private Double _amount;
    private string _category = String.Empty;
    private string _subcategory = String.Empty;
    private string _description = String.Empty;

    public DateOnly? Date
    {
        get { return _date; }
        set { _date = value; }
    }

    public Double Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public string Category
    {
        get { return _category; }
        set { _category = value; }
    }

    public string SubCategory
    {
        get { return _subcategory; }
        set { _subcategory = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

}