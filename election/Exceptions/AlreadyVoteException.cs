using System;
using System.Runtime.Serialization;

namespace election.Exceptions
{
    public class AlreadyVoteException : Exception
    {
        public AlreadyVoteException()
        {
        }

        public AlreadyVoteException(string message) : base(message)
        {

        }

        public AlreadyVoteException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected AlreadyVoteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
