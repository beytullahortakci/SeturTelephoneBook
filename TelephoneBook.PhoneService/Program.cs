using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Services;
using TelephoneBook.PhoneService.Data;
using TelephoneBook.PhoneService.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.ConfigureServices();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();