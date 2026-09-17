using EnterpriseStandardsRag.Api.Models;
using EnterpriseStandardsRag.Api.Services;

namespace EnterpriseStandardsRag.Api.Infrastructure;

public class RagPipeline
{
    private readonly InMemoryVectorStore _vectorStore;
    private readonly RetrieverService _retriever;
    private readonly PromptBuilderService _promptBuilder;
    private readonly IChatClient _chatClient;

    public RagPipeline(
        InMemoryVectorStore vectorStore,
        RetrieverService retriever,
        PromptBuilderService promptBuilder,
        IChatClient chatClient)
    {
        _vectorStore = vectorStore;
        _retriever = retriever;
        _promptBuilder = promptBuilder;
        _chatClient = chatClient;
    }

    public string Ask(string question)
    {
        var chunks = _retriever.Search(question, 3);
        var prompt = _promptBuilder.BuildPrompt(question, chunks);
        return _chatClient.GenerateResponse(prompt);
    }

    public void AddDocumentChunk(DocumentChunk chunk)
    {
        _vectorStore.Add(chunk);
    }
}
