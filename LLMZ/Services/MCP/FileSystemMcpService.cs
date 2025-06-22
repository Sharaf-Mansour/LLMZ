using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
namespace LLMZ.Services.MCP;

public class FileSystemMcpService
{
    StdioClientTransport clientTransport = new(new()
    {
        Name = "fetch",
        Command = "bunx",
        Arguments = [ "-y",
        "@modelcontextprotocol/server-filesystem",
        "/Users/shara/Desktop",
        "D:/"],

    });
    public async Task<IEnumerable<AIFunction>> GetToolsAsync()
    {
        var client = await McpClientFactory.CreateAsync(clientTransport);
        var tools = await client.ListToolsAsync();
        return tools;
    }
}
