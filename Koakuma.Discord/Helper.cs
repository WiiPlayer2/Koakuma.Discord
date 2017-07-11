using Discord.WebSocket;
using Koakuma.Discord.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koakuma.Discord
{
    static class Helper
    {
        public static Channel To(this ISocketMessageChannel channel)
        {
            throw new NotImplementedException();
        }

        public static ISocketMessageChannel To(this Channel channel)
        {
            throw new NotImplementedException();
        }
        public static User To(this SocketUser channel)
        {
            throw new NotImplementedException();
        }

        public static SocketUser To(this User channel)
        {
            throw new NotImplementedException();
        }
    }
}
