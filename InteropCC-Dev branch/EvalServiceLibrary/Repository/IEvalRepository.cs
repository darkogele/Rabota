using System;
using System.Collections.Generic;
using EvalServiceLibrary.Model;

namespace EvalServiceLibrary.Repository
{
    public interface IEvalRepository
    {
        List<EvalDTO> GetEvals();
        void SubmitEval(EvalDTO eval);
        void RemoveEval(string id);
    }
}
