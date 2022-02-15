using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EmailSender.WebApi.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEmailSenderLocalization(this IApplicationBuilder app)
        {

            var supportedCultures = new[] { "en-US", "de-DE", "pl-PL" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures.First())
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            return app;
        }

        public static void UseEmailSenderHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/hello", new HealthCheckOptions
            {
                ResponseWriter = (ctx, rep) =>
                {
                    var version = Assembly.GetExecutingAssembly()
                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
                    return Task.FromResult(ctx.Response.WriteAsync($"Web Form Email Sender v.{version}"));
                }
            });

        }
    }
}