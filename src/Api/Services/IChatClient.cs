namespace EnterpriseStandardsRag.Api.Services;

public interface IChatClient
{
    string GenerateResponse(string prompt);
}
