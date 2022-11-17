global using BTP_API.Context;
global using BTP_API.Helpers;
global using BTP_API.Services;
global using BTP_API.ServicesImpl;
global using BTP_API.Ultils;
global using BTP_API.Models;
global using BTP_API.ViewModels;
global using Microsoft.AspNetCore.Mvc;
global using static BTP_API.Helpers.EnumVariable;
global using System.Data;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(builder.Environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
builder.Configuration.AddConfiguration(configurationBuilder.Build());
// Add services to the container.

var defaultConnectionString = string.Empty;

if (builder.Environment.EnvironmentName == "Development")
{
    defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    // Use connection string provided at runtime by Heroku.
    var connectionUrl = "postgres://pywgupxdwfpxrf:72920fd6643b9123783422128cd07c8ab0381d206608988768d3e1be53fc3441@ec2-3-214-57-29.compute-1.amazonaws.com:5432/dr8bb7r6ai6bk";

    connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
    var userPassSide = connectionUrl.Split("@")[0];
    var hostSide = connectionUrl.Split("@")[1];

    var user = userPassSide.Split(":")[0];
    var password = userPassSide.Split(":")[1];
    var host = hostSide.Split("/")[0];
    var database = hostSide.Split("/")[1].Split("?")[0];

    defaultConnectionString = $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
}

builder.Services.AddDbContext<BTPContext>(option =>
{
    option.UseNpgsql(defaultConnectionString);
});

builder.Services.AddCors(p => p.AddPolicy("BTP_CORS", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
//Life cycle DI: AddSingleton(), AddTransient(), AddScope()
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IManageAdminRepository, ManageAdminRepository>();
builder.Services.AddScoped<IManageBillRepository, ManageBillRepository>();
builder.Services.AddScoped<IManageBookRepository, ManageBookRepository>();
builder.Services.AddScoped<IManageCategoryRepository, ManageCategoryRepository>();
builder.Services.AddScoped<IManageFeeRepository, ManageFeeRepository>();
builder.Services.AddScoped<IManagePostRepository, ManagePostRepository>();
builder.Services.AddScoped<IManageTransactionRepository, ManageTransactionRepository>();
builder.Services.AddScoped<IManageUserRepository, ManageUserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    opt =>
    {
        opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        opt.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        //Tự cấp token 
        ValidateIssuer = false,
        ValidateAudience = false,

        //Ký vào token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("BTP_CORS");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
