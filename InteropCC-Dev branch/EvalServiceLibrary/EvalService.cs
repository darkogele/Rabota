using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using EvalServiceLibrary.Exceptions;
using EvalServiceLibrary.Model;
using System;

namespace EvalServiceLibrary
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class EvalService : IEvalService
    {
        //private EvalRepository _repository;

        //EvalService()
        //{
        //    _repository = new EvalRepository();
        //}

        #region IEvalService Members

        public void SubmitEval(Eval eval)
        {
            var transformedEval = new EvalDTO()
            {
                Id = eval.Id,
                Comments = eval.Comments,
                Submitter = eval.Submitter,
                TimeSubmitted = eval.TimeSubmitted,
            };

           // _repository.SubmitEval(transformedEval);
        }

        public List<Eval> GetEvals()
        {
            //List<EvalDTO> evals = _repository.GetEvals();
            List<Eval> transformedEvals = new List<Eval>();

            var s = new Eval()
            {
                Comments = "comment",
                Id = "1",
                Submitter = "Dijana",
                TimeSubmitted = DateTime.Now
            };
            transformedEvals.Add(s);
            return transformedEvals;
        }

        public void RemoveEval(string id)
        {
            //_repository.RemoveEval(id);
            //try
            //{
            //    _repository.RemoveEval(id);
            //}
            //catch (FaultException ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }

        #endregion
    }
}
