using Backend.Interfaces;
using Backend.Models;

public abstract class DataLoadBase : IDataLoad
{
  public List<PersonBase> GetDataset()
  {
    return Load<PersonBase>("Car_Insurance_Claim");
  }
  public abstract List<T> Load<T>(string local);
}