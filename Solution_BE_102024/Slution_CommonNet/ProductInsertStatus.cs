using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork
{
    public enum ProductInsertStatus
    {
        Success = 1,            //Thành công 
        DataInvalid = -1,       //Dữ liệu không hợp lệ 
        ProductNameNull = -2,   //Dữ liệu rỗngclear ạ
        InvalidXSSInput = -3,   //Lỗi XSS
        Dupplicate = -4         //Dữ liệu trùng
    }
}
