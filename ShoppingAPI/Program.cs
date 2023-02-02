using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.Helpers;
using ShoppingAPI.Services;
using ShoppingAPI.Services.Contracts;
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureSqlServerContext(builder.Configuration);

//Repositories
builder.Services.ConfigureRepositoryWrapper();

//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IProductBrandService, ProductBrandService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISuperMarketService, SuperMarketService>();
builder.Services.AddScoped<ISuperMarketProductBrandService, SuperMarketProductBrandService>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();
builder.Services.AddScoped<IShoppingDetailListService, ShoppingDetailListService>();



builder.Services.AddAutoMapper(typeof(Program));



builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

