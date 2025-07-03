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

    public FetchService(HttpClient httpClient, IConfiguration config,ILogger<FetchService> logger)
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
        return bobe;
      }
      else
      {
        logger.LogError("Error reaching U2U BOBE Export");
        return null;
      }
    }
  }
}
