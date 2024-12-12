//using BE_10_2024_API.Middleware;

using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DALImpliment;
using BE_102024.DataAces.NetCore.DALImpliment.ImplimentCreateToken;
using BE_102024.DataAces.NetCore.DALImpliment.Token;
using BE_102024.DataAces.NetCore.DBContext;
using BE102024.DataAces.NetCore.DAL_Impliment;
using BE102024.DataAces.NetCore.DAL_Interface;
using BE102024.DataAces.NetCore.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddDbContext<BE_102024Context>(options =>
               options.UseSqlServer(configuration.GetConnectionString("BE_102024_ConnString")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ICategoryServis, CategoryServis>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>(); 
builder.Services.AddSingleton<IProductRepositor, ProductRepositor>();
//builder.Services.AddSingleton<IGenericRoomRepository, GenericRoomRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IToken, TokenImpliment>();


builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<MyCustomeMiddlerware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
