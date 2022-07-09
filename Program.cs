using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ManterCursosAPI.Data;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ManterCursosAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ManterCursosAPIContext") ?? throw new InvalidOperationException("Connection string 'ManterCursosAPIContext' not found.")));

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();

                      });
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

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
