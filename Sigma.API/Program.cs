using Sigma.API;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var serviceConfigurator = new ServiceConfigurator(builder.Services, configuration);
serviceConfigurator.ConfigureServices();

var app = builder.Build();

var pipelineConfigurator = new PipelineConfigurator(app);
pipelineConfigurator.ConfigurePipeline();

app.Run();