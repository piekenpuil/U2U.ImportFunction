using System;
using System.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using U2U.External.Models;
using U2U.External.Services;

namespace U2U.External;

public class ImportrSync
{
  private readonly ILogger _logger;
  FetchService fetchService;
  TokenService tokenService;
  CourseService courseService;
  IConfiguration config; 

  public ImportrSync(ILoggerFactory loggerFactory, IConfiguration config,FetchService fetchService, TokenService tokenService, CourseService courseService)
  {
    _logger = loggerFactory.CreateLogger<ImportrSync>();
    this.fetchService = fetchService;
    this.tokenService = tokenService;
    this.courseService = courseService;
    this.config = config;
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
    //TestCall
    var i = await courseService.GetAllCoursesCountAsync();
    _logger.LogInformation($"Courses count: {i}");
    //Register Location
    (int added, int updated, int failed) locationResult = await RegisterLocationAsync();
    _logger.LogInformation($"locations new: {locationResult.added}, updated: {locationResult.updated}, failed : {locationResult.failed}");
    //Register Courses
    var info = await RegisterCoursesAsync(bobe);
    _logger.LogInformation($"courses new: {info.added}, updated: {info.updated}, failed : {info.failed}");

    if (myTimer.ScheduleStatus is not null)
    {
      _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
    }
  }

  private async Task<(int added, int updated, int failed)> RegisterCoursesAsync(BOBECourseImport bobe)
  {
    var result = await courseService.RegisterCoursesAsync(bobe);
    if (result != null)
    {
      var info = (added: result.addedEntities.Length, updated: result.updatedEntities.Length, failed: result.failedEntities.Length);
      return info;
    }
    return (0, 0, 0);
  }

  private async Task<(int added, int updated, int failed)> RegisterLocationAsync()
  {
    var dryRun = bool.Parse(config["dryRun"]);
    var courseProviderId = config["courseProviderId"];
    var locationResult = await courseService.RegisterLocation(new()
    {
      dryRun = dryRun,
      detailedResults = false,
      entities = [
          new () {
            title = "U2U",
            postalCode = "1731",
            city = "Zellik (Brussels)",
            address = "Z.1. ResearchPark 110",
            courseProviderId = courseProviderId,
            locationId="1",
            countryIsoCode = "BE"
          }
          ]
    });
    return locationResult;
  }
}