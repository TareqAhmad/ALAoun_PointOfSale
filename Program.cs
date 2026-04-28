using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Services; 
using ALAoun_Pos.Data;
using ALAoun_Pos.Models;

var builder = WebApplication.CreateBuilder(args);

// DI Container configuration

builder.Services.AddControllersWithViews(); // extension method to add MVC services
builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // Registering the configuration instance as a singleton
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


builder.Services.AddScoped<DbHelper>(); 
builder.Services.AddScoped<IHomeService,HomeService>();    
builder.Services.AddScoped<ICompaniesService, CompaniesService>();  
builder.Services.AddScoped<IBranchesService,BranchesService>(); 
builder.Services.AddScoped<IPosPointsService,PosPointsService>(); 
builder.Services.AddScoped<IUsersServices,UsersService>(); 
builder.Services.AddScoped<ICategoriesService,CategoriesService>(); 
builder.Services.AddScoped<IProductsService,ProductsService>(); 
builder.Services.AddScoped<ICustomersService,CustomersService>(); 
builder.Services.AddScoped<ISuppliersService,SuppliersService>();
builder.Services.AddScoped<ISalesInvoicesService,SalesInvoicesService>(); 
builder.Services.AddScoped<IPurchaseInvoicesService,PurchaseInvoicesService>();  
builder.Services.AddScoped<IExpensesService,ExpensesService>(); 
builder.Services.AddScoped<IPendingInvoicesService,PendingInvoicesService>(); 
builder.Services.AddScoped<IStockMovementsService,StockMovementsService>();
builder.Services.AddScoped<IDiscountsService, DiscountsService>();
builder.Services.AddScoped<ITaxiesService, TaxiesService>();
builder.Services.AddScoped<IPaymentMethodsService, PaymentMethodsService>();

var app = builder.Build();


// Middleware Setup

app.UseSession(); 

app.UseStaticFiles(); 

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"
);



app.Run();
