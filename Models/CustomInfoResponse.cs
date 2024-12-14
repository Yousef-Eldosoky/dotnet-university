namespace University.Models;

public class CustomInfoResponse
{
    /// <summary>
    /// The email address associated with the authenticated user.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Indicates whether or not the <see cref="Email"/> has been confirmed yet.
    /// </summary>
    public required bool IsEmailConfirmed { get; init; }
    
    public required Student Student { get; init; }
}