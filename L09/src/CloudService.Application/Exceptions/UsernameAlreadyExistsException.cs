namespace CloudService.Application.Exceptions;

public class UsernameAlreadyExistsException(string username) : Exception($"User '{username}' already exists") { }
public class InvalidCredentialsException() : Exception($"Invalid credentials") { }
public class NotFoundException(string message) : Exception(message) { }