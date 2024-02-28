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

builder.Services.AddScoped<IKassaService,KassaManager>();   

builder.Services.AddScoped<IKassaActionListDAL, KassaActionListRepository>();
builder.Services.AddScoped<IKassaActionListService, KassaActionListManager>();

builder.Services.AddScoped<IStockService, StockManager>();
builder.Services.AddScoped<IStockDAL, StockRepository>();

builder.Services.AddScoped<ILazerMasterService, LazerMasterManager>();
builder.Services.AddScoped<ILazerMasterDAL, LazerMasterRepository>();

builder.Services.AddScoped<ILazerCategoryService, LazerCategoryManager>();
builder.Services.AddScoped<ILazerCategoryDAL, LazerCategoryRepository>();

builder.Services.AddScoped<ISolariumUsingListService, SolariumUsingListManager>();//Bussinesslayer
builder.Services.AddScoped<ISolariumUsingListDAL, SolariumUsingListRepository>();//DataAccessLayer

builder.Services.AddScoped<ISolariumCategoryService, SolariumCategoryManager>();//Bussinesslayer
builder.Services.AddScoped<ISolariumCategoryDAL, SolariumCategoryRepository>();//DataAccessLayer

builder.Services.AddScoped<ISolariumAppointmentService, SolariumAppointmentManager>();//Bussinesslayer
builder.Services.AddScoped<ISolariumAppointmentDAL, SolariumAppointmentRepository>();//DataAccessLayer

builder.Services.AddScoped<IBodyShapingAppointmentService, BodyShapingAppointmentManager>();
builder.Services.AddScoped<IBodyShapingAppointmentDAL, BodyShapingAppointmentsRepository>();//DataAccessLayer

builder.Services.AddScoped<IBodyShapingPacketCategoriesService, BodyShapingPacketCategoriesManager>();//Bussinesslayer
builder.Services.AddScoped<IBodyShapingPacketCategoriesDAL, BodyShapingPacketCategoriesRepository>();
//DataAccessLayer
builder.Services.AddScoped<IBodyShapingSessionListService, BodyShapingSessionListManager>();
builder.Services.AddScoped<IBodyShapingSessionListDAL, BodyShapingSessionListRepository>();

builder.Services.AddScoped<IBodyShapingPacketsReportsService, BodyShapingPacketsReportsManager>();
builder.Services.AddScoped<IBodyShapingAppointmentReportsDAL, BodyShapingAppointmentReportRepository>();

builder.Services.AddScoped<ICosmetologService, CosmetologManager>();
builder.Services.AddScoped<ICosmetologDAL, CosmetologRepository>();

builder.Services.AddScoped<ICosmetologyReportService, CosmetologyReportManager>();
builder.Services.AddScoped<ICosmetologyReportDAL, CosmetologyReportsRepository>();

builder.Services.AddScoped<ISpendCategoryService, SpendCategoryManager>();
builder.Services.AddScoped<ISpendCategoryDAL, SpendCategoryRepository>();

builder.Services.AddScoped<ICosmetologCategoryService, CosmetologCategoryManager>();
builder.Services.AddScoped<ICosmetologCategoryDAL, CosmetologCategoryRepository>();

builder.Services.AddScoped<ICosmetologyAppointmentService, CosmetologAppointmentManager>();
builder.Services.AddScoped<ICosmetologyAppointmentDAL, CosmetologyAppointmentRepository>();

builder.Services.AddScoped<IBodyShapingMasterService, BodyShapingMasterManager>();
builder.Services.AddScoped<IBodyShapingMasterDAL, BodyShapingMasterRepository>();

builder.Services.AddScoped<ILipuckaAppointmentService, LipuckaAppointmentManager>();
builder.Services.AddScoped<ILipuckaAppointmentDAL, LipuckaAppointmentRepository>();

builder.Services.AddScoped<ILipuckaCategoriesService, LipuckaCategoriesManager>();
builder.Services.AddScoped<ILipuckaCategoriesDAL, LipuckaCategoriesRepository>();

builder.Services.AddScoped<ILipuckaReportsService, LipuckaReportsManager>();
builder.Services.AddScoped<ILipuckaReportsDAL, LipuckaReportsRepository>();

builder.Services.AddScoped<IPirsinqAppointmentService, PirsinqAppointmentManager>();
builder.Services.AddScoped<IPirsinqAppointmentDAL, PirsinqAppointmentRepository>();

builder.Services.AddScoped<IPirsinqCategoriesService, PirsinqCategoriesManager>();
builder.Services.AddScoped<IPirsinqCategoriesDAL, PirsinqCategoriesRepository>();

builder.Services.AddScoped<IPirsinqReportsService, PirsinqReportsManager>();
builder.Services.AddScoped<IPirsinqReportsDAL, PirsinqReportsRepository>();




builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
   options.Password.RequireUppercase = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuöğıəşçvwxyzABCDEFGHIJŞÖĞIƏKLMNOPÇQRSTUVWXYZ0123456789";
   
    

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
