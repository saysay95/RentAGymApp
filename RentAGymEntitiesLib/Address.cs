using System.Collections.Generic;

namespace Shared
{
    public class Address
    {
        // these properties map to columns in the database
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Space> Spaces { get; set; }
        
    }
}