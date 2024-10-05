using ApiDevBP.Options;
using ApiDevBP.Profiles;
using ApiDevBP.Services;
using Microsoft.OpenApi.Models;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
    });
    var xmlFilename = $"./Documentation.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
