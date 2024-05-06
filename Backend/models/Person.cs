using System.ComponentModel.DataAnnotations;
using Backend.Enums;

namespace Backend.Models;

public class Person
{
  [Required(ErrorMessage = "Age is required")]
  public int age { get; set; }
  [Required(ErrorMessage = "Gender is required")]
  public Gender gender { get; set; }
  [Required(ErrorMessage = "Driving experience is required")]
  public int drivingExperience { get; set; }
  [Required(ErrorMessage = "Education is required")]
  public Education education { get; set; }
  [Required(ErrorMessage = "Income is required")]
  public Income income { get; set; }
  [Required(ErrorMessage = "Vehicle year is required")]
  public int vehicleYear { get; set; }
  [Required(ErrorMessage = "Vehicle type is required")]
  public VehicleType vehicleType { get; set; }
  [Required(ErrorMessage = "Annual mileage is required")]
  public float annualMileage { get; set; }

  public Person(int age, Gender gender, int drivingExperience, Education education, Income income, int vehicleYear, VehicleType vehicleType, float annualMileage)
  {
    if (age < 0) throw new Exception("Invalid age.");
    this.age = age;

    if (gender == null) throw new ArgumentNullException(nameof(gender), "Gender can't be null.");
    if (gender != Gender.female && gender != Gender.male) throw new Exception("Invalid gender.");
    this.gender = gender;

    if (drivingExperience < 0) throw new Exception("Invalid driving experience.");
    this.drivingExperience = drivingExperience;

    if (education == null) throw new ArgumentNullException(nameof(education), "Education can't be null.");
    if (education != Education.none && education != Education.university && education != Education.high_school) throw new Exception("Invalid education type.");
    this.education = education;

    if (income == null) throw new ArgumentNullException(nameof(income), "Income can't be null.");
    if (income != Income.poverty && income != Income.working_class && income != Income.upper_class && income != Income.middle_class) throw new Exception("Invalid income type.");
    this.income = income;

    if (vehicleYear < 0) throw new Exception("Invalid year to vehicle.");
    this.vehicleYear = vehicleYear;

    if (vehicleType == null) throw new ArgumentNullException(nameof(vehicleType), "Vehicle type can't be null.");
    if (vehicleType != VehicleType.sedan && vehicleType != VehicleType.sports_car) throw new Exception("Invalid vehicle type.");
    this.vehicleType = vehicleType;

    if (annualMileage < 0) throw new Exception("Invalid annual mileage.");
    this.annualMileage = annualMileage;
  }

  public string SetAgeInterval()
  {
    if (age < 17) throw new Exception("Age younger than expected.");
    if (age < 26) return "16-25";
    if (age < 40) return "26-39";
    if (age < 65) return "40-64";
    return "65+";
  }

  public string ConvertGenderEnum()
  {
    if (Gender.male == gender) return "male";
    return "female";
  }

  public string SetDrivingExperienceInterval()
  {
    if (drivingExperience < 10) return "0-9y";
    if (drivingExperience < 20) return "10-19y";
    if (drivingExperience < 30) return "20-29y";
    return "30y+";
  }

  public string ConvertEducationEnum()
  {
    if (education == Education.high_school) return "high school";
    if (education == Education.university) return "university";
    return "none";
  }

  public string ConvertIncomeEnum()
  {
    if (income == Income.middle_class) return "middle class";
    if (income == Income.poverty) return "poverty";
    if (income == Income.upper_class) return "upper class";
    return "working class";
  }

  public string SetVehicleYearToBeforeOrAfter()
  {
    if (vehicleYear > 2024) throw new Exception("Vehicle year greater than the current calendar year.");
    if (vehicleYear < 2015) return "before 2015";
    return "after 2015";
  }

  public string ConvertVehicleTypeEnum()
  {
    if (vehicleType == VehicleType.sedan) return "sedan";
    return "sports car";
  }

  public string RoundAnnualMileage()
  {
    var quilometers = annualMileage;

    var roundedDecimals = Math.Round(quilometers, 1);

    var roundedThousands = Math.Round(roundedDecimals / 1000) * 1000;

    return $"{roundedThousands:#.0}";
  }
}
