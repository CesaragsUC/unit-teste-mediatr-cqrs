using Domain;
using Domain.Interfaces;
using Infrasctructure;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

// Antes de construir o aplicativo, inicialize o SQLitePCL.raw
Batteries.Init();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatrService();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
