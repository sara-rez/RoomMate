using RoomMate.Core.Handler;
using RoomMate.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddAntiforgery();
builder.Services.AddSqliteDbContext(builder.Configuration);
builder.Services.AddScoped<IBookingRequestHandler, BookingRequestHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
