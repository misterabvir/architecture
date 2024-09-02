using System.ComponentModel.DataAnnotations;

namespace RobotCloudService.Web.Components.Authentications.Models;

public class RegisterAccountModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string Password2 { get; set; } = string.Empty;

}
public class ResetAccountModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string Password2 { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;
}

public class ConfirmAccountModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;
}

public class ForgotAccountModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}