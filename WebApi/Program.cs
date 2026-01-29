using Application.Abstractions;
using Application.Helpers;
using Application.Services;
using Data.Db;
using Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LogisticUnitService>();
builder.Services.AddScoped<AggregationService>();
builder.Services.AddScoped<WorkOrderDetailService>();
builder.Services.AddScoped<SerialService>();
builder.Services.AddScoped<Gs1Builder>();
builder.Services.AddScoped<INumberSequence, EfNumberSequence>();

var cs = builder.Configuration.GetConnectionString("connString");
if (string.IsNullOrWhiteSpace(cs))
    throw new InvalidOperationException("Bağlantı dizesi 'connString' bulunamadı.appsettings.json'ı kontrol ediniz -> ConnectionStrings:connString");

builder.Services.AddDbContext<CodeMagDbContext>(options =>
    options.UseSqlServer(cs));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CodeMagDbContext>();

    for (int i = 1; i <= 3; i++)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch when (i < 3)
        {
            Thread.Sleep(500);
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
