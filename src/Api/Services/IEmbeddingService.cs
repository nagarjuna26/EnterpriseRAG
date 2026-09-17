namespace EnterpriseStandardsRag.Api.Services;

public interface IEmbeddingService
{
    double[] GenerateEmbedding(string text);
}
