using Koakuma.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koakuma.Discord.Shared.Messages
{
    [Serializable]
    public abstract class DiscordMessage : BaseMessage
    {
        public override Koakuma.Shared.Messages.MessageType Type
        {
            get
            {
                return Koakuma.Shared.Messages.MessageType.Special;
            }

            protected set
            {

            }
        }

        public abstract MessageType DiscordType { get; }
    }
}
