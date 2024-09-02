using System.ComponentModel.DataAnnotations;

namespace RobotCloudService.Web.Components.Authentications.Models;

public class LoginAccountModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}
