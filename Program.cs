using UrlShortener.Services;
using UrlShortener.Endpoints;
using UrlShortner.Data;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UrlShortenerDbContext>(options => options.UseInMemoryDatabase("UrlShortenerDb"));
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapUrlShortenerEndpoints();

app.Run();


//for detect integration tests | WebApplicationFactory
public partial class Program { }