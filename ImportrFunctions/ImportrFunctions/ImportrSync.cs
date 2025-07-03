using System;
using ImportrFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ImportrFunctions;

public class ImportrSync
{
  private readonly ILogger _logger;
  FetchService fetchService;
  TokenService tokenService;

  public ImportrSync(ILoggerFactory loggerFactory, FetchService fetchService, TokenService tokenService)
  {
    _logger = loggerFactory.CreateLogger<ImportrSync>();
    this.fetchService = fetchService;
    this.tokenService = tokenService;
  }
  //url: /admin/functions/synccourses
  [Function("SyncCourses")]
  public async Task Run([TimerTrigger("* * * 1 * *"
    #if DEBUG
        ,RunOnStartup = true
    #endif
    )] TimerInfo myTimer)
  {

    _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);


    //fetchInfo
    var bobe = await fetchService.GetCoursesAsync();
    if (bobe == null)
    {
      return;
    }
    //GetToken
    var token = await tokenService.GetTokenAsync();
    //TestCall

    //Register Location
    //Register Courses

    if (myTimer.ScheduleStatus is not null)
    {
      _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
    }
  }
}