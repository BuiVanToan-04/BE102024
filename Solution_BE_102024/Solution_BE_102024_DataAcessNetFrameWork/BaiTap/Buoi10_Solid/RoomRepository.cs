using Solution_BE_102024_Common;
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid.Interface;
using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi9_Inteface.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid
{
    public class RoomRepository : IRoomRepository
    {
        List<Room> listRoom = new List<Room>();
        public Room GetByIDRoom(int roomNumber)
        {
            var result = (from room in listRoom
                          where room.RoomNumber == roomNumber
                          select room).FirstOrDefault();
            return result;
        }

        public void CheckAvailableRoom(int roomNumber, bool isAvailable)
        {
            var room = GetByIDRoom(roomNumber);
            if (room != null)
            {
                room.IsAvailable = isAvailable;
            }
        } 
    }
}
