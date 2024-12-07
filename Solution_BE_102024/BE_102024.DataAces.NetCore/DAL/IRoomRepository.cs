using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DataOpject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DAL
{
    public interface IRoomRepository
    {
        public Task<ReturnData> InsertRoom(Room_InsertRequestData room);
        public Task<ReturnData> UpdateRoom(Room_InsertRequestData room);
        public Task<ReturnData> DeleteRoom(int roomID);
        public Task<List<Room>> GetAllRoom(string? roomName);
    }
}
