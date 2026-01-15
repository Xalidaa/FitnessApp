using FitnessApp.Models;

namespace FitnessApp.Areas.Admin.ViewModels.Trainer
{
    public class CreateTrainerVM
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public int PositionId { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
