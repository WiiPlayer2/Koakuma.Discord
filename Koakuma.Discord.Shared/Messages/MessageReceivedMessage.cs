using Koakuma.Discord.Shared.Data;
using Koakuma.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koakuma.Discord.Shared.Messages
{
    [Serializable]
    public class MessageReceivedMessage : DiscordMessage
    {
        public override MessageType DiscordType
        {
            get
            {
                return MessageType.MessageReceived;
            }
        }

        public Channel Channel { get; set; }

        public User Author { get; set; }

        public string Text { get; set; }
    }
}
