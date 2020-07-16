using System.Collections.Generic;

namespace Shared
{
    public class Booking
    {
        // these properties map to columns in the database
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public SpaceWithType Type { get; set; }
        public User User { get; set; }
        // public ICollection<BookingCalendar> BookingCalendar { get; set; }
        
    }
}