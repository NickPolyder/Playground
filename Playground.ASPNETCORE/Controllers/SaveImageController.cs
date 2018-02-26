using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Playground.ASPNETCORE.Controllers
{
    public class SaveImageController : Controller
    {

        private IHostingEnvironment _env;

        public SaveImageController(IHostingEnvironment env)
        {
            _env = env;
        }
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Employee());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var root = _env.WebRootPath;
                var uploads = "uploads\\img";
                var uploadDirectory = Path.Combine(root, uploads);
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image != null && image.Length > 0)
                    {
                        // you can change the Guid.NewGuid().ToString().Replace("-", "")
                        // to Guid.NewGuid().ToString("N") it will produce the same result
                        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);

                        using (var fileStream = new FileStream(Path.Combine(uploadDirectory, fileName), FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                            // This will produce uploads\img\fileName.ext
                            emp.ImageUrl = Path.Combine(uploads, fileName);
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine(emp);
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(emp);
        }
    }
    [DebuggerDisplay("{Id} {Name} {ImageUrl}")]
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }


        public override string ToString()
        {
            return $"Employee: {Id}: {Name} ,{ImageUrl}";
        }
    }
}