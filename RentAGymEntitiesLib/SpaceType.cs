
using System.Collections.Generic;

namespace Shared
{
    public class SpaceType
    {
        // these properties map to columns in the database

        public string Type { get; set; }

        public string Label { get; set; }

        public ICollection<SpaceWithType> SpaceWithTypes { get; set; }
    }
}