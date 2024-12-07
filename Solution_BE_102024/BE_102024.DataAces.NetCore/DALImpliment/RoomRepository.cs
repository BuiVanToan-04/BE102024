using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using BE_102024.DataAces.NetCore.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class RoomRepository : IRoomRepository
    {
        private BE_102024Context _context;
        public RoomRepository(BE_102024Context context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllRoom(string? roomName)
        {
            try
            {
                var roomDb = _context.room.ToList();
                if (!string.IsNullOrEmpty(roomName))
                {
                    roomDb = roomDb.FindAll(r => r.RoomName.Contains(roomName));
                }
                return roomDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnData> DeleteRoom(int roomID)
        {
            var returnData = new ReturnData();
            try
            {
                if (roomID == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "RoomID rỗng";
                    return returnData;
                }
                var findRoomID = await _context.room.FindAsync(roomID);
                if (findRoomID == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = $"Không tìm thấy: {roomID}";
                    return returnData;
                } 
                _context.Remove(findRoomID);
                var rs = _context.SaveChanges();
                if (rs == 0)
                {
                    returnData.ReponseCode = -5;
                    returnData.ResponseMessenger = "Delete thất bại";
                    return returnData;
                }
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Delete thành công";
                return returnData;
            }
            catch (Exception ex) 
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
                return returnData;
            }
        }

        public async Task<ReturnData> InsertRoom(Room_InsertRequestData room)
        {
            var returnData = new ReturnData();
            try
            {
                if (room == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ Liệu Room Không Hợp Lệ";
                    return returnData;
                }
                if (room.RoomID < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ Liệu RoomID Không Hợp Lệ";
                    return returnData;
                }
                if (!Validation.CheckSting(room.RoomName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "Dữ Liệu RoomName Không Được Rỗng";
                    return returnData;
                }
                if (!Validation.CheckXSSInput(room.RoomName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "Dữ Liệu RoomName Lỗi XSS";
                    return returnData;
                }
                if (room.RoomTypeID < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ Liệu RoomTypeID Không Hợp Lệ";
                    return returnData;
                }
                var request = new Room
                {
                    RoomID = room.RoomID,
                    RoomName = room.RoomName,
                    RoomTypeID = room.RoomTypeID,
                };
                _context.Add(request);
                var rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    returnData.ReponseCode = -5;
                    returnData.ResponseMessenger = "Thêm mới thất bại";
                    return returnData;
                }
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Thêm mới thành công";
                return returnData;
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }

        public async Task<ReturnData> UpdateRoom(Room_InsertRequestData room)
        {
            var returnData = new ReturnData();
            try
            {
                if (room == null)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ Liệu Room Không Hợp Lệ";
                    return returnData;
                }
                if (room.RoomID < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ Liệu RoomID Không Hợp Lệ";
                    return returnData;
                }
                if (!Validation.CheckSting(room.RoomName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.ProductNameNull;
                    returnData.ResponseMessenger = "Dữ Liệu RoomName Không Được Rỗng";
                    return returnData;
                }
                if (!Validation.CheckXSSInput(room.RoomName))
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.InvalidXSSInput;
                    returnData.ResponseMessenger = "Dữ Liệu RoomName Lỗi XSS";
                    return returnData;
                }
                if (room.RoomTypeID < 0)
                {
                    returnData.ReponseCode = (int)ProductInsertStatus.DataInvalid;
                    returnData.ResponseMessenger = "Dữ Liệu RoomTypeID Không Hợp Lệ";
                    return returnData;
                }
                var request = new Room
                {
                    RoomID = room.RoomID,
                    RoomName = room.RoomName,
                    RoomTypeID = room.RoomTypeID,
                };
                _context.Update(request);
                var rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    returnData.ReponseCode = -5;
                    returnData.ResponseMessenger = "Update thất bại";
                    return returnData;
                }
                returnData.ReponseCode = (int)ProductInsertStatus.Success;
                returnData.ResponseMessenger = "Update thành công";
                return returnData;
            }
            catch (Exception ex)
            {
                returnData.ReponseCode = -99;
                returnData.ResponseMessenger = ex.Message;
            }
            return returnData;
        }
    }
}
