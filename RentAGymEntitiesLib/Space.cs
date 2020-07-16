
using System.Collections.Generic;

namespace Shared
{
    public class Space
    {
        public int SpaceId { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public bool HasSpaceToRent { get; set; }
        public Address Address { get; set; }
        public User Owner { get; set; }

        // public ICollection<User> Users { get; set; } 
    }
}
