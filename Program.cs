using Microsoft.EntityFrameworkCore;
using RMeets;
using RMeets.Contexts;
using RMeets.Middlewares.ChechAccess;
using RMeets.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Java.session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.IsEssential = true;
});


builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseNpgsql("Server=localhost;Port=5432;Database=RMeets;User Id=postgres;Password=admin;").EnableSensitiveDataLogging().UseLazyLoadingProxies(),ServiceLifetime.Singleton);
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<BlankRepository>();
builder.Services.AddScoped<CityRepository>();
builder.Services.AddScoped<FactRepository>();
builder.Services.AddScoped<GenderRepository>();
builder.Services.AddScoped<InterestRepository>();
builder.Services.AddScoped<PhotosRepository>();
builder.Services.AddScoped<ProfileRepository>();
builder.Services.AddScoped<ReactionRepository>();
builder.Services.AddScoped<TargetRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BlankDataService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAccessUrl();
//app.UseAuthorization();
app.MapRazorPages();
app.Run();
