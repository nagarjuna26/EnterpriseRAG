using EnterpriseStandardsRag.Api.Models;

namespace EnterpriseStandardsRag.Api.Services;

public class ChunkingService
{
    private const int DefaultChunkSize = 500;
    private const int DefaultOverlap = 80;

    public IReadOnlyList<string> ChunkText(string text, int chunkSize = DefaultChunkSize, int overlap = DefaultOverlap)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Array.Empty<string>();
        }

        var normalized = text.Replace("\r\n", "\n");
        var paragraphs = normalized
            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var chunks = new List<string>();
        var current = new List<string>();
        var currentLength = 0;

        foreach (var paragraph in paragraphs)
        {
            if (paragraph.Length + currentLength <= chunkSize)
            {
                current.Add(paragraph);
                currentLength += paragraph.Length;
                continue;
            }

            if (current.Count > 0)
            {
                chunks.Add(string.Join("\n\n", current));
            }

            var paragraphParts = SplitParagraph(paragraph, chunkSize, overlap);
            chunks.AddRange(paragraphParts);
            current.Clear();
            currentLength = 0;
        }

        if (current.Count > 0)
        {
            chunks.Add(string.Join("\n\n", current));
        }

        return chunks;
    }

    public IReadOnlyList<DocumentChunk> CreateChunks(string source, string text)
    {
        return ChunkText(text)
            .Select((chunk, index) => new DocumentChunk
            {
                Id = $"{source}-{index}",
                Text = chunk,
                Source = source,
                Metadata = new Dictionary<string, object>
                {
                    ["chunkIndex"] = index,
                    ["source"] = source
                }
            })
            .ToList();
    }

    private static List<string> SplitParagraph(string paragraph, int chunkSize, int overlap)
    {
        var result = new List<string>();
        var start = 0;

        while (start < paragraph.Length)
        {
            var end = Math.Min(start + chunkSize, paragraph.Length);
            var chunk = paragraph.Substring(start, end - start);
            result.Add(chunk.Trim());

            if (end == paragraph.Length)
            {
                break;
            }

            start = Math.Max(start + chunkSize - overlap, start + 1);
        }

        return result;
    }
}
