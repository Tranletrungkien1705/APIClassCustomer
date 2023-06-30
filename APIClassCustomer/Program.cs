using Microsoft.EntityFrameworkCore;
using APIClassCustomer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DbConnection>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(name: "MyRequestMVCAPI",
    policy =>
    {
        policy.WithOrigins("https://localhost:7017")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }
    ));

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
app.UseCors("MyRequestMVCAPI");

app.Run();
