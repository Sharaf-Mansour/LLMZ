using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
namespace LLMZ.Services.MCP;

public class ThinkingMcpService
{
    StdioClientTransport clientTransport = new(new()
    {
        Name = "sequential-thinking",
        Command = "bunx",
        Arguments = [ "-y",
        "@modelcontextprotocol/server-sequential-thinking"],

    });
    public async Task<IEnumerable<AIFunction>> GetToolsAsync()
    {
        var client = await McpClientFactory.CreateAsync(clientTransport);
        var tools = await client.ListToolsAsync();
        return tools;
    }
}