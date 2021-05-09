using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RouletteParser.GrandCasino.Exceptions
{
    [Serializable]
    public class UrlParseException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UrlParseException()
        {
        }

        public UrlParseException(string message) : base(message)
        {
        }

        public UrlParseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UrlParseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
