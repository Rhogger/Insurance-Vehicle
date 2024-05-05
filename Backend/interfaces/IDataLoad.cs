using Backend.Models;

namespace Backend.Interfaces;

public interface IDataLoad
{
  List<PersonBase> GetDataset(IEnumerable<string> csvData);

  List<PersonBase> Load<PersonBase>(IEnumerable<string> csvData);
}