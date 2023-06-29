using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTO.Options;

namespace apollo.Presentation.Options
{
    public static class OptionsStartupExtensions
    {
        public static void ConfigureAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfig>(option => configuration.Bind(nameof(EmailConfig), option));
        }
    }
}