using CSRF.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddDebug();
    loggingBuilder.AddConsole();
});
builder.Services.AddSession(options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAntiforgery(options =>
{
    // When supressed, this code is vulnerable. For educational purposes only
    options.SuppressXFrameOptionsHeader = false;
});

builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews(options =>
//{
//    //Adds anti-fogery validation project-wide
//    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
StaticHttpContext.Services = app.Services;

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
