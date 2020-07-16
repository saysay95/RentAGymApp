
using System.Collections.Generic;

namespace Shared
{
    public class User
    {
        // these properties map to columns in the database
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Space> Spaces { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}