namespace RobotCloudService.Authentications.Presentation.Contracts;

public static class Authentications
{
    
    public static class  Requests
    {
        public record Login(string Email, string Password)
        {
            public const string Route = "authentication/login";
        }

        public record Register(string Email, string Password)
        {
            public const string Route = "authentication/register";
        }

        public record Confirm(string Email, string Code)
        {
            public const string Route = "authentication/confirm";
        }
        public record ResetPassword(string Email, string NewPassword, string Code)
        {
            public const string Route = "authentication/reset-password";
        }

        public record ForgotPassword(string Email)
        {
            public const string Route = "authentication/forgot-password";
        }

        public record RepeatSendCode(string Email)
        {
            public const string Route = "authentication/repeat-send-code";
        }
    }
}
