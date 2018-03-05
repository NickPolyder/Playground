using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playground.ASPNETCORE.Models;

namespace Playground.ASPNETCORE.Controllers
{
    public class DepartmentController : Controller
    {
        private MyDbContext _context;

        public DepartmentController(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Get_Department_Section(string companyName)// To select Departments/sections for 'assign shifts'
        {

            var depSec = _context.Departments.Where(tt => tt.Name == companyName).Join(_context.Sections, d => d.Id, s => s.DepartmentId, (d, s) =>
                  new DepSecViewModel { department = d, section = s }).OrderBy(i => i.department.Name).ThenBy(i => i.section.Name);
            return Json(await depSec.ToListAsync());
        }
    }

    public class DepSecViewModel
    {
        public Department department { get; set; }
        public Section section { get; set; }

    }


}