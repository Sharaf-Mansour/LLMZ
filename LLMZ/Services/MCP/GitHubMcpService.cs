using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Microsoft.Extensions.AI;
using System.Collections;

namespace LLMZ.Services.MCP;
public class GitHubMcpService
{
 
    StdioClientTransport clientTransport = new  (new ()
    {
        Name = "GitHub",
        Command = "bunx",
        Arguments = ["-y", "@modelcontextprotocol/server-github"],
        EnvironmentVariables = new Dictionary<string, string?>
        {
            { "GITHUB_PERSONAL_ACCESS_TOKEN",
"Your PAT"
            }
        }
    });
    public async Task<IEnumerable<AIFunction>> GetToolsAsync()
    {
        var client = await McpClientFactory.CreateAsync(clientTransport);
        var tools =   await client.ListToolsAsync();
        return tools;
    }
}