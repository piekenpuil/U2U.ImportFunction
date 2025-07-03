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

  }
}
