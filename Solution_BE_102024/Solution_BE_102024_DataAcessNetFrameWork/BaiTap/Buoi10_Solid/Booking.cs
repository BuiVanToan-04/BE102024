using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid
{
    public class Booking
    {
        public static int AutoBookingID = 1000;

        public int BookingId { get; set; }

        public string CustomerName { get; set; }

        public int RoomNumber { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int ID
        {
            get => BookingId;
            set
            {
                if (value < 1000)
                {
                    BookingId = AutoBookingID++;
                }
                else
                {
                    BookingId = value;
                }
            }
        }

        public Booking() { }

        public Booking(int id,string customerName, int roomNumber, DateTime checkInDate, DateTime checkOutDate)
        {
            ID = id;
            CustomerName = customerName;
            RoomNumber = roomNumber;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}
