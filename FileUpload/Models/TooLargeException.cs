using System;
using System.Runtime.Serialization;

namespace FileUpload.Controllers
{
    [Serializable]
    internal class TooLargeException : Exception
    {
        public int SizeInBytes { get; set; }
        public int LengthInBytes { get; set; }

        public TooLargeException()
        {
            SizeInBytes = 25;
            LengthInBytes = 25;
        }
        
        public TooLargeException(string message) : base(message)
        {

        }

        public TooLargeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooLargeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}