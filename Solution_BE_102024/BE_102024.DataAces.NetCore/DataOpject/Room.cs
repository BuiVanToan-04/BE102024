using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DataOpject
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType roomType { get; set; }
    }
    public class Room_InsertRequestData
    {
        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int RoomTypeID { get; set; }
    }
}
