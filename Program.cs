using LibWEB.Controllers;
using LibWEB.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext, LibContext>(options =>
	options.UseMySql("Server=localhost;User=root;Password=R4h4-ch4n_o@;Database=library_web",
					new MySqlServerVersion(new Version(8, 0, 34))));
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); 
    builder.AddDebug();                    
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
