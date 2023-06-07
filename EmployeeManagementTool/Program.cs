using EmployeeManagementTool.DataModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Service For Db Conetext
builder.Services.AddDbContext<EmployeeDBContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add Services For Session
builder.Services.AddSession();
builder.Services.AddHttpClient("HttpClientWithSSLUntrusted")
               .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
               {
                   // ClientCertificateOptions = ClientCertificateOption.Manual,
                   ServerCertificateCustomValidationCallback =
                   (httpRequestMessage, cert, cetChain, policyErrors) =>
                   {
                       return true;
                   }
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
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Login}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Admin}/{action=LogIn}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "Employees",
        pattern: "{area:exists}/{controller=Employee}/{action=EmployeeLogin}/{id?}"
    );

    // Set default URL to employee login page
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "",
        defaults: new { area = "Admin", controller = "Admin", action = "LogIn" }
    );
});

app.Run();
