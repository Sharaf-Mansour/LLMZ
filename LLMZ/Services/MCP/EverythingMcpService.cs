using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
namespace LLMZ.Services.MCP;
public class EverythingMcpService
{
    StdioClientTransport clientTransport = new(new()
    {
        Name = "everything",
        Command = "bunx",
        Arguments = ["-y", "@modelcontextprotocol/server-everything"],
     
    });
    public async Task<IEnumerable<AIFunction>> GetToolsAsync()
    {
        var client = await McpClientFactory.CreateAsync(clientTransport);
        var tools = await client.ListToolsAsync();
        return tools;
    }
}