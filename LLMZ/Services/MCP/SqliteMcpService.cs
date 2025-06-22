using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
namespace LLMZ.Services.MCP;

public class SqliteMcpService
{
    StdioClientTransport clientTransport = new(new()
    {
        Name = "sqlite",
        Command = "C:\\Users\\shara\\.local\\bin\\uvx",
        Arguments = [  "mcp-server-sqlite",
          "--db-path",
      "D:/DATA.db"],

    });
public async Task<IEnumerable<AIFunction>> GetToolsAsync()
{
    var client = await McpClientFactory.CreateAsync(clientTransport);
    var tools = await client.ListToolsAsync();
    return tools;
}
}
