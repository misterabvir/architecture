namespace CloudService.Application.Exceptions;

public class UsernameAlreadyExistsException(string username) : Exception($"User '{username}' already exists") { }
