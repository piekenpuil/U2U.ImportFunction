using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportrFunctions.Services
{
  class ImportrDelegatingHandler : DelegatingHandler
  {
    TokenService tokenService;
    IConfiguration config;
    public ImportrDelegatingHandler(TokenService tokenService,IConfiguration config)
    {
      this.tokenService = tokenService;
      this.config = config;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var token = await tokenService.GetTokenAsync();

      request.Headers.Authorization = new("Bearer", token);
      request.Headers.Add("DbEnvironment", config["DatabaseType"]);//test or live

      var response = await base.SendAsync(request, cancellationToken);
      return response;
    }
  }
}
