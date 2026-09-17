using EnterpriseStandardsRag.Api.Endpoints;
using EnterpriseStandardsRag.Api.Infrastructure;
using EnterpriseStandardsRag.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmbeddingService, DeterministicEmbeddingService>();
builder.Services.AddSingleton<InMemoryVectorStore>();
builder.Services.AddSingleton<ChunkingService>();
builder.Services.AddSingleton<RetrieverService>();
builder.Services.AddSingleton<PromptBuilderService>();
builder.Services.AddSingleton<DocumentIngestionService>();
builder.Services.AddSingleton<IChatClient>(_ => new OpenAIChatClient(
    builder.Configuration["OpenAI:ApiKey"] ?? string.Empty,
    builder.Configuration["OpenAI:Model"] ?? "gpt-4o-mini"));
builder.Services.AddSingleton<RagPipeline>();

var app = builder.Build();

var standardsDirectory = Path.Combine(app.Environment.ContentRootPath, "Standards");
if (Directory.Exists(standardsDirectory))
{
    using var scope = app.Services.CreateScope();
    var ingestionService = scope.ServiceProvider.GetRequiredService<DocumentIngestionService>();
    var ingestedChunkCount = ingestionService.IngestDirectory(standardsDirectory);
    app.Logger.LogInformation("Seeded {ChunkCount} chunks from {Directory}", ingestedChunkCount, standardsDirectory);
}

app.MapGet("/", () => "Enterprise Standards RAG API is running.");
app.MapRagEndpoints();

app.Run();
