namespace WebApp.API.Services
{

    public interface IAccountService
    {
        string GenerateResetUrl(string code);
    }
}
