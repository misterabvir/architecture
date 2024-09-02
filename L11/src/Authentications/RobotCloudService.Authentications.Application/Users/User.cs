using RobotCloudService.Authentications.Application.Users.Events;
using RobotCloudService.Authentications.Application.Users.ValueObjects;
using RobotCloudService.Application.Common;

namespace RobotCloudService.Authentications.Application.Users;

public sealed class User : AggregateRoot
{
    public UserId UserId { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
    public Email Email { get; private set; } = default!;

    public bool EmailVerified { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; private set; } = DateTime.UtcNow;

    private User() { } //ef
    private User(UserId userId, Email email, Password password)
    {
        UserId = userId;
        Email = email;
        Password = password;
    }

    public static User Create(Email email, Password password)
    {
        var user = new User(UserId.CreateUnique(), email, password);
        user.AddDomainEvent(new Registered.Notification(user.UserId, user.Email));
        return user;
    }

    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;
        UpdateAt = DateTime.UtcNow;
        AddDomainEvent(new UserPasswordChangedDomainEvent(UserId, Email));
    }

    public void ConfirmEmail()
    {
        EmailVerified = true;
        UpdateAt = DateTime.UtcNow;
        AddDomainEvent(new Confirmed.Notification(UserId, Email));
    }

    protected override IEnumerable<ValueObject> EqualityComponents()
    {
        yield return UserId;    
    }
}
