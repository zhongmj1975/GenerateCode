using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Utilities
{
    [Serializable]
    public class EventArgs<T> : System.EventArgs
    {
        public T Argument;

        public EventArgs() : this(default(T))
        {
        }

        public EventArgs(T argument)
        {
            Argument = argument;
        }
    }
}
