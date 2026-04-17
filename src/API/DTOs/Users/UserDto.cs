namespace API.DTOs.Users;

public sealed record UserDto(
    Guid Id,
    string Username,
    string DisplayName,
    string Role,
    bool IsActivated,
    DateTimeOffset CreatedAt,
    DateTimeOffset? LastLoginAt
);
