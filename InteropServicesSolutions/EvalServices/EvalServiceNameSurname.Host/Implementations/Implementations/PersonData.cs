using System;
using System.Collections.Generic;
using System.ServiceModel;
using Implementations.Contracts;
using Implementations.Helpers;
using Implementations.Models;

namespace Implementations.Implementations
{
    public class PersonData : IPersonData
    {
        public List<Person> GetPersonData(bool getData)
        {
            var people = new List<Person>();
            if (getData)
            {
                var personOne = new Person
                {
                    Name = "Paul",
                    Surname = "Oakenfold",
                    Age = "47",
                    DateOfBirth = new DateTime(1970, 2, 12),
                    MarriageStatus = "Married",
                    WorkStatus = "Employee"
                };
                people.Add(personOne);
                var personTwo = new Person
                {
                    Name = "Cali",
                    Surname = "Dandee",
                    Age = "31",
                    DateOfBirth = new DateTime(1985, 5, 2),
                    MarriageStatus = "Unmarried",
                    WorkStatus = "Employee"
                };
                people.Add(personTwo);
            }
            else
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Нема податоци!");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }

            return people;
        }
    }
}
