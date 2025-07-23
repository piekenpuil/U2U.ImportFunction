using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using U2U.External.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();



builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var bobeUrl = builder.Configuration["BOBEURL"];
var importRUrl = builder.Configuration["ImportRURL"];

builder.Services.AddScoped<ImportrDelegatingHandler>();
builder.Services.AddHttpClient<FetchService>(c => c.BaseAddress = new Uri(bobeUrl));
builder.Services.AddHttpClient<TokenService>(c => c.BaseAddress = new Uri(importRUrl));
builder.Services.AddHttpClient<CourseService>(c => 
{ 
  c.BaseAddress = new Uri(importRUrl); 
  c.Timeout = TimeSpan.FromMinutes(5);
})
  .AddHttpMessageHandler<ImportrDelegatingHandler>();

builder.Build().Run();
