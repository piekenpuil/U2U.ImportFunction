using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.External.Models
{
  public record BOBECourseImport
  {
    public bool dryRun { get; set; }
    public bool detailedResults { get; set; }
    public IList<BOBECourse> entities { get; set; }
  }

  public record BOBECourse
  {
    public string providerMasterCourseId { get; set; }
    public string providerCourseSessionId { get; set; }
    public string providerCourseCategoryId { get; set; }
    public string applicationStatusId { get; set; }
    public string durationUnitId { get; set; }
    public string locationId { get; set; }
    public string levelId { get; set; }
    public string trainerId { get; set; }
    public string instructionMethodId { get; set; }
    public string languageId { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int duration { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public int priceExVAT { get; set; }
    public int priceGroupExVAT { get; set; }
    public string targetGroups { get; set; }
    public string goals { get; set; }
    public string prerequisites { get; set; }
    public int maxParticipants { get; set; }
    public bool certificate { get; set; }
    public string urlToCourseDetails { get; set; }
    public string courseProviderId { get; set; }
  }
}
