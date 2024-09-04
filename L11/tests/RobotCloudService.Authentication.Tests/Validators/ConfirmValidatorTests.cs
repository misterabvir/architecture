using FluentValidation;
using FluentValidation.TestHelper;

using RobotCloudService.Authentications.Application.Users.Commands;

namespace RobotCloudService.Authentication.Tests.Validators;

public class ConfirmValidatorTests
{
    private readonly Confirm.Validator _validator = new();


    [Fact]
    public void WhenEmailIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new Confirm.Command(string.Empty, "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenEmailNotValid_ShouldHaveError()
    {
        // Arrange
        var model = new Confirm.Command("not-valid-email", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenCodeEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new Confirm.Command("email@address.com", string.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Code);
    }

    [Fact]
    public void WhenSucces_ShouldNotHaveError()
    {
        // Arrange
        var model = new Confirm.Command("email@address.com", "verification-code");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Code);
    }
}
