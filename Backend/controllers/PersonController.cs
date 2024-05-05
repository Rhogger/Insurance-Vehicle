using System.Globalization;
using Backend.Enums;
using Backend.Interfaces;
using Backend.Models;
using CsvHelper;
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
    private const string CsvCacheKey = "CsvData";
    public PersonController(ILogger<PersonController> logger, IDataLoad dataLoad, IMemoryCache cache)
    {
        _logger = logger;
        _dataLoad = dataLoad;
        _cache = cache;
    }

    [HttpGet(Name = "GetCsvData")]
    public async Task<IEnumerable<string>> GetCsvData()
    {
        // Tente recuperar o CSV do cache
        string csvContent;
        if (!_cache.TryGetValue(CsvCacheKey, out csvContent))
        {
            // Se o CSV não estiver em cache, faça a solicitação para a URL e obtenha o conteúdo do CSV
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync("https://insuranceapistorage.blob.core.windows.net/csv/Car_Insurance_Claim.csv?sv=2022-11-02&ss=b&srt=sco&sp=rlitf&se=2024-06-06T04:48:06Z&st=2024-05-05T20:48:06Z&spr=https&sig=W1%2FAoUXGf6XuVAp3J2q7gRNix0hGDEo0y1%2F%2FHdZLV%2BE%3D");
                    if (response.IsSuccessStatusCode)
                    {
                        csvContent = await response.Content.ReadAsStringAsync();

                        // Armazene o CSV no cache com um tempo de vida (TTL)
                        _cache.Set(CsvCacheKey, csvContent, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(20) // Expira em 20 minutos
                        });
                    }
                    else
                    {
                        // Se não foi possível obter o conteúdo do CSV, lance uma exceção ou retorne uma coleção vazia, dependendo do seu requisito
                        throw new Exception("Falha ao obter o conteúdo do CSV.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao acessar a URL do CSV.");
                    // Se houve um erro ao acessar a URL do CSV, lance uma exceção ou retorne uma coleção vazia, dependendo do seu requisito
                    throw;
                }
            }
        }

        // Separe o conteúdo do CSV em linhas e retorne como uma coleção de strings
        var csvLines = csvContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return csvLines;
    }


    [HttpGet(Name = "Buscar o Score")]
    public async Task<IActionResult> GetScore(int age, Gender gender, int drivingExperience, Education education, Income income, int vehicleYear, VehicleType vehicleType, float annualMileage)
    {
        // Busca o conteúdo do CSV
        IEnumerable<string> csvData = await GetCsvData();

        // Inicialize uma lista para armazenar os dados do CSV
        List<PersonBase> dataset = new List<PersonBase>();

        // Use o CsvReader para processar o conteúdo do CSV
        using (var reader = new StringReader(string.Join(Environment.NewLine, csvData)))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Leia os registros do CSV e adicione-os à lista dataset
            dataset = csv.GetRecords<PersonBase>().ToList();
        }

        Person person = new Person(age, gender, drivingExperience, education, income, vehicleYear, vehicleType, annualMileage);

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
