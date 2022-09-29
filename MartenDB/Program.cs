using Marten;
using MartenDB.Models;

var builder = WebApplication.CreateBuilder(args);
const string connectionString = "User ID=marten-db-test;Password=marten0db;Host=localhost;Port=5432;Database=marten-db;";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMarten(option =>
{
    option.Connection(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "Welcome to index page.");
app.MapGet("/api", () => "Welcome to api page.");

app.MapControllers();
app.Run();