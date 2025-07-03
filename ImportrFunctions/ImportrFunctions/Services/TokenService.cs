using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace U2U.External.Services
{
  public class TokenService
  {
    IConfiguration config;
    HttpClient httpClient;
    static string accessToken;
    static DateTime obtainedAt;

    public TokenService(HttpClient httpClient, IConfiguration config)
    {
      this.httpClient = httpClient;
      this.config = config;
    }

    public async Task<string> GetTokenAsync()
    {
      if (accessToken == null || obtainedAt < DateTime.Now.AddHours(-2))
      {
        ClientCredentials credentials = new ClientCredentials(config["Credentials:ClientId"],
          config["Credentials:ClientSecret"]);
        var scerd = JsonSerializer.Serialize(credentials);
        StringContent body = new StringContent(scerd, Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Add("accept", "*/*");
        body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        var resp = await httpClient.PostAsync(config["TokenUrl"], body);
        var s = await resp.Content.ReadAsStringAsync();
        var tr = JsonSerializer.Deserialize<TokenResponse>(s);
        accessToken = tr!.access_token;
        obtainedAt = DateTime.Now;
      }
      return accessToken;

    }
  }

  public record ClientCredentials(string client_id, string client_secret);

  public record TokenResponse
  {
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
  }
}
