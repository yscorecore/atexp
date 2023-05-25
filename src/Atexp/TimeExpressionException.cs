using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atexp
{

    [Serializable]
    public class TimeExpressionException : Exception
    {
        public TimeExpressionException() { }
        public TimeExpressionException(string message) : base(message) { }
        public TimeExpressionException(string message, Exception inner) : base(message, inner) { }
        protected TimeExpressionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
