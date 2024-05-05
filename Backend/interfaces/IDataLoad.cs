using Backend.Models;

namespace Backend.Interfaces;

public interface IDataLoad
{
  List<PersonBase> GetDataset();

  List<T> Load<T>(string local);
}