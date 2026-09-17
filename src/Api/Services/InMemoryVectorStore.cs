using EnterpriseStandardsRag.Api.Models;

namespace EnterpriseStandardsRag.Api.Services;

public class InMemoryVectorStore
{
    private readonly IEmbeddingService _embeddingService;
    private readonly List<DocumentChunk> _chunks = new();

    public InMemoryVectorStore(IEmbeddingService embeddingService)
    {
        _embeddingService = embeddingService;
    }

    public void Add(DocumentChunk chunk)
    {
        chunk.Embedding = _embeddingService.GenerateEmbedding(chunk.Text);
        _chunks.Add(chunk);
    }

    public IReadOnlyList<DocumentChunk> GetAll() => _chunks.AsReadOnly();
}
