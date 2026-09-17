using EnterpriseStandardsRag.Api.Infrastructure;
using EnterpriseStandardsRag.Api.Models;
using EnterpriseStandardsRag.Api.Services;

namespace EnterpriseStandardsRag.Api.Endpoints;

public static class RagEndpoints
{
    public static void MapRagEndpoints(this WebApplication app)
    {
        app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

        app.MapPost("/api/rag/query", (AskRequest request, RagPipeline pipeline) =>
        {
            var response = pipeline.Ask(request.Question);
            return Results.Ok(new { answer = response });
        });

        app.MapPost("/api/rag/chunks", (DocumentChunk chunk, RagPipeline pipeline) =>
        {
            pipeline.AddDocumentChunk(chunk);
            return Results.Ok(new { message = "Chunk indexed successfully." });
        });

        app.MapGet("/api/rag/status", (InMemoryVectorStore vectorStore) =>
        {
            var chunks = vectorStore.GetAll();
            return Results.Ok(new
            {
                chunkCount = chunks.Count,
                sources = chunks.Select(c => c.Source).Distinct().OrderBy(s => s).ToList()
            });
        });

        app.MapPost("/api/rag/ingest", (IngestRequest request, DocumentIngestionService ingestionService) =>
        {
            var count = ingestionService.IngestDirectory(request.DirectoryPath);
            return Results.Ok(new { ingestedChunks = count, directory = request.DirectoryPath });
        });
    }

    public sealed class AskRequest
    {
        public string Question { get; set; } = string.Empty;
    }

    public sealed class IngestRequest
    {
        public string DirectoryPath { get; set; } = string.Empty;
    }
}
