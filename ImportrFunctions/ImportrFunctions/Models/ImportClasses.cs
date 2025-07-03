using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.External.Models
{
  public class ImportingCourseProviderLocationImportingBundle
  {
    public bool dryRun { get; set; }
    public bool detailedResults { get; set; }
    public ImportingCourseProviderLocation[] entities { get; set; }
  }

  public class ImportingCourseProviderLocation
  {
    public string locationId { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string postalCode { get; set; }
    public string countryIsoCode { get; set; }
    public string bobeLearningLocationId { get; set; }
    public string title { get; set; }
    public int gpsLong { get; set; }
    public int gpsLat { get; set; }
    public string courseProviderId { get; set; }
  }
}
