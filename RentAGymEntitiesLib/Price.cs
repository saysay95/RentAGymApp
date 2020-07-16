

namespace Shared
{
    public class Price
    {
        public int TypeId { get; set; }
        public double Amount { get; set; }
        public string DurationUnit { get; set; }
        // public DurationUnit Duration { get; set; }
        public SpaceWithType Type { get; set; }
    }
}