using System;
using EvalServiceLibrary.Model;
using InteropCC.Resources;

namespace EvalServiceLibrary.Exceptions
{
    public class DuplicateEvalException : Exception
    {
        private EvalDTO _eval;

        public DuplicateEvalException(EvalDTO eval)
        {
            _eval = eval;
        }

        public override string Message
        {
            get { return String.Format(Resources.DuplicateEval, _eval.Id); }
        }
    }
}
