using EnterpriseStandardsRag.Api.Services;

namespace EnterpriseStandardsRag.Tests;

public class RetrievalPipelineTests
{
    [Fact]
    public void Retriever_ReturnsMostRelevantChunk()
    {
        var embeddingService = new DeterministicEmbeddingService();
        var vectorStore = new InMemoryVectorStore(embeddingService);

        vectorStore.Add(new DocumentChunk
        {
            Id = "security-1",
            Text = "Security policy requires multi-factor authentication for privileged access.",
            Source = "SecurityStandards.md",
            Metadata = new Dictionary<string, object> { ["documentType"] = "policy" }
        });

        vectorStore.Add(new DocumentChunk
        {
            Id = "messaging-1",
            Text = "Messaging platform uses an event-driven architecture with asynchronous contracts.",
            Source = "MessagingStandards.md",
            Metadata = new Dictionary<string, object> { ["documentType"] = "design" }
        });

        var retriever = new RetrieverService(vectorStore, embeddingService, topK: 1);

        var results = retriever.Search("What is required for privileged access?", 1);

        Assert.Contains(results, result => result.Source == "SecurityStandards.md");
    }

    [Fact]
    public void DocumentIngestionService_LoadsMarkdownStandardsFiles()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), $"rag-standards-{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempDirectory);

        File.WriteAllText(Path.Combine(tempDirectory, "SecurityStandards.md"),
            "# Security Standards\n\nAll privileged access requires multi-factor authentication.\n\nSecrets must be stored in managed secret storage.");

        File.WriteAllText(Path.Combine(tempDirectory, "ApiStandards.md"),
            "# API Standards\n\nAll APIs must support idempotent POST semantics and use structured error payloads.");

        var vectorStore = new InMemoryVectorStore(new DeterministicEmbeddingService());
        var ingestionService = new DocumentIngestionService(vectorStore, new ChunkingService());

        var chunkCount = ingestionService.IngestDirectory(tempDirectory);

        Assert.True(chunkCount >= 2);
        Assert.NotEmpty(vectorStore.GetAll());

        Directory.Delete(tempDirectory, recursive: true);
    }
}
