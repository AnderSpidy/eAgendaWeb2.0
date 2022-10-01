using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;

namespace eAgenda.webapi.Config
{
    public static class SwaggerConfig
    {
        public static void ConfigurarSwagger(this IServiceCollection services)//o this antes, tranforma o parametro, em um metodo de extenção de services neste caso
        {
            services.AddSwaggerGen(c =>//LEMBRETE: O SWAGGER É AQUELA "INTERFACE" QUE O NOS INTERAGIMOS COM A WEB API
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eAgenda.webapi", Version = "v1" });
                //PARA ABLITAR UMA OPÇÃO NO SWAGGER PARA IMPUTARMOS TOKEN E CONSEGUIR UTILIZAR O SWAGGER

                //SERVE PARA QUE O SWEGGER RECEBA O TIMESPAN COMO STRING, PARA SER MAIS FACIL IMPUTAR OS DADOS
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor informe o token neste padrão {Bearer token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
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
                        new string[]{}
                    }
                });
            });
        }

    }
}
