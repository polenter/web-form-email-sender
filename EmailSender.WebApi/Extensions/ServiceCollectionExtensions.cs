using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EmailSender.WebApi.Email;
using EmailSender.WebApi.Options;
using EmailSender.WebApi.Services;
using EmailSender.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;

namespace EmailSender.WebApi.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services,
            Action<List<FormClientMetadata>> configureFormClients,
            Action<SmtpEmailSenderOptions> configureSmtpEmailOptions,
            Action<CorsPolicyBuilder> corsPolicyBuilder)
        {
#if DEBUG
            services.TryAddScoped<IEmailSender, Mock.MockEmailSender>();
#else
            services.TryAddScoped<IEmailSender, SmtpEmailSender>();
#endif
            services.TryAddScoped<IFormClientRepository, FormClientRepository>();
            services.TryAddSingleton<ISystemClock, DefaultSystemClock>();

            services.Configure<SmtpEmailSenderOptions>(configureSmtpEmailOptions);
            services.Configure<List<FormClientMetadata>>(configureFormClients);

            services.AddHealthChecks();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(corsPolicyBuilder);
            });

            return services;
        }

        public static IServiceCollection AddEmailSenderLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();

            return services;
        }
    }

}