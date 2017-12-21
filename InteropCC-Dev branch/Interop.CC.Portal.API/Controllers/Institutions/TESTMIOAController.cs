using Interop.CC.CrossCutting.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    [System.Web.Http.Authorize]
    public class TESTMIOAController : ApiController
    {
        private ILogger _logger;

        // Опис: Конструктор на TESTMIOAController модулот 
        // Влезни параметри: модел ILogger
        public TESTMIOAController(ILogger logger)
        {
            _logger = logger;
        }
        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };

        // Опис: Методот го повикува TestServiceMioaClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност firstName, lastName
        // Излезни параметри: податочен тип string 
        [System.Web.Http.HttpPost]
        public string GetFullName(string firstName, string lastName)
        {

            var TestClient = new TestServiceMIOA.TestServiceMioaClient();

            var fullName = "";
            try
            {
                fullName = TestClient.GetFullName(firstName, lastName);
                _logger.Info(fullName);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error");
                throw e;
            }
            return fullName;
        }

        // Опис: Методот го повикува TestServiceMioaClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност first, second
        // Излезни параметри: податочен тип string  
        [System.Web.Http.HttpPost]
        public int GetSumOfNums(int first, int second)
        {

            var TestClient = new TestServiceMIOA.TestServiceMioaClient();

            var sum = 0;
            try
            {
                sum = TestClient.SumOfNums(first, second);
                _logger.Info(sum.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error");
                throw e;
            }
            return sum;
        }
    }
}