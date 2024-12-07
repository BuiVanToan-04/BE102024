using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid.Interface
{
    public interface IBookingCancellation
    {
        bool BookingCancellation(Booking booking);
    }
}
