using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RouletteParser.LiveDealer.Exceptions
{
    [Serializable]
    public class SessionDataParseException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SessionDataParseException()
        {
        }

        public SessionDataParseException(string message) : base(message)
        {
        }

        public SessionDataParseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SessionDataParseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
