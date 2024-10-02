var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("constr")
            ?? throw new InvalidOperationException("No Connection String Was found.");
builder.Services.AddDbContext<ApplicationDbContext> 
    (options => options.UseSqlServer(ConnectionString));
// Add services to the container. 
builder.Services.AddScoped<IGameServices, GameServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IDeviceServices, DeviceServices>();

builder.Services.AddControllersWithViews(); 



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
