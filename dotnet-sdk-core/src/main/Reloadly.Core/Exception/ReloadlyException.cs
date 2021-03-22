using System;

namespace Reloadly.Core.Exception
{
    [Serializable]
    public class ReloadlyException : System.Exception
    {
        public ReloadlyException() { }
        public ReloadlyException(string message) : base(message) { }
        public ReloadlyException(string message, System.Exception inner) : base(message, inner) { }
        protected ReloadlyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
