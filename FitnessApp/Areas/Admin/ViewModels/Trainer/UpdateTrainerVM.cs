using FitnessApp.Models;

namespace FitnessApp.Areas.Admin.ViewModels.Trainer
{
    public class UpdateTrainerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public int PositionId { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
