using EnterpriseStandardsRag.Api.Models;

namespace EnterpriseStandardsRag.Api.Services;

public class PromptBuilderService
{
    public string BuildPrompt(string question, IReadOnlyList<DocumentChunk> contextChunks)
    {
        var context = string.Join("\n\n", contextChunks.Select(chunk => $"Source: {chunk.Source}\n{chunk.Text}"));

        return $"You are an enterprise standards assistant. Use only the provided context to answer the user question.\n\nContext:\n{context}\n\nQuestion:\n{question}\n\nAnswer:";
    }
}
