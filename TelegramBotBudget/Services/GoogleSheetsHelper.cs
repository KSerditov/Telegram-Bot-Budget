using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

public class GoogleSheetsHelper
{
    private readonly ILogger<GoogleSheetsHelper> _logger;
    private readonly IConfiguration _config;
    public SheetsService Service { get; set; } = null!;
    private static string SHEET_ID = null!;
    private const string RANGE = "A:R"; // number of columns in row - Google Sheets uses this to search for 'table' and append on next empty line;
                                        // A:D refers to entire columns A,B,C,D
    static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    public GoogleSheetsHelper(ILogger<GoogleSheetsHelper> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
        InitializeService();
    }
    private void InitializeService()
    {
        var credential = GetCredentialsFromFile();
        SHEET_ID = _config["GoogleSettings:SHEET_ID"];
        Service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _config["GoogleSettings:APPLICATION_NAME"]
        });
    }
    private GoogleCredential GetCredentialsFromFile()
    {
        GoogleCredential credential;
        using (var stream = new FileStream(_config["GoogleSettings:KEYFILE_NAME"], FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }
        return credential;
    }

    public async Task<bool> WriteRowAsync(List<IList<Object>> values)
    {
        try
        {
            SpreadsheetsResource.ValuesResource.AppendRequest request =
               Service.Spreadsheets.Values.Append(new ValueRange() { Values = values }, SHEET_ID, $"{_config["GoogleSettings:SHEET_NAME"]}!{RANGE}");
            request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            AppendValuesResponse response = await request.ExecuteAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured while writing to sheet");
        }
        return false;
    }

    public static Uri GetGSheetLink()
    {
        return new Uri(@"https://docs.google.com/spreadsheets/d/" + SHEET_ID);
    }

}