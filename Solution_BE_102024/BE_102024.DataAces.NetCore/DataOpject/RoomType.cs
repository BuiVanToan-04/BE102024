using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DataOpject
{
    public class RoomType
    {
        [Key]
        public int RoomTypeID { get; set; }
        public string? RoomTypeName { get; set; }

    }
}
