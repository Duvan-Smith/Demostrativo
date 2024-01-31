using Demostrativo.Ocelot.WebApi.Config;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

const string CorsPolicyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var authenticationProviderKey = "IdentityApiKey";

if (environment == "Development")
{
    var routes = "Routes";
    //var routes = "RoutesDevelop";
    builder.Configuration.AddOcelotWithSwaggerSupport(options => options.Folder = routes);

    builder.Services.AddAuthentication()
        .AddJwtBearer(authenticationProviderKey, opt =>
        {
            opt.Authority = "https://localhost:5000";
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
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
            })
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    }));

    builder.Services.AddOcelot(builder.Configuration).AddPolly();
    builder.Services.AddSwaggerForOcelot(builder.Configuration);

    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
        .AddOcelot(routes, builder.Environment)
        .AddEnvironmentVariables();

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

    var app = builder.Build();

    app.UseStaticFiles();

    app.UseSwagger();

    app.UseHttpsRedirection();

    var supportedCultures = new string[] { "es-ES" };
    app.UseRequestLocalization(supportedCultures);
    app.UseCors(CorsPolicyName);

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwaggerForOcelotUI(options =>
    {
        options.PathToSwaggerGenerator = "/swagger/docs";
        options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;
        options.DownstreamSwaggerHeaders = new[]
        {
            new KeyValuePair<string, string>("Auth-Key", "AuthValue"),
        };
    }).UseOcelot().Wait();

    app.MapSwagger();

    app.Run();
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

    builder.Services.AddOcelot();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(o => o.AddPolicy(CorsPolicyName, builder =>
    {
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins(new[]
            {
                "https://localhost:3000",
            })
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    }));

    var app = builder.Build();

    app.UseStaticFiles();

    app.UseHttpsRedirection();

    var supportedCultures = new string[] { "es-ES" };
    app.UseRequestLocalization(supportedCultures);
    app.UseCors(CorsPolicyName);

    app.UseAuthorization();

    app.UseOcelot().Wait();

    app.MapSwagger();

    app.Run();
}