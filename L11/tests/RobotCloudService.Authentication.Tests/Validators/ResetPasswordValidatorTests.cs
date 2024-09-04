using System.Configuration;

using FluentValidation;
using FluentValidation.TestHelper;

using RobotCloudService.Authentications.Application.Users.Commands;
using RobotCloudService.Authentications.Application.Users.Queries;

namespace RobotCloudService.Authentication.Tests.Validators;

public class ResetPasswordValidatorTests
{
    private readonly ResetPassword.Validator _validator = new();


    [Fact]
    public void WhenEmailIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command(string.Empty, "Pa$$w0rd", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenEmailNotValid_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("not-valid-email", "Pa$$w0rd", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenPasswordIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", string.Empty, "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPassword);
    }

    [Fact]
    public void WhenPasswordIsShorterThen8_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "pass", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPassword);
    }

    [Fact]
    public void WhenPasswordNotHasDigits_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "Pa$$word", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPassword);
    }
    [Fact]
    public void WhenPasswordNotHasUpperLetter_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "pa$$w0rd", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPassword);
    }

    [Fact]
    public void WhenPasswordNotHasLowerLetter_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "PA$$W0RD", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPassword);
    }

    [Fact]
    public void WhenPasswordNotHasSpecial_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "Passw0rd", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPassword);
    }

    [Fact]
    public void WhenCodeEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "Pa$$w0rd", string.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Code);
    }


    [Fact]
    public void WhenSucces_ShouldNotHaveError()
    {
        // Arrange
        var model = new ResetPassword.Command("email@address.com", "Pa$$w0rd", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Code);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.NewPassword);
    }

}
