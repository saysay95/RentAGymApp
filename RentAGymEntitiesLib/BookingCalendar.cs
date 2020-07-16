

namespace Shared
{
    public class BookingCalendar
    {
        public int BookingId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public Booking Booking { get; set; }
    }
}