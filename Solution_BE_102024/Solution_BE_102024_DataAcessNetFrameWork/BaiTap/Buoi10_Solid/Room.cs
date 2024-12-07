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
    public abstract class Room : IRoomDoWork
    {
        public int RoomNumber { get; set; }

        public string RoomType { get; set; }

        public bool IsAvailable { get; set; }

        public static List<Room> listRoom = new List<Room>();

        public Room() { }

        public Room(int roomNumber, string roomType)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            IsAvailable = true;
        }

        public ProductData InsertRoom(Room room)
        {
            var returnData = new ProductData();

            try
            {
                if (!Validation.CheckSting(room.RoomType))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu không gợp lệ";
                    return returnData;
                }
                if (room.RoomNumber < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ liệu RoomNumber không gợp lệ";
                    return returnData;
                }
                listRoom.Add(room);
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Phòng đã được thêm thành công";
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.StackTrace;
                return returnData;
            }
            return returnData;
        }
    }
}
