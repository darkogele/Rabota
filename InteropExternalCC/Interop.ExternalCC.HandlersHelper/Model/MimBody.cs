﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Interop.ExternalCC.HandlersHelper.Model
{
    public class MimBody
    {
        [XmlAttribute]
        public string id { get; set; }
        public object Message { get; set; }
    }
}