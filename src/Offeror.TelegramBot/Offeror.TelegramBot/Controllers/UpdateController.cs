using Microsoft.AspNetCore.Mvc;
using Offeror.TelegramBot.Commands;
using Telegram.Bot.Types;

namespace Offeror.TelegramBot.Controllers
{
    [ApiController]
    [Route("bot/telegram/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly CommandExecutor _commandExecutor;

        public UpdateController(CommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            await _commandExecutor.ExecuteAsync(update);
            return Ok();
        }
    }
}
