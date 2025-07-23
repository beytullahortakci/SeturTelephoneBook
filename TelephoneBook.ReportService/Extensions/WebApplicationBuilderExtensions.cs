using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Services;
using TelephoneBook.Infrastructure;

namespace TelephoneBook.ReportService.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IContactService,ContactService>();
            builder.Services.AddScoped<IContactDetailService, ContactDetailService>();
            builder.Services.AddScoped<IReportService, Application.Services.ReportService>();


            builder.Services.AddInfrastructure(builder.Configuration);
        }
    }
}
