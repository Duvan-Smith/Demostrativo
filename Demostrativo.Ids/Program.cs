using Demostrativo.Ids.Configs.Api;
using Demostrativo.Ids.Configs.Clients;
using Demostrativo.Ids.Configs.Identity;
using Demostrativo.Ids.Configs.Scopes;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoIds4.IdentityServer4AspNet.Data;
using ProyectoIds4.IdentityServer4AspNet.Models;

string MiCors = "MiCords";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy(MiCors, builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(new[]
        {
            "https://localhost:3000",
            "https://localhost:5001",
            "https://localhost:5002",
        })
        .SetIsOriginAllowedToAllowWildcardSubdomains();
}));

#region Auth
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var builderIds = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = true;
})
    .AddInMemoryIdentityResources(IdentityResourcesHelper.GetIdentityResources())
    .AddInMemoryApiResources(ApiResourcesHelper.GetApiResources())
    .AddInMemoryApiScopes(ScopesHelper.GetApiScopes())
    .AddInMemoryClients(ClientsHelper.GetClients())
    .AddAspNetIdentity<ApplicationUser>();

// not recommended for production - you need to store your key material somewhere secures
builderIds.AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        // register your IdentityServer with Google at https://console.developers.google.com
        // enable the Google+ API
        // set the redirect URI to https://localhost:5001/signin-google
        options.ClientId = "copy client ID from Google here";
        options.ClientSecret = "copy client secret from Google here";
    });
#endregion Auth

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(MiCors);

app.UseIdentityServer();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

using var services = app.Services.CreateScope();
var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();
try
{
    dbContext?.Database.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

app.Run();
