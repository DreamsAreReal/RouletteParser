﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RouletteParser.GrandCasino.Exceptions
{
    [Serializable]
    public class ApiCodeParseException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ApiCodeParseException()
        {
        }

        public ApiCodeParseException(string message) : base(message)
        {
        }

        public ApiCodeParseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ApiCodeParseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
