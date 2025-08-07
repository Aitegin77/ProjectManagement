namespace AppProjectManagement.Extensions
{
    internal static class WebApplicationBuilderExtension
    {
        internal static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.RegisterConnectionString(configuration);
            services.AddHttpContextAccessor();
            services.RegisterServices();
            services.RegisterRepositories();
            services.RegisterAuthentication(configuration);
            services.AddAuthorization();
        }

        internal static WebApplication Configure(this WebApplicationBuilder builder)
        {
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.InitializeDatabase();
            app.RegisterMapping();

            return app;
        }
    }
}
