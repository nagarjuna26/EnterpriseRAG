using EnterpriseStandardsRag.Api.Models;

namespace EnterpriseStandardsRag.Api.Services;

public class RetrieverService
{
    private readonly InMemoryVectorStore _vectorStore;
    private readonly IEmbeddingService _embeddingService;
    private readonly int _topK;

    public RetrieverService(InMemoryVectorStore vectorStore, IEmbeddingService embeddingService, int topK = 3)
    {
        _vectorStore = vectorStore;
        _embeddingService = embeddingService;
        _topK = topK;
    }

    public IReadOnlyList<DocumentChunk> Search(string query, int limit = 3)
    {
        var queryEmbedding = _embeddingService.GenerateEmbedding(query);
        var results = _vectorStore.GetAll()
            .Select(chunk => new
            {
                Chunk = chunk,
                Score = CosineSimilarity(queryEmbedding, chunk.Embedding)
            })
            .OrderByDescending(x => x.Score)
            .Take(Math.Max(1, limit > 0 ? limit : _topK))
            .Select(x => x.Chunk)
            .ToList();

        return results;
    }

    private static double CosineSimilarity(double[] a, double[] b)
    {
        if (a.Length != b.Length)
        {
            throw new InvalidOperationException("Embedding lengths must match.");
        }

        var dot = 0d;
        var aMagnitude = 0d;
        var bMagnitude = 0d;

        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            aMagnitude += a[i] * a[i];
            bMagnitude += b[i] * b[i];
        }

        if (aMagnitude == 0d || bMagnitude == 0d)
        {
            return 0d;
        }

        return dot / (Math.Sqrt(aMagnitude) * Math.Sqrt(bMagnitude));
    }
}
