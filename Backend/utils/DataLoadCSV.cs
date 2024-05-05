using Backend.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Backend.Utils.DataLoad;
public class DataLoadCSV : DataLoadBase
{
  public override List<PersonBase> Load<PersonBase>(IEnumerable<string> csvData)
  {
    var config = new CsvConfiguration(CultureInfo.InvariantCulture);
    using (var reader = new StringReader(string.Join(Environment.NewLine, csvData)))
    using (var csv = new CsvReader(reader, config))
    {
      return csv.GetRecords<PersonBase>().ToList();
    }
  }
}