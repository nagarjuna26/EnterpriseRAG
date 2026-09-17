using System.Text;

namespace EnterpriseStandardsRag.Api.Services;

public class DeterministicEmbeddingService : IEmbeddingService
{
    public double[] GenerateEmbedding(string text)
    {
        var normalized = text.Trim();
        var bytes = Encoding.UTF8.GetBytes(normalized);
        var vector = new double[8];

        for (var i = 0; i < bytes.Length; i++)
        {
            var index = i % vector.Length;
            vector[index] += bytes[i];
        }

        for (var i = 0; i < vector.Length; i++)
        {
            vector[i] = Math.Round(vector[i] / Math.Max(1, bytes.Length / vector.Length), 6);
        }

        return vector;
    }
}
