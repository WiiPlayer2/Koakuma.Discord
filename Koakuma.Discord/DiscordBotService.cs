using Koakuma.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koakuma.Shared.Messages;
using Discord.WebSocket;
using Koakuma.Discord.Shared.Messages;

namespace Koakuma.Discord
{
    public class DiscordBotService : IService
    {
        private DiscordSocketClient client;

        #region Public Properties

        public ModuleConfig Config { get; set; }

        public ModuleFeatures Features { get { return ModuleFeatures.Service; } }

        public IEnumerable<string> Hooks
        {
            get
            {
                return new[]
                {
                    "message.received",
                };
            }
        }

        public string ID { get { return "discord.bot"; } }

        public IEnumerable<string> Invokes
        {
            get
            {
                return new[]
                {
                    "message.send",
                };
            }
        }

        public IKoakuma Koakuma { get; set; }

        #endregion Public Properties

        #region Public Methods

        public BaseMessage Invoke(ModuleID from, string command, byte[] payload = null)
        {
            var cmd = ExtractArgument(ref command);
            switch (cmd)
            {
                case "message.send":
                    {
                        var channelName = ExtractArgument(ref command);
                        var msg = ExtractArgument(ref command);

                        var channel = client.GetGuild(Config.Get("guild", 0UL))
                            .TextChannels
                            .SingleOrDefault(o => o.Name == channelName);
                        channel?.SendMessageAsync(msg);
                    }
                    break;
            }
            return null;
        }

        public void Load()
        {
            var config = new DiscordSocketConfig()
            {

            };
            client = new DiscordSocketClient(config);
            client.MessageReceived += Client_MessageReceived;
        }

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            if (arg.Author.Id != client.CurrentUser.Id && client.GetGuild(Config.Get("guild", 0UL))?.GetChannel(arg.Channel.Id) != null)
            {
                Koakuma.Logger.Log(LogLevel.Verbose, $"[#{arg.Channel}] {arg.Author}: {arg}");
                Koakuma.SendHook("message.received", new MessageReceivedMessage()
                {
                    Channel = arg.Channel.To(),
                    Author = arg.Author.To(),
                    Text = arg.Content,
                });
            }
        }

        public void OnMessage(ModuleID from, BaseMessage msg, byte[] payload)
        {
        }

        public void Reload()
        {
        }

        public void Start()
        {
            client.LoginAsync(global::Discord.TokenType.Bot,
                Config.Get<string>("token", null)).Wait();
            client.StartAsync().Wait();
        }

        public void Stop()
        {
        }

        public void Unload()
        {
        }

        #endregion Public Methods

        #region Private Methods

        private string ExtractArgument(ref string args)
        {
            if (args == null)
            {
                return null;
            }

            var index = args.IndexOf('|');
            var ret = args;
            if (index != -1)
            {
                ret = args.Substring(0, index);
                args = args.Substring(index + 1);
            }
            else
            {
                args = null;
            }
            return ret;
        }

        #endregion Private Methods
    }
}
