namespace API.DTOs.Auth;

public sealed record SessionEventDto(
    Guid Id,
    string EventType,
    DateTimeOffset EventAt,
    Guid? ActorUserId,
    string? Details
);
