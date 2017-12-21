using System;
using System.Collections.Generic;
using System.Xml.Schema;
using InteropCC.Resources;

namespace Interop.CC.Handler.Helper.Exceptions
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
