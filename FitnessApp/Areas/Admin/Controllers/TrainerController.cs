using FitnessApp.Areas.Admin.ViewModels.Trainer;
using FitnessApp.DAL.Contexts;
using FitnessApp.Models;
using FitnessApp.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TrainerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetTrainerVM> trainerVMs = await _context.Trainers
                .Include(t => t.Position)
                .Select(p => new GetTrainerVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    PositionName = p.Position.Name,
                    Image = p.Image
                })
                .ToListAsync();

            return View(trainerVMs);
        }

        public async Task<IActionResult> Create()
        {
            CreateTrainerVM createTrainerVM = new CreateTrainerVM()
            {
                Positions = await _context.Positions.ToListAsync()
            };
            return View(createTrainerVM);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateTrainerVM createTrainerVM)
        {
            
            if (!ModelState.IsValid)
            {
                createTrainerVM.Positions = await _context.Positions.ToListAsync();
                return View(createTrainerVM);
            }

            bool result = await _context.Positions.AnyAsync(p => p.Id == createTrainerVM.PositionId);
            if (!result)
            {
                ModelState.AddModelError(nameof(createTrainerVM.PositionId), "Such position does not exists");
                return View(createTrainerVM);
            }
            Trainer trainer = new Trainer()
            {
                Name = createTrainerVM.Name,
                Description = createTrainerVM.Description,
                PositionId = createTrainerVM.PositionId,
                Image = await createTrainerVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };


            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Update(int id)
        {
            Trainer? trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            UpdateTrainerVM updateTrainerVM = new()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Image = trainer.Image,
                Description = trainer.Description,
                PositionId = trainer.PositionId,
                Positions = await _context.Positions.ToListAsync()
            };

            return View(updateTrainerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTrainerVM updateTrainerVM)
        {
            if (!ModelState.IsValid)
            {
                updateTrainerVM.Positions = await _context.Positions.ToListAsync();
                return View(updateTrainerVM);
            }


            Trainer? trainer = await _context.Trainers.FirstOrDefaultAsync(w => w.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }

            if (await _context.Trainers.AnyAsync(w => w.Name == updateTrainerVM.Name && w.Id != id))
            {
                ModelState.AddModelError("Name", "This trainer name is already taken by another trainer.");
                updateTrainerVM.Positions = await _context.Positions.ToListAsync();
                return View(updateTrainerVM);
            }

            trainer.Name = updateTrainerVM.Name;
            trainer.PositionId = updateTrainerVM.PositionId;

            if (updateTrainerVM.Photo != null)
            {
                trainer.Image = await updateTrainerVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Trainer? trainer = await _context.Trainers.FirstOrDefaultAsync(w => w.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }
            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
