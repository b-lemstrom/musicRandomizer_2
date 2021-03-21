using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.EventArgs
{
    public class MessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }

        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
