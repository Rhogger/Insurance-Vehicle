using Backend.Enums;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IDataLoad _dataLoad;
    public PersonController(ILogger<PersonController> logger, IDataLoad dataLoad)
    {
        _logger = logger;
        _dataLoad = dataLoad;
    }

    [HttpGet(Name = "Buscar o Score")]
    public IActionResult GetScore(int age, Gender gender, int drivingExperience, Education education, Income income, int vehicleYear, VehicleType vehicleType, float annualMileage)
    {
        List<PersonBase> dataset = _dataLoad.GetDataset();

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
