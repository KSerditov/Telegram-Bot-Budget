using System.Text.Json;

public class Category
{
    private IConfiguration _config;

    private Dictionary<string, List<string>> categories { get; set; } = null!;

    public Category(IConfiguration config)
    {
        _config = config;
        LoadFromFile();
    }

    private async void LoadFromFile()
    {
        using (FileStream fileStream = File.OpenRead(_config["CATEGORIES_FILE_NAME"]))
        {
            categories = (await JsonSerializer.DeserializeAsync<Dictionary<string, List<string>>>(fileStream))!;
        }
    }

    public List<string> GetCategories()
    {
        return categories.Keys.ToList<string>();
    }

    public List<string> GetSubCategories(string category)
    {
        if (!categories.ContainsKey(category)) return new List<string>();
        return categories[category];
    }

}