using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Services;
using TelephoneBook.Domain.Common;
using TelephoneBook.Infrastructure.Consumers;
using TelephoneBook.ReportService;
using TelephoneBook.ReportService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.ConfigureServices();
builder.Services.AddHostedService<ReportWorker>();
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
