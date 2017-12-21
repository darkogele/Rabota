using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using InteropCS.Resources;

namespace CSHandlerHelper.Exceptions
{
    public class XmlValidationException : Exception
    {
        private string _e;

        public XmlValidationException(List<ValidationEventArgs> e)
        {
            foreach (var errorElement in e)
            {
                _e = _e + Environment.NewLine + "Линија: " + errorElement.Exception.LineNumber + " , Позиција: " +
                errorElement.Exception.LinePosition + " , Порака: " + errorElement.Exception.Message;
            }
        }

        public override string Message
        {
            get { return String.Format(Resources.XmlValidation, _e); }
        }
    }
}
