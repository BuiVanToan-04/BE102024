using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.CheckConditions
{
    public class ReturnData
    {
        public int ReponseCode { get; set; }
        public string ResponseMessenger { get; set; }
    }

    public class ProductData : ReturnData
    {

    }
}
