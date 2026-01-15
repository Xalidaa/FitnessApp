using FitnessApp.Models.Common;

namespace FitnessApp.Models
{
    public class Trainer:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
