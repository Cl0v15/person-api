using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Reflection;
using System.Text;
using TPICAP.Persons.API.Core;
using TPICAP.Persons.API.Mappings;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.API.Requests.Validators;
using TPICAP.Persons.Domain.Persons;
using TPICAP.Persons.Persistence;
using TPICAP.Persons.Persistence.Repositories;

namespace TPICAP.Persons.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);

            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration).CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddControllers();
            services.AddScoped<HttpRequestMiddleware>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TPICAP.Person.API", Version = "v1" });
            });

            ConfigureAuthentication(services);

            services.AddMediatR(Assembly.GetExecutingAssembly());

            ConfigureValidators(services);
            ConfigurePersistance(services);
            ConfigureMappings(services);
            ConfigureSpecifications(services);
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtOptions =>
                {
                    JwtOptions.RequireHttpsMetadata = false;
                    JwtOptions.SaveToken = true;
                    JwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration.GetSection("Jwt:Key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                    };
                });

            var jwtConfig = new JwtConfiguration();
            Configuration.Bind("JWT", jwtConfig);
            services.AddSingleton(jwtConfig);
        }

        private static void ConfigureValidators(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddSingleton<IValidator<AddPersonCommand>, AddPersonCommandValidator>();
            services.AddSingleton<IValidator<UpdatePersonCommand>, UpdatePersonCommandValidator>();
        }

        private static void ConfigureMappings(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void ConfigureSpecifications(IServiceCollection services)
        {
            services.AddSingleton<IExistsPersonSpecification, ExistsPersonSpecification>();
        }

        private void ConfigurePersistance(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(Configuration.GetConnectionString("AppEntities"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TPICAP.Person.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<HttpRequestMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
