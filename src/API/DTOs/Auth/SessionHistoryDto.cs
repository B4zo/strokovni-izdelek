namespace API.DTOs.Auth;

public sealed record SessionHistoryDto(
    Guid SessionId,
    string SessionKey,
    Guid UserId,
    string Username,
    string? DeviceName,
    string? IpAddress,
    DateTimeOffset CreatedAt,
    DateTimeOffset ExpiresAt,
    IReadOnlyList<SessionEventDto> Events
);
