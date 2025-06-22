using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
namespace LLMZ.Services.MCP;

public class TimeMcpService
{
    StdioClientTransport clientTransport = new(new()
    {
        Name = "fetch",
        Command = "python",
        Arguments = ["-m", "mcp_server_time", "--local-timezone=Africa/Cairo"],

    });
    public async Task<IEnumerable<AIFunction>> GetToolsAsync()
    {
        var client = await McpClientFactory.CreateAsync(clientTransport);
        var tools = await client.ListToolsAsync();
        return tools;
    }
}
