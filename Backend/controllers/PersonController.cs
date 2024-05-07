using Backend.Enums;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IDataLoad _dataLoad;
    private readonly IMemoryCache _cache;
    private const string csvCacheKey = "CsvData";
    public PersonController(ILogger<PersonController> logger, IDataLoad dataLoad, IMemoryCache cache)
    {
        _logger = logger;
        _dataLoad = dataLoad;
        _cache = cache;
    }

    [HttpGet(Name = "GetCsvData")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IEnumerable<string>> GetCsvData()
    {
        string csvContent;

        if (!_cache.TryGetValue(csvCacheKey, out csvContent))
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync("https://insuranceapistorage.blob.core.windows.net/csv/Car_Insurance_Claim.csv?sv=2022-11-02&ss=b&srt=sco&sp=rlitf&se=2024-06-06T04:48:06Z&st=2024-05-05T20:48:06Z&spr=https&sig=W1%2FAoUXGf6XuVAp3J2q7gRNix0hGDEo0y1%2F%2FHdZLV%2BE%3D");
                    if (response.IsSuccessStatusCode)
                    {
                        csvContent = await response.Content.ReadAsStringAsync();
    
                        _cache.Set(csvCacheKey, csvContent, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(20)
                        });
                    }
                    else
                    {
                        throw new Exception("Failed to get CSV content.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error accessing CSV URL.");
                    throw;
                }
            }
        }

        var csvLines = csvContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return csvLines;
    }

    [HttpGet(Name = "Buscar o Score")]
    public async Task<IActionResult> GetScore(int age, Gender gender, int drivingExperience, Education education, Income income, int vehicleYear, VehicleType vehicleType, float annualMileage)
    {
        Person person = new Person(age, gender, drivingExperience, education, income, vehicleYear, vehicleType, annualMileage);

        IEnumerable<string> csvData = await GetCsvData();

        List<PersonBase> dataset = _dataLoad.GetDataset(csvData);

        var _age = person.SetAgeInterval();
        var _gender = person.ConvertGenderEnum();
        var _drivingExperience = person.SetDrivingExperienceInterval();
        var _education = person.ConvertEducationEnum();
        var _income = person.ConvertIncomeEnum();
        var _vehicleYear = person.SetVehicleYearToBeforeOrAfter();
        var _vehicleType = person.ConvertVehicleTypeEnum();
        var _annualMileage = person.RoundAnnualMileage();

        var dataFiltered = dataset.Where(x => x.AGE == _age && x.GENDER == _gender && x.DRIVING_EXPERIENCE == _drivingExperience && x.EDUCATION == _education && x.INCOME == _income && x.VEHICLE_YEAR == _vehicleYear && x.VEHICLE_TYPE == _vehicleType && x.ANNUAL_MILEAGE == _annualMileage).ToList();

        if (!dataFiltered.Any())
        {
            return NotFound("No person with this data was found.");
        }

        var creditScores = dataFiltered.Select(x => new { id = x.ID, creditScore = x.CREDIT_SCORE }).ToList();

        if (!creditScores.Any())
        {
            return NotFound("This person does not have a credit score.");
        }

        return Ok(creditScores);
    }

}
