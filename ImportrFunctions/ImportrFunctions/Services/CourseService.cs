using ImportrFunctions.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImportrFunctions.Services
{
  public class CourseService
  {
    HttpClient client;
    IConfiguration config;

    public CourseService(HttpClient client,IConfiguration config)
    {
      this.client = client;
      this.config = config;
    }

    //To my surprise this counts all courses including from other providers
    public async Task<int> GetAllCoursesCountAsync()
    {
      var resp = await client.GetAsync(config["CourseUrl"]);
      var sCount = await resp.Content.ReadAsStringAsync();
      if (int.TryParse(sCount, out var coursesCount))
      {
        return coursesCount;
      }
      ;
      return 0;
    }

    public async Task<(int added,int updated,int failed)> RegisterLocation(ImportingCourseProviderLocationImportingBundle locationInfo)
    {
      var sLocation = JsonSerializer.Serialize(locationInfo);
      StringContent body = new StringContent(sLocation, Encoding.UTF8, "application/json");
      var resp = await client.PostAsync("/importrapi/v1/importingProviderLocation/importbundle", body);
      var sInfo = await resp.Content.ReadAsStringAsync();
      var info = JsonSerializer.Deserialize<ImportingEntityImportingEntityResult>(sInfo);
      return (info.countAdded,info.countUpdated,info.countFailed);
    }

    public async Task<ImportingEntityImportingEntityResult> RegisterCoursesAsync(BOBECourseImport courseInfo)
    {
      var dryRun = bool.Parse(config["dryRun"]);
      courseInfo.dryRun = dryRun;
      var sCourses = JsonSerializer.Serialize(courseInfo);
      StringContent body = new StringContent(sCourses, Encoding.UTF8, "application/json");
      var resp = await client.PostAsync(config["CourseImportUrl"], body);
      var sInfo = await resp.Content.ReadAsStringAsync();
      var info = JsonSerializer.Deserialize<ImportingEntityImportingEntityResult>(sInfo);
      return info;
    }

  }
}
