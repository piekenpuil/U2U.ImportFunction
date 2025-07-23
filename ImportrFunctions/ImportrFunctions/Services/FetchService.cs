using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using U2U.External.Models;

namespace U2U.External.Services
{
  public class FetchService
  {
    HttpClient httpClient;
    IConfiguration config;
    ILogger<FetchService> logger;

    public FetchService(HttpClient httpClient, IConfiguration config, ILogger<FetchService> logger)
    {
      this.httpClient = httpClient;
      this.config = config;
      this.logger = logger;
    }

    public async Task<BOBECourseImport?> GetCoursesAsync()
    {
      var respo = await httpClient.GetAsync("");

      if (respo.IsSuccessStatusCode)
      {
        var s = await respo.Content.ReadAsStringAsync();
        var bobe = JsonSerializer.Deserialize<BOBECourseImport>(s);
        PatchCourses(bobe);
        return bobe;
      }
      else
      {
        logger.LogError("Error reaching U2U BOBE Export");
        return null;
      }
    }

    private void PatchCourses(BOBECourseImport? bobe)
    {
      var urls = bobe.entities.Select(b=>b.urlToCourseDetails).Distinct().ToList();
      //for now...
      var tokeep = bobe.entities.Where(b => b.providerCourseCategoryId != "305" 
        && b.providerCourseCategoryId != "312" && b.startTime<DateTime.Now.AddMonths(1)).ToList();
      bobe.entities = tokeep;
      foreach (var course in bobe.entities)
      {
        course.urlToCourseDetails = "https://u2u.be/training/Models.Bookings.Core.Entities.Seourl";
        course.maxParticipants = 25;
        if (course.levelId == "None")
        {
          course.levelId = "none";
        }
        if (course.description.Length < 64)
        {
          course.description = string.Concat(course.description,new string(' ',64-course.description.Length));
        }
        if (course.description.Length>1024)
        {
          course.description = course.description.Substring(0, 1024);
        }
        if (course.goals.Length>512)
        {
          course.goals = course.goals.Substring(0, 512);
        }
        if (course.locationId=="online")
        {
          course.providerCourseSessionId = string.Concat(course.providerCourseSessionId, "ol");
        }
      }
    }
  }
}
