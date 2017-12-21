using System;
using InteropCC.Resources;

namespace EvalServiceLibrary.Exceptions
{
    public class NotFoundEvalException : Exception
    {
        private string _id;

        public NotFoundEvalException(string id)
        {
            _id = id;
        }

        public override string Message
        {
            get { return String.Format(Resources.NotFoundEval, _id); }
        }
    }
}
