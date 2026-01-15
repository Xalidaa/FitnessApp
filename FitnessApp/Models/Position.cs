using FitnessApp.Models.Common;

namespace FitnessApp.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
