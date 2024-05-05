using Backend.Interfaces;
using Backend.Models;

public abstract class DataLoadBase : IDataLoad
{
  public List<PersonBase> GetDataset(IEnumerable<string> csvData)
  {
    return Load<PersonBase>(csvData);
  }
  public abstract List<T> Load<T>(IEnumerable<string> csvData);
}