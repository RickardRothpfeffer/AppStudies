using Services;
using Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

//use multiple Secret sources, Database connections and their respective DbContexts
builder.Configuration.AddApplicationSecrets("../Configuration/Configuration.csproj");
builder.Services.AddDatabaseConnections(builder.Configuration);

#region Setup the Dependency service
builder.Services.AddSingleton<IQuoteService, QuoteService>();
builder.Services.AddSingleton<LatinService>();
builder.Services.AddScoped<IMusicService, MusicServiceWapi>();
builder.Services.AddHttpClient(name: "MusicWebApi", configureClient: options =>
{
    options.BaseAddress = new Uri(builder.Configuration["DataService:WebApiBaseUri"]);
    options.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(
            mediaType: "application/json",
            quality: 1.0));
});
#endregion

var app = builder.Build();

//using Hsts and https to secure the site
if (!app.Environment.IsDevelopment())
{
    //https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security
    //https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//Enable static and default files
app.UseDefaultFiles();
app.UseStaticFiles();

//Map Razorpages into Pages folder
app.MapRazorPages();

//Show Mapping works even if it is razor pages, like for WebApi
app.MapGet("/hello", () =>
{
    //read the environment variable ASPNETCORE_ENVIRONMENT
    //Change in launchSettings.json, (not VS2022 Debug/Release)
    var _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var _envMyOwn = Environment.GetEnvironmentVariable("MyOwn");

    return $"Hello World!\nASPNETCORE_ENVIRONMENT: {_env}\nMyOwn: {_envMyOwn}";
});

app.Run();
