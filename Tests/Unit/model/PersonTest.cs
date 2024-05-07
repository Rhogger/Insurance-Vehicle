using Backend.Enums;
using Backend.Models;
using Moq;

namespace Tests.Unit.Models;

public class PersonTest
{
    [Theory]
    [InlineData(22, Gender.male, 8, Education.high_school, Income.middle_class, 2018, VehicleType.sedan, 8000.8)]
    [InlineData(17, Gender.female, 11, Education.university, Income.upper_class, 2024, VehicleType.sedan, 12376)]
    [InlineData(38, Gender.male, 29, Education.none, Income.working_class, 2006, VehicleType.sports_car, 36186.0)]
    [InlineData(38, Gender.female, 29, Education.university, Income.poverty, 2015, VehicleType.sports_car, 998.123)]
    public void ConstructorAcceptsValidValues(int age, Gender gender, int drivingExperience, Education education, Income income, int vehicleYear, VehicleType vehicleType, float annualMileage)
    {
        // Act
        var mockPerson = new Mock<Person>(age, gender, drivingExperience, education, income, vehicleYear, vehicleType, annualMileage);
        var person = mockPerson.Object;

        // Assert
        Assert.Equal(age, person.age);
        Assert.Equal(gender, person.gender);
        Assert.Equal(drivingExperience, person.drivingExperience);
        Assert.Equal(education, person.education);
        Assert.Equal(income, person.income);
        Assert.Equal(vehicleYear, person.vehicleYear);
        Assert.Equal(vehicleType, person.vehicleType);
        Assert.Equal(annualMileage, person.annualMileage);
    }

    [Theory]
    [InlineData(-5)]
    public void ThrowsExceptionForNegativeAge(int age)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(age, Gender.male, 5, Education.university, Income.poverty, 2010, VehicleType.sedan, 10000));
    }

    [Theory]
    [InlineData((Gender)3)]
    public void ThrowsExceptionForInvalidGender(Gender gender)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, gender, 5, Education.university, Income.poverty, 2010, VehicleType.sedan, 2450));
    }

    [Theory]
    [InlineData(-2)]
    public void ThrowsExceptionForNegativeDrivingExperience(int drivingExperience)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, Gender.female, drivingExperience, Education.high_school, Income.upper_class, 2010, VehicleType.sedan, 1000));
    }

    [Theory]
    [InlineData((Education)4)]
    public void ThrowsExceptionForInvalidEducation(Education education)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, Gender.female, 5, education, Income.poverty, 2010, VehicleType.sedan, 2450));
    }

    [Theory]
    [InlineData((Income)5)]
    public void ThrowsExceptionForInvalidIncome(Income income)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, Gender.female, 5, Education.none, income, 2010, VehicleType.sedan, 10000));
    }

    [Theory]
    [InlineData(-8)]
    public void ThrowsExceptionForNegativeVehicleYear(int vehicleYear)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, Gender.female, 5, Education.none, Income.poverty, vehicleYear, VehicleType.sedan, 3000));
    }

    [Theory]
    [InlineData((VehicleType)8)]
    public void ThrowsExceptionForInvalidVehicleType(VehicleType vehicleType)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, Gender.female, 5, Education.university, Income.upper_class, 2010, vehicleType, 10000));
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(-1)]
    [InlineData(-0.9)]
    public void ThrowsExceptionForNegativeAnnualMileage(float annualMileage)
    {
        // Assert
        Assert.Throws<Exception>(() => new Person(25, Gender.male, 5, Education.university, Income.poverty, 2010, VehicleType.sedan, annualMileage));
    }

    [Theory]
    [InlineData(20, "16-25")]
    [InlineData(30, "26-39")]
    [InlineData(50, "40-64")]
    [InlineData(70, "65+")]
    public void SetAgeIntervalReturnsCorrectInterval(int age, string expectedInterval)
    {
        // Arrange
        var mockPerson = new Mock<Person>(age, Gender.male, 5, Education.high_school, Income.middle_class, 2010, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.SetAgeInterval();

        // Assert
        Assert.Equal(expectedInterval, result);
    }

    [Theory]
    [InlineData(10)]
    public void ThrowsExceptionForSetInvalidAge(int age)
    {
        // Arrange
        var mockPerson = new Mock<Person>(age, Gender.male, 5, Education.high_school, Income.middle_class, 2010, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        // Act & Assert
        Assert.Throws<Exception>(person.SetAgeInterval);
    }

    [Theory]
    [InlineData(Gender.male, "male")]
    [InlineData(Gender.female, "female")]
    public void ConvertGenderEnumReturnsCorrectClass(Gender gender, string expectedClass)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, gender, 12, Education.high_school, Income.middle_class, 2010, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.ConvertGenderEnum();

        // Assert
        Assert.Equal(expectedClass, result);
    }

    [Theory]
    [InlineData(7, "0-9y")]
    [InlineData(18, "10-19y")]
    [InlineData(29, "20-29y")]
    [InlineData(31, "30y+")]
    public void SetDrivingExperienceIntervalReturnsCorrectInterval(int drivingExperience, string expectedInterval)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, Gender.male, drivingExperience, Education.high_school, Income.middle_class, 2010, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.SetDrivingExperienceInterval();

        // Assert
        Assert.Equal(expectedInterval, result);
    }

    [Theory]
    [InlineData(Education.none, "none")]
    [InlineData(Education.university, "university")]
    [InlineData(Education.high_school, "high school")]
    public void ConvertEducationEnumReturnsCorrectClass(Education education, string expectedClass)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, Gender.male, 12, education, Income.middle_class, 2010, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.ConvertEducationEnum();

        // Assert
        Assert.Equal(expectedClass, result);
    }

    [Theory]
    [InlineData(Income.upper_class, "upper class")]
    [InlineData(Income.middle_class, "middle class")]
    [InlineData(Income.working_class, "working class")]
    [InlineData(Income.poverty, "poverty")]
    public void ConvertIncomeEnumReturnsCorrectClass(Income income, string expectedClass)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, Gender.male, 12, Education.high_school, income, 2010, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.ConvertIncomeEnum();

        // Assert
        Assert.Equal(expectedClass, result);
    }

    [Theory]
    [InlineData(2006, "before 2015")]
    [InlineData(2014, "before 2015")]
    [InlineData(2015, "after 2015")]
    [InlineData(2020, "after 2015")]
    [InlineData(2024, "after 2015")]
    public void SetVehicleYearToBeforeOrAfter(int vehicleYear, string expectedResult)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, Gender.male, 10, Education.high_school, Income.middle_class, vehicleYear, VehicleType.sedan, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.SetVehicleYearToBeforeOrAfter();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(VehicleType.sedan, "sedan")]
    [InlineData(VehicleType.sports_car, "sports car")]
    public void ConvertVehicleTypeEnumReturnsCorrectClass(VehicleType vehicleType, string expectedClass)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, Gender.male, 12, Education.high_school, Income.middle_class, 2010, vehicleType, 10000);
        var person = mockPerson.Object;

        //Act
        var result = person.ConvertVehicleTypeEnum();

        // Assert
        Assert.Equal(expectedClass, result);
    }

    [Theory]
    [InlineData(500, "0.0")]
    [InlineData(500.5, "1000.0")]
    [InlineData(500.6, "1000.0")]
    [InlineData(501, "1000.0")]
    [InlineData(998, "1000.0")]
    [InlineData(999.9, "1000.0")]
    [InlineData(1000, "1000.0")]
    [InlineData(1234, "1000.0")]
    [InlineData(1589, "2000.0")]
    [InlineData(12467, "12000.0")]
    [InlineData(124678, "125000.0")]
    public void RoundAnnualMileageReturnsCorrectRoundedNumber(float annualMileage, string expectedNumber)
    {
        // Arrange
        var mockPerson = new Mock<Person>(26, Gender.male, 12, Education.high_school, Income.middle_class, 2010, VehicleType.sedan, annualMileage);
        var person = mockPerson.Object;

        //Act
        var result = person.RoundAnnualMileage();

        // Assert
        Assert.Equal(expectedNumber, result);
    }
}