using Application.Abstractions;
using Application.Helpers;
using Application.Services;
using Data.Db;
using Data.Services;
using Infrastructure.Printing;
using Microsoft.EntityFrameworkCore;
using static Application.Exceptions.DomainExceptions;

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
builder.Services.AddScoped<IPrinterService, MockPrinterService>();

var cs = builder.Configuration.GetConnectionString("connString");


if (string.IsNullOrWhiteSpace(cs))
    throw new InvalidOperationException("Bağlantı dizesi 'connString' bulunamadı.appsettings.json'ı kontrol ediniz -> ConnectionStrings:connString");

builder.Services.AddDbContext<CodeMagDbContext>(options =>
    options.UseSqlServer(cs));



var app = builder.Build();

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (NotFoundException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status404NotFound;
        await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (ValidationException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (ConflictException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status409Conflict;
        await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
});


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
