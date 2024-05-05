using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Backend.Utils.DataLoad;
public class DataLoadCSV : DataLoadBase
{
  public override List<T> Load<T>(string local)
  {
    local = local + ".csv";
    if (!File.Exists(local))
      throw new ArgumentException(local);

    var config = new CsvConfiguration(CultureInfo.InvariantCulture);
    using (var reader = new StreamReader(local))
    using (var csv = new CsvReader(reader, config))
    {
      return csv.GetRecords<T>().ToList();
    }
  }
}