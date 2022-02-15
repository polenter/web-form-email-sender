using EmailSender.WebApi.Extensions;

namespace EmailSender.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddEmailSender(
                formClientsOptions => Configuration.GetSection("FormClients").Bind(formClientsOptions),
                smtpEmailSenderOptions => Configuration.GetSection("SmtpEmailSender").Bind(smtpEmailSenderOptions),
                corsPolicyBuilder =>
                {
                    var knownOrigins = Configuration.GetSection("KnownCorsOrigins").Get<List<string>>();
                    corsPolicyBuilder.WithOrigins(knownOrigins.ToArray()).AllowAnyHeader().AllowAnyMethod();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseEmailSenderLocalization();
            app.UseEmailSenderHealthChecks();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

        }
    }
}
