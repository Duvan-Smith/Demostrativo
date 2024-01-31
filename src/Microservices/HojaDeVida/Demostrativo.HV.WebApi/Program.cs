using Micro.Infra.Bus;
using Micro.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

const string CorsPolicyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
#region AddSwaggerGen
builder.Services.AddSwaggerGen(swa =>
{
    swa.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "'Bearer' + token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    swa.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference=new OpenApiReference
               {
                Type= ReferenceType.SecurityScheme,
                   Id="Bearer"
               },
               Scheme="oauth2",
               Name="Bearer",
               In=ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
#endregion AddSwaggerGen

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.Authority = "https://localhost:5000";
    opt.Audience = "demostrativo.HojaDeVida.api";
    opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
});

builder.Services.AddCors(o => o.AddPolicy(CorsPolicyName, builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(new[]
        {
            "https://localhost:3000",
            "https://localhost:5000",
            "https://localhost:5001",
        })
        .SetIsOriginAllowedToAllowWildcardSubdomains();
}));

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.ConfigurationAplicationRabbitMQService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var supportedCultures = new string[] { "es-ES" };
app.UseRequestLocalization(supportedCultures);
app.UseCors(CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();