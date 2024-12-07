using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface.Bussiness
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
