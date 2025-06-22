using LLMZ.Components;
using LLMZ.Services;
using LLMZ.Services.Ingestion;
using LLMZ.Services.MCP;
using Microsoft.Extensions.AI;
using OllamaSharp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

IChatClient chatClient = new OllamaApiClient(new Uri("http://localhost:11434"),
    "qwen3");
IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = new OllamaApiClient(new Uri("http://localhost:11434"),
    "all-minilm");

var vectorStorePath = Path.Combine(AppContext.BaseDirectory, "vector-store.db");
var vectorStoreConnectionString = $"Data Source={vectorStorePath}";
builder.Services.AddSqliteCollection<string, IngestedChunk>("data-llmz-chunks", vectorStoreConnectionString);
builder.Services.AddSqliteCollection<string, IngestedDocument>("data-llmz-documents", vectorStoreConnectionString);
builder.Services.AddSingleton<GitHubMcpService>();
builder.Services.AddSingleton<EverythingMcpService>();
builder.Services.AddSingleton<FetchMcpService>();
builder.Services.AddSingleton<SqliteMcpService>();
builder.Services.AddSingleton<TimeMcpService>();
builder.Services.AddSingleton<ThinkingMcpService>();
builder.Services.AddSingleton<FileSystemMcpService>();
builder.Services.AddScoped<DataIngestor>();
builder.Services.AddSingleton<SemanticSearch>();
builder.Services.AddChatClient(chatClient).UseFunctionInvocation().UseLogging();
builder.Services.AddEmbeddingGenerator(embeddingGenerator);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// By default, we ingest PDF files from the /wwwroot/Data directory. You can ingest from
// other sources by implementing IIngestionSource.
// Important: ensure that any content you ingest is trusted, as it may be reflected back
// to users or could be a source of prompt injection risk.
await DataIngestor.IngestDataAsync(
    app.Services,
    new PDFDirectorySource(Path.Combine(builder.Environment.WebRootPath, "Data")));


app.Run();
