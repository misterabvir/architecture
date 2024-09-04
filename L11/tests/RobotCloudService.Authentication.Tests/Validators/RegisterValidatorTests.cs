using System.Configuration;

using FluentValidation;
using FluentValidation.TestHelper;

using RobotCloudService.Authentications.Application.Users.Commands;
using RobotCloudService.Authentications.Application.Users.Queries;

namespace RobotCloudService.Authentication.Tests.Validators;

public class RegisterValidatorTests
{
    private readonly Register.Validator _validator = new();


    [Fact]
    public void WhenEmailIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command(string.Empty, "Pa$$w0rd");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenEmailNotValid_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("not-valid-email", "Pa$$w0rd");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenPasswordIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", string.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void WhenPasswordIsShorterThen8_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", "pass");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void WhenPasswordNotHasDigits_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", "Pa$$word");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
    [Fact]
    public void WhenPasswordNotHasUpperLetter_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", "pa$$w0rd");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void WhenPasswordNotHasLowerLetter_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", "PA$$W0RD");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void WhenPasswordNotHasSpecial_ShouldHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", "Passw0rd");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void WhenSucces_ShouldNotHaveError()
    {
        // Arrange
        var model = new Register.Command("email@address.com", "Pa$$w0rd");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

}
