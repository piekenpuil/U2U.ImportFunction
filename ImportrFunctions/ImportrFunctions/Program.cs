using ImportrFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();



builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var bobeUrl = builder.Configuration["BOBEURL"];
var importRUrl = builder.Configuration["ImportRURL"];

builder.Services.AddHttpClient<FetchService>(c => c.BaseAddress = new Uri(bobeUrl));
builder.Services.AddHttpClient<TokenService>(c=>c.BaseAddress = new Uri(importRUrl));

builder.Build().Run();
