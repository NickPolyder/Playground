using System.Net;
using System.Web;
using System.Web.Mvc;
using Playground.ASPNETMVC.Models;

namespace Playground.ASPNETMVC.Controllers
{
    public class ImagesController : Controller
    {
        private ImgContext db;
        public ImagesController()
        {
            db = new ImgContext();
        }

        // GET: Images
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        public ActionResult Show(int id)
        {
            var imageData = db.Images.Find(id)?.Img;
            return File(imageData, "image/jpg");
        }
        public ActionResult Create()
        {
            Image image = new Image();
            return View(image);
        }

        [HttpPost]
        public ActionResult Create(Image image, HttpPostedFileBase file1)
        {
            if (file1 != null)
            {
                image.Img = new byte[file1.ContentLength];
                file1.InputStream.Read(image.Img, 0, file1.ContentLength);
            }
            db.Images.Add(image);
            db.SaveChanges();
            return View(image);
        }
    }
}