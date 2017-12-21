﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IAKNDataForIFDoc
    {
        [OperationContract]
        AKNDocOutput GetDataForIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
    }
}
