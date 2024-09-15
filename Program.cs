using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


var descriptions = new[]
{
    "Pet Cat", "Buy Milk", "Eat Chilli", "Make Ice Cream", "Sell scope", "Blarg", "Code", "Fix dishwasher"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/todos", () =>
{
    var all_todos = Enumerable.Range(1, 50).Select(index =>
            new Todo
            {
                due = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                id = Random.Shared.Next(-20, 55),
                description = descriptions[Random.Shared.Next(descriptions.Length)]
            })
        .ToArray();
    return all_todos;
});

app.MapPost("/todos/{description}", (
    [FromRoute] string description) =>
{
    var range = Enumerable.Range(1, 7);
    int days = range.TakeFirstRandom();
    var todo = new Todo
    {
        due = DateOnly.FromDateTime(DateTime.Now.AddDays(days)),
        id = Random.Shared.Next(-20, 55),
        description = description //descriptions[Random.Shared.Next(descriptions.Length)]
    };
    return todo;
});

// Upload a file with todos
app.MapPost("/todos/upload", (IFormFile file) =>
{
    using var reader = new StreamReader(file.OpenReadStream());

    while (reader.Peek() >= 0)
    {
        var line = (reader.ReadLine() ?? string.Empty);
        // todo: process line
        Console.WriteLine(line);
    }
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}