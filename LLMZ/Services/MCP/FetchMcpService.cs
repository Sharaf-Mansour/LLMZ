using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
namespace LLMZ.Services.MCP;
public class FetchMcpService
{
    StdioClientTransport clientTransport = new(new()
    {
        Name = "fetch",
        Command = "python",
        Arguments = ["-m", "mcp_server_fetch"],
    });
    public async Task<IEnumerable<AIFunction>> GetToolsAsync()
    {
        var client = await McpClientFactory.CreateAsync(clientTransport);
        var tools = await client.ListToolsAsync();
        return tools;
    }
}
