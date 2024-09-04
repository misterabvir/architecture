using FluentValidation.TestHelper;
using RobotCloudService.Authentications.Application.Users.Queries;

namespace RobotCloudService.Authentication.Tests.Validators;

public class RepeatSendCodeValidatorTests
{
    private readonly RepeatSendCode.Validator _validator = new();


    [Fact]
    public void WhenEmailIsEmpty_ShouldHaveError()
    {
        // Arrange
        var model = new RepeatSendCode.Query(string.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void WhenEmailNotValid_ShouldHaveError()
    {
        // Arrange
        var model = new RepeatSendCode.Query("not-valid-email");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }


    [Fact]
    public void WhenSucces_ShouldNotHaveError()
    {
        // Arrange
        var model = new RepeatSendCode.Query("email@address.com");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }
}
