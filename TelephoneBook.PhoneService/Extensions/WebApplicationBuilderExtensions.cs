using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Services;
using TelephoneBook.Infrastructure;

namespace TelephoneBook.PhoneService.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IContactService,ContactService>();
            builder.Services.AddInfrastructure(builder.Configuration);
        }
    }
}
