﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientCertTest.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Work";
        }
    }
}