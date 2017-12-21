using System;
using System.ComponentModel.DataAnnotations;

namespace Implementations.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string WorkStatus { get; set; }
        public string MarriageStatus { get; set; }
    }
}
