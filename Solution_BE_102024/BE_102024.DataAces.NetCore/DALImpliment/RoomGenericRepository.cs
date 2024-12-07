using BE_102024.DataAces.NetCore.DataOpject;
using BE_102024.DataAces.NetCore.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class RoomGenericRepository : GenericRepository<Room>
    {
        public RoomGenericRepository(BE_102024Context context) : base(context)
        {

        }
    }
}
