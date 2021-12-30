using MediatR;
using Microsoft.EntityFrameworkCore;
using Offeror.TelegramBot;
using Offeror.TelegramBot.BackgroundServices;
using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Data;
using Offeror.TelegramBot.Middleware;
using Offeror.TelegramBot.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTelegramBot(builder.Configuration)
    .AddBotStates(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<ICommandExecutor, CommandExecutor>();
builder.Services.AddScoped<SearchFilter>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly())
    .AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddHostedService<CommandCleanerHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    /// Environment IsDevelopment
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
