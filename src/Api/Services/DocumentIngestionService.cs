using EnterpriseStandardsRag.Api.Models;

namespace EnterpriseStandardsRag.Api.Services;

public class DocumentIngestionService
{
    private readonly InMemoryVectorStore _vectorStore;
    private readonly ChunkingService _chunkingService;

    public DocumentIngestionService(InMemoryVectorStore vectorStore, ChunkingService chunkingService)
    {
        _vectorStore = vectorStore;
        _chunkingService = chunkingService;
    }

    public int IngestDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
        }

        var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
            .Where(file => file.EndsWith(".md", StringComparison.OrdinalIgnoreCase) ||
                           file.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));

        var totalChunks = 0;

        foreach (var file in files)
        {
            var text = File.ReadAllText(file);
            var chunks = _chunkingService.CreateChunks(Path.GetFileName(file), text);

            foreach (var chunk in chunks)
            {
                _vectorStore.Add(chunk);
                totalChunks++;
            }
        }

        return totalChunks;
    }
}
