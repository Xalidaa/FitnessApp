
using FitnessApp.DAL.Contexts;
using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _context.Positions.Include(p=> p.Trainers).ToListAsync();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Positions.AnyAsync(p => p.Name == position.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Such position already exists");
                return View();
            }

            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }

            Position position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);

        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, Position position)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }

            Position exists = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (exists == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Positions.AnyAsync(p=>p.Name == position.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Such position already exists");
                return View();
            }
            exists.Name = position.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id == null || id < 1)
            {
                return BadRequest();
            }

            Position position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if(position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
