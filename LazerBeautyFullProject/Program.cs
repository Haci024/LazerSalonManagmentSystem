using Business.IServices;
using Business.Manager;
using Business.ValidationRules;
using Data.Concrete;
using Data.DAL;
using Data.Repositories;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using Validation.ValidationRules;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<ILazerAppointmentService, LazerAppointmentManager>();//BusineessLayer
builder.Services.AddScoped<ILazerAppointmentDAL, LazerAppointmentRepository>();//DataAccessLayer
builder.Services.AddScoped<IOutMoneyService, OutMoneyManager>();//Bussinesslayer
builder.Services.AddScoped<IOutMoneyDAL, OutMoneyRepository>();//DataAccessLayer 
builder.Services.AddScoped<ICustomerService, CustomerManager>();//Bussinesslayer
builder.Services.AddScoped<ICustomerDAL, CustomerRepository>();//DataAccessLayer
builder.Services.AddScoped<InComeService, IncomeManager>();//Bussinesslayer
builder.Services.AddScoped<InComeDAL, IncomeRepository>();
builder.Services.AddScoped<ILazerAppointmentsReportService, LazerAppointmentsReportManager>();//Bussinesslayer
builder.Services.AddScoped<ILazerAppointmentReportsDAL, LazerAppointmentReportsRepo>();//DataAccessLayer
builder.Services.AddScoped<IKassaDAL, KassaRepository>();
builder.Services.AddScoped<IKassaService, KassaManager>();
builder.Services.AddScoped<IKassaActionListDAL, KassaActionListRepository>();
builder.Services.AddScoped<IKassaActionListService, KassaActionListManager>();
builder.Services.AddScoped<IStockService, StockManager>();
builder.Services.AddScoped<IStockDAL, StockRepository>();


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>().AddErrorDescriber<CustomIdentityValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
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
    name: "ArzumMini",  
    pattern: "{area:exists}/{controller=LazerAppointment}/{action=AllReservations}/{id?}");

app.MapControllerRoute(
    name: "ArzumBeauty",
    pattern: "{area:exists}/{controller=LazerAppointment}/{action=AllReservations}/{id?}");
app.MapControllerRoute(
    name: "ArzumEstetic",
    pattern: "{area:exists}/{controller=LazerAppointment}/{action=AllReservations}/{id?}");
app.MapControllerRoute(
    name: "Admin",

    pattern: "{area:exists}/{controller=Kassa}/{action=BudgetPage}/{id?}");
app.MapControllerRoute(
    name: "Support",
    pattern: "{area:exists}/{controller=Statistics}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");



app.Run();
