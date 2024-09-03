using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRoom.Data;
using MRoom.Models;

namespace MRoom.Controllers
{
    public class AdminController : Controller
    {
        private readonly MRoomDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(MRoomDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            db = dbContext;
            _hostingEnvironment = hostEnvironment;
        }

        [NonAction]
        private string? SaveFile(IFormFile? file, string subfolder)
        {
            if (file != null && file.Length > 0)
            {
                string imgurl = "/" + subfolder + "/";
                string filenamec = DateTime.UtcNow.Ticks.ToString() + Path.GetExtension(file.FileName);
                string xyz = _hostingEnvironment.WebRootPath + imgurl + filenamec;
                Directory.CreateDirectory(_hostingEnvironment.WebRootPath + imgurl);

                using (var fileStream = new FileStream(xyz, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return imgurl + filenamec;
            }
            return null;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AcSliders()
        {
            List<Slider> sliders = db.Sliders.AsNoTracking().ToList();
            return View(sliders);
        }

        public IActionResult AcSlidersCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AcSlidersCreate(Slider slider, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile.Length > 0)
                {
                    slider.FilePath = SaveFile(formFile, "Sliders");
                }
                db.Sliders.Add(slider);
                db.SaveChanges();
                TempData["datachange"] = "Slider is Successfully Saved.";
                return RedirectToAction("AcSliders");
            }
            else
            {
                TempData["datachange"] = "Slider is Not Saved.";
            }
            return View(slider);
        }

        public IActionResult AcSlidersEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Slider? slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return Content("Nothing Found");
            }
            return View(slider);
        }

        [HttpPost]
        public IActionResult AcSlidersEdit(Slider slider, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile != null && formFile.Length > 0)
                {
                    slider.FilePath = SaveFile(formFile, "Sliders");
                }
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Slider is Successfully Updated.";
                return RedirectToAction("AcSliders");
            }
            else
            {
                TempData["datachange"] = "Slider is Not Updated.";
            }
            return View(slider);
        }

        public IActionResult AcSlidersDelete(int id)
        {
            Slider? slider = db.Sliders.Find(id);
            if (slider != null)
            {
                db.Sliders.Remove(slider);
                db.SaveChanges();
                TempData["datachange"] = "Slider Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("AcSliders");
        }

        public IActionResult AcTestimonials()
        {
            List<Testimonial> testimonials = db.Testimonials.AsNoTracking().ToList();
            return View(testimonials);
        }

        public IActionResult AcTestimonialsCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AcTestimonialsCreate(Testimonial testimonial, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile.Length > 0)
                {
                    testimonial.ImgPath = SaveFile(formFile, "Testimonials");
                }
                db.Testimonials.Add(testimonial);
                db.SaveChanges();
                TempData["datachange"] = "Testimonial is Successfully Saved.";
                return RedirectToAction("AcTestimonials");
            }
            else
            {
                TempData["datachange"] = "Testimonial is Not Saved.";
            }
            return View(testimonial);
        }

        public IActionResult AcTestimonialsEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Testimonial? testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return Content("Nothing Found");
            }
            return View(testimonial);
        }

        [HttpPost]
        public IActionResult AcTestimonialsEdit(Testimonial testimonial, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile != null && formFile.Length > 0)
                {
                    testimonial.ImgPath = SaveFile(formFile, "Testimonials");
                }
                db.Entry(testimonial).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Testimonial is Successfully Updated.";
                return RedirectToAction("AcTestimonials");
            }
            else
            {
                TempData["datachange"] = "Testimonial is Not Updated.";
            }
            return View(testimonial);
        }

        public IActionResult AcTestimonialsDelete(int id)
        {
            Testimonial? testimonial = db.Testimonials.Find(id);
            if (testimonial != null)
            {
                db.Testimonials.Remove(testimonial);
                db.SaveChanges();
                TempData["datachange"] = "Testimonial Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("AcTestimonials");
        }

        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Settings(string OldPassword, string NewPassword)
        {
            UserLogin? login = db.UserLogins.Where(ur => ur.Role == "Admin").FirstOrDefault();
            if (login != null && login.Password == OldPassword)
            {
                login.Password = NewPassword;
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Your Password has Sucessfully Change";
            }
            else if (String.IsNullOrEmpty(OldPassword))
            {
                TempData["datachange"] = "Please Enter Your Old Password";
            }
            else
            {
                TempData["datachange"] = "Incorrect Old Passsword";
            }
            return RedirectToAction("Settings");
        }
    }
}
