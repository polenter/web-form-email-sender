using System;
using Microsoft.Extensions.Internal;

namespace EmailSender.WebApi.Utils
{
    internal class DefaultSystemClock: ISystemClock
    {
        public DefaultSystemClock()
        {
        }

        public DateTimeOffset UtcNow => DateTimeOffset.Now;
    }
}