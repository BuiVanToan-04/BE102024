//using BE_10_2024_API.Middleware;

using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DALImpliment;
using BE_102024.DataAces.NetCore.DBContext;
using BE102024.DataAces.NetCore.DAL_Impliment;
using BE102024.DataAces.NetCore.DAL_Interface;
using BE102024.DataAces.NetCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddDbContext<BE_102024Context>(options =>
               options.UseSqlServer(configuration.GetConnectionString("BE_102024_ConnString")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ICategoryServis, CategoryServis>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>(); 
builder.Services.AddSingleton<IProductRepositor, ProductRepositor>();
//builder.Services.AddSingleton<IGenericRoomRepository, GenericRoomRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();


builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<MyCustomeMiddlerware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
