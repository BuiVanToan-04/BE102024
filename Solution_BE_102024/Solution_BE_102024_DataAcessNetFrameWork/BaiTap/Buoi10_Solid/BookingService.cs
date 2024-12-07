using Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution_BE_102024_DataAcessNetFrameWork.BaiTap.Buoi10_Solid
{
    public class BookingService : IBookingCreation, IBookingCancellation
    {
        private IRoomRepository _roomRepository;
        public BookingService(IRoomRepository roomRepository) 
        {
            _roomRepository = roomRepository;
        }

        public bool BookingCancellation(Booking booking)
        {
            try
            {
                var room = _roomRepository.GetByIDRoom(booking.RoomNumber);
                if (room != null)
                {
                    Console.WriteLine($"Khách hàng vẫn chưa đặt phòng: {booking.RoomNumber}");
                    return false;
                }
                _roomRepository.CheckAvailableRoom(booking.RoomNumber, true);
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool BookingCreation(Booking booking)
        {
            try
            {
                var room = _roomRepository.GetByIDRoom(booking.ID);
                if (room != null && !room.IsAvailable)
                {
                    return false;
                }
                _roomRepository.CheckAvailableRoom(booking.RoomNumber, false);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
