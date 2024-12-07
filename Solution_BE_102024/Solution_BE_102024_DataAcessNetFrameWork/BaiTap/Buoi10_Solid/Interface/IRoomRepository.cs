using DocumentFormat.OpenXml.Spreadsheet;
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid.Interface
{
    public interface IRoomRepository
    {
        Room GetByIDRoom(int roomNumber);
        void CheckAvailableRoom(int roomNumber, bool isAvailable);
    }
}
