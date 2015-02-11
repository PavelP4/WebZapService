using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebZapService.Exceptions
{
    public class AlertSendException : Exception
    {
        public AlertSendException()
        {
        }

        public AlertSendException(string message)
            : base(message)
        {
        }

        public AlertSendException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
