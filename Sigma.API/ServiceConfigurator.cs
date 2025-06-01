namespace Sigma.API
{
    using AutoMapper;
    using Microsoft.OpenApi.Models;
    using Sigma.Infra.CrossCutting.IoC;

    public class ServiceConfigurator
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public ServiceConfigurator(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void ConfigureServices()
        {
            AddBasicServices();
            AddSwagger();
            AddDatabase();
            AddAutoMapper();
            AddApplicationServices();
        }

        private void AddBasicServices()
        {
            _services.AddControllers();
            _services.AddEndpointsApiExplorer();
        }

        private void AddSwagger()
        {
            _services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Informe o token JWT: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private void AddDatabase()
        {
            _services.AddApplicationContext(_configuration.GetValue<string>("ConnectionStrings:Database")!);
        }

        private void AddAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(mapperConfig => mapperConfig.AddMaps(["Sigma.Application"]));
            _services.AddSingleton(mapperConfiguration.CreateMapper());
        }

        private void AddApplicationServices()
        {
            _services.AddApplicationServicesCollections(_configuration);
            _services.AddAuthorization();
        }
    }
}
