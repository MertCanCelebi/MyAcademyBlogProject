using Blogy.Business.Extensions;
using Blogy.Business.Services.GeminiServices;
using Blogy.Business.Services.HuggingServices;
using Blogy.DataAccess.Extensions;
using Blogy.WebUI.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IGeminiService, GeminiService>();
builder.Services.AddScoped<IToxicityService, HuggingFaceToxicityService>();
builder.Services.AddMemoryCache();
builder.Services.AddServicesExt();
builder.Services.AddRepositoriesExt(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login/Index";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
