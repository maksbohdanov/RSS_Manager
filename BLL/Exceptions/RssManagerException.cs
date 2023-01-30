using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public class RssManagerException : Exception
    {
        public RssManagerException()
        {
        }

        public RssManagerException(string message) : base(message)
        {
        }

        public RssManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RssManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
