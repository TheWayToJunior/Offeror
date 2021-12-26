using MediatR;
using Microsoft.EntityFrameworkCore;
using Offeror.TelegramBot;
using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTelegramBot(builder.Configuration)
    .AddScoped<CommandExecutor>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly())
    .AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    /// Environment IsDevelopment
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
