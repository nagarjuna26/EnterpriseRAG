namespace EnterpriseStandardsRag.Api.Models;

public class DocumentChunk
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public Dictionary<string, object> Metadata { get; set; } = new();
    public double[] Embedding { get; set; } = Array.Empty<double>();
}
