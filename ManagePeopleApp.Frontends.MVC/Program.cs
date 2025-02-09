using ManagePeopleApp.Frontends.MVC.Domains.Persons;
using ManagePeopleApp.Frontends.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ?? Register configuration first
builder.Services.AddConfigurationServices(builder.Configuration);

// ?? Register HttpClient before PersonClientService
builder.Services.AddHttpClientServices(builder.Configuration);

// ?? Register application services
builder.Services.AddPersonServices();

// ?? Add MVC and context accessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ?? Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();

