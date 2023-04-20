using TaxCalculator.Controllers;
using TaxCalculator.Models;

var publisher = new Publisher();
var deductSub = new DisplaySubscriber();

publisher.Subscribe(deductSub.Display);

Deduction d = new Deduction();

publisher.Publish(d);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/");

app.Run();
