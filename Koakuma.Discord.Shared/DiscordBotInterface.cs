using Koakuma.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koakuma.Shared.Messages;
using Koakuma.Discord.Shared.Messages;

namespace Koakuma.Discord.Shared
{
    [Defaults("discord.bot")]
    public class DiscordBotInterface : ServiceInterface
    {
        private InterfaceHookManager<MessageReceivedEventHandler> messageReceived;

        public delegate void MessageReceivedEventHandler(DiscordBotInterface sender, MessageReceivedMessage msg);

        public DiscordBotInterface(ModuleID target, IModule module, TimeSpan timeout) : base(target, module, timeout)
        {
            messageReceived = CreateHookManager<MessageReceivedEventHandler>("message.received");
        }

        protected override void HandleMessageInternal(ModuleID from, BaseMessage msg, byte[] payload)
        {
            switch (msg.Cast<DiscordMessage>().DiscordType)
            {
                case Messages.MessageType.MessageReceived:
                    messageReceived.Invoke(this, msg.Cast<MessageReceivedMessage>());
                    break;
            }
        }

        public event MessageReceivedEventHandler MessageReceived
        {
            add
            {
                messageReceived.Add(value);
            }
            remove
            {
                messageReceived.Remove(value);
            }
        }
    }
}
