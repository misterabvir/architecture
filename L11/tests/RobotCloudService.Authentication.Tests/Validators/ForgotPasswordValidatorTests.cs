using FluentValidation.TestHelper;
using RobotCloudService.Authentications.Application.Users.Queries;

namespace RobotCloudService.Authentication.Tests.Validators;

public class ForgotPasswordValidatorTests
{
    private readonly ForgotPassword.Validator _validator = new();


    [Fact]
    public void WhenEmailIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new ForgotPassword.Query(string.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenEmailNotValid_ShouldHaveError()
    {
        // Arrange
        var model = new ForgotPassword.Query("not-valid-email");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenSucces_ShouldNotHaveError()
    {
        // Arrange
        var model = new ForgotPassword.Query("email@address.com");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }
}
