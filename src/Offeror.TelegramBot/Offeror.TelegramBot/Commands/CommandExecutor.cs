using Offeror.TelegramBot.Contracts;
using Offeror.TelegramBot.Exceptions;
using Offeror.TelegramBot.Extensions;
using System.Collections.Concurrent;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Offeror.TelegramBot.Commands
{
    public sealed class CommandExecutor : ICommandExecutor
    {
        private readonly IDictionary<string, Func<IBotCommand>> _commands;

        private readonly ConcurrentDictionary<long, IBotCommand> _usersCommands;

        public CommandExecutor(IServiceProvider provider)
        {
            _commands = new Dictionary<string, Func<IBotCommand>>()
            {
                { BotCommands.StartCommand, () => new StartCommand(provider) },
                { BotCommands.StopCommand,  () => new StopCommand(provider) },
            };

            _usersCommands = new();
        }

        public IBotCommand? GetUserCommand(long chatId) => _usersCommands.GetValueOrDefault(chatId);

        public async Task ExecuteAsync(Update update)
        {
            if (update.IsTypeMessage(MessageEntityType.BotCommand))
            {
                SetBotCommand(update);
            }

            IBotCommand? botCommand = GetUserCommand(update.GetChatId());

            if(botCommand?.IsCompleted ?? false)
            {
                /// Will happen only if the end of the command has not been processed
                throw new InvalidOperationException("The command is completed");
            }

            await (botCommand?.InvokeAsync(update)
                ?? throw new CommandNoSelectException());
        }

        private void SetBotCommand(Update update)
        {
            string? commandKey = update.GetTextMessage();

            if (string.IsNullOrWhiteSpace(commandKey) || !_commands.ContainsKey(commandKey))
            {
                throw new CommandNotFoundException();
            }

            long chatId = update.GetChatId();
            var currentCommand = GetUserCommand(chatId);

            if (currentCommand?.CommandName == commandKey)
            {
                currentCommand.Cast<IBotStateMachine>()
                    .Restart();

                return;
            }

            var newCommand = _commands[commandKey].Invoke();
            newCommand.CommandCompleted += CommandCompletedEventHandler;

            _usersCommands.AddOrUpdate(chatId, newCommand, (id, oldCommand) => newCommand);
        }

        private void CommandCompletedEventHandler(object? sender, long id) => TryRemoveCommand(id);

        /// <summary>
        /// Deleted commands whose lifetime has expired
        /// </summary>
        /// <returns>A collection of deleted commands and the chat id that the command belongs to</returns>
        public async Task<IEnumerable<KeyValuePair<long, IBotCommand>>> ClearOutdatedCommandsAsync()
        {
            return await Task.Run(() => ClearOutdatedCommands());
        }

        private IEnumerable<KeyValuePair<long, IBotCommand>> ClearOutdatedCommands()
        {
            /// TODO: Take out in the project configuration
#if DEBUG
            TimeSpan commandsLifetime = TimeSpan.FromMinutes(5);
#else
            TimeSpan commandsLifetime = TimeSpan.FromMinutes(20);
#endif
            var expiredCommands = _usersCommands.Where(key => DateTime.Now - key.Value.CommandStartTime >= commandsLifetime);

            foreach (var item in expiredCommands)
            {
                if (!TryRemoveCommand(item.Key))
                {
                    throw new FailDeleteCommandException();
                }

                yield return item;
            }
        }

        private bool TryRemoveCommand(long id)
        {
            if (!_usersCommands.TryRemove(id, out IBotCommand? command))
            {
                return false;
            }

            command.CommandCompleted -= CommandCompletedEventHandler;
            return true;
        }
    }
}
