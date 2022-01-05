using Microsoft.AspNetCore.Mvc;
using Offeror.TelegramBot.Commands;
using Offeror.TelegramBot.Contracts;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Controllers
{
    [ApiController]
    [Route("bot/telegram/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;

        public UpdateController(ICommandExecutor commandExecutor, ITelegramBotClient client)
        {
            _commandExecutor = new ExecutorExceprionHandler(commandExecutor, client);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            await _commandExecutor.ExecuteAsync(update);
            return Ok();
        }
    }
}
