using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectNET_ASP.Data;
using projectNET_ASP.Models;

namespace projectNET_ASP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EmployeeController(EmployeeContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var em = await _context.Employee.ToListAsync();
            return View(em);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var em = await _context.Employee.FirstOrDefaultAsync(c => c.Id == id);
            if (em == null) return NotFound();

            _context.Employee.Remove(em);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee em)
        {
            var e = new Employee
            {
                Name = em.Name,
                Address = em.Address,
                Age = em.Age,
                Salary = em.Salary,
            };
            _context.Employee.Add(e);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
