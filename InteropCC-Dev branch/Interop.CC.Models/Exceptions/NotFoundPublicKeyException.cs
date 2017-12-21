using System;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден јавен клуч
    public class NotFoundPublicKeyException : Exception
    {
        private readonly string _routingToken;

        public NotFoundPublicKeyException(string routingToken)
        {
            _routingToken = routingToken;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.NotFoundPublicKey, _routingToken);
            }
        }
    }
}
