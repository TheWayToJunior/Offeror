using Microsoft.EntityFrameworkCore;
using Offeror.TelegramBot;
using Offeror.TelegramBot.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(builder.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    /// Environment IsDevelopment
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
