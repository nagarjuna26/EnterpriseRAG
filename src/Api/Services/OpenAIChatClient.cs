namespace EnterpriseStandardsRag.Api.Services;

public class OpenAIChatClient : IChatClient
{
    private readonly string _apiKey;
    private readonly string _model;

    public OpenAIChatClient(string apiKey, string model = "gpt-4o-mini")
    {
        _apiKey = apiKey;
        _model = model;
    }

    public string GenerateResponse(string prompt)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            return "OpenAI API key is not configured. Add a key to enable LLM-backed responses.";
        }

        return $"[OpenAI model: {_model}] Response generated from prompt: {prompt.Substring(0, Math.Min(prompt.Length, 120))}...";
    }
}
