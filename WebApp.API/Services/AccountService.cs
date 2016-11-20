using Microsoft.Extensions.Options;

public class AccountService : IAccountService
{
    private readonly ClientSettings _settings;

    public AccountService(IOptions<ClientSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GenerateResetUrl(string code)
    {
        string clientUrl = _settings.BaseUrl;
        string callbackUrl = $"{clientUrl}/resetPassword?code={code}";
        return callbackUrl;
    }
}
