using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace EvalServiceLibrary.Model
{
    public class EvalRepository : IDisposable
    {
        private EvalContext _context;

        public EvalRepository()
        {
            _context = new EvalContext();
        }

        public List<EvalDTO> GetEvals()
        {
            return _context.Evals.ToList();
        }

        public void SubmitEval(EvalDTO eval)
        {
            var foundEval = _context.Evals.Find(eval.Id);

            if (foundEval != null)
            {
                throw new FaultException(eval.Id);
            }

            _context.Evals.Add(eval);
            _context.SaveChanges();

        }

        public void RemoveEval(string id)
        {
            var eval = _context.Evals.Find(id);

            if (eval == null)
            {
                throw new FaultException(id);
            }

            _context.Evals.Remove(eval);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
