namespace RobotCloudService.Authentications.Presentation.Contracts;


public static class Requests
{
    public record Login(string Email, string Password)
    {
        public const string Route = "login";
    }

    public record Register(string Email, string Password)
    {
        public const string Route = "register";
    }

    public record Confirm(string Email, string Code)
    {
        public const string Route = "confirm";
    }
    public record ResetPassword(string Email, string NewPassword, string Code)
    {
        public const string Route = "reset-password";
    }

    public record ForgotPassword(string Email)
    {
        public const string Route = "forgot-password";
    }

    public record RepeatSendCode(string Email)
    {
        public const string Route = "repeat-send-code";
    }
}

