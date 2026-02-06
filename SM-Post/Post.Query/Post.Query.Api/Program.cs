using Microsoft.AspNetCore.Mvc;

// Simple DTO describing a user activity event
var builder = WebApplication.CreateBuilder(args);

// Swagger for quick testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health check
app.MapGet("/health", () => Results.Ok("OK"));

// First feature: receive an activity
app.MapPost("/api/activity", ([FromBody] UserActivityDto activity) =>
{
    // For now just log to console (later we publish to Kafka)
    Console.WriteLine(
        $"[API] Activity from {activity.UserId}: {activity.ActivityType} at {activity.TimestampUtc:o} - {activity.Description}"
    );

    // In event-driven systems, 202 Accepted is nice
    return Results.Accepted();
});

app.Run();

public record UserActivityDto(
    string UserId,
    string ActivityType,          // e.g. "WORKOUT_LOGGED", "TASK_COMPLETED"
    string? Description,
    DateTime TimestampUtc
);