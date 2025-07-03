using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2U.External.Models
{
  public class ImportingCourseProviderTrainerImportingBundle
  {
    public bool dryRun { get; set; }
    public bool detailedResults { get; set; }
    public ImportingCourseProviderTrainer[] entities { get; set; }
  }

  public class ImportingCourseProviderTrainer
  {
    public string courseProviderTrainerId { get; set; }
    public string name { get; set; }
    public string prename { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string description { get; set; }
    public string courseProviderId { get; set; }
  }


  //for trainers, locations,courses...
  public class ImportingEntityImportingEntityResult
  {
    public ImportingEntityFailedCreateEntity[] failedEntities { get; set; }
    public ImportingEntity[] addedEntities { get; set; }
    public ImportingEntityDuplicateEntities duplicateEntities { get; set; }
    public ImportingEntity[] updatedEntities { get; set; }
    public int countFailed { get; set; }
    public int countAdded { get; set; }
    public int countUpdated { get; set; }
    public int countDuplicate { get; set; }
    public string persistenceErrorMessage { get; set; }
    public string persistenceErrorStacktrace { get; set; }
    public bool dryRun { get; set; }
  }

  public class ImportingEntityDuplicateEntities
  {
    public KeyProperty[] keyProperties { get; set; }
    public ImportingEntity[] entities { get; set; }
  }

  public class KeyProperty
  {
    public Additionalprop1 additionalProp1 { get; set; }
    public Additionalprop2 additionalProp2 { get; set; }
    public Additionalprop3 additionalProp3 { get; set; }
  }

  public class Additionalprop1
  {
  }

  public class Additionalprop2
  {
  }

  public class Additionalprop3
  {
  }

  public class ImportingEntityFailedCreateEntity
  {
    public ImportingEntity entity { get; set; }
    public ModelFieldError[] errors { get; set; }
  }

  public class ImportingEntity
  {
    public string courseProviderId { get; set; }
  }

  public class ModelFieldError
  {
    public string errMessage { get; set; }
    public string fieldName { get; set; }
  }
}
