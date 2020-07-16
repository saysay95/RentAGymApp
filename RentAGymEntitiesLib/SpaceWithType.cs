
using System.Collections.Generic;

namespace Shared
{
    public class SpaceWithType
    {
        // these properties map to columns in the database
        public int TypeId { get; set; }
        public int SpaceId { get; set; }
        public string Type { get; set; }
        public SpaceType TypeNavigation { get; set; }

        // public ICollection<Price> Prices { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}