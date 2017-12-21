using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден клиент
    public class NotFoundClient : Exception
    {
        private string _clientId;
        public NotFoundClient(string clientId)
        {
            _clientId = clientId;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.NotFoundClient, _clientId);
            }
        }
    }
}
