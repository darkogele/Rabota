using System;

namespace EvalServiceLibrary.Model
{
    public class EvalDTO
    {
        public string Id { get; set; }
        public string Submitter { get; set; }
        public string Comments { get; set; }
        public DateTime TimeSubmitted { get; set; }
    }
}
