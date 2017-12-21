﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHandlerHelper.Model
{
    public class CryptoHeader
    {
        public string Key { get; set; }

        public string InitializationVector { get; set; }

        public string FormatValue { get; set; }
    }
}
