namespace University.Models;

public class CustomRegisterRequest
{
    /// <summary>
    /// The user's email address which acts as a user name.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public required string Password { get; init; }
    
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Name { get; init; }
}