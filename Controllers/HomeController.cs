using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MRoom.Models;
using System.Diagnostics;
using System.Security.Claims;
using MRoom.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly MRoomDbContext db;
        public HomeController(MRoomDbContext dbContext)
        {
            db = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.Slider = db.Sliders.AsNoTracking().OrderByDescending(x => x.Id).Take(8).ToList();
            ViewBag.Testimonial = db.Testimonials.AsNoTracking().OrderByDescending(x => x.Id).Take(8).ToList();
            ViewBag.CommercialShop = db.PropertyDetails.AsNoTracking().Where(x => x.PropertyVariantName == "SHOP").Take(8).ToList();
            ViewBag.BHK1 = db.PropertyDetails.AsNoTracking().Where(x => x.BHKTypeName == "1 BHK").Take(8).ToList();
            ViewBag.BHK2 = db.PropertyDetails.AsNoTracking().Where(x => x.BHKTypeName == "2 BHK").Take(8).ToList();
            ViewBag.BHK3 = db.PropertyDetails.AsNoTracking().Where(x => x.BHKTypeName == "3 BHK").Take(8).ToList();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TermsOfUse()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult PropertyList(string Name = "")
        {
            IQueryable<PropertyDetail> query = db.PropertyDetails.AsNoTracking();
            if (Name == "CommercialShop")
            {
                query = query.Where(x => x.PropertyVariantName == "SHOP");
            }
            else if (Name == "BHK1")
            {
                query = query.Where(x => x.BHKTypeName == "1 BHK");
            }
            else if (Name == "BHK2")
            {
                query = query.Where(x => x.BHKTypeName == "2 BHK");
            }
            else if (Name == "BHK3")
            {
                query = query.Where(x => x.BHKTypeName == "3 BHK");
            }
            List<PropertyDetail> result = query.ToList();
            ViewBag.City = new SelectList(db.CityMasters.AsNoTracking(), "Name", "Name");
            ViewBag.Colony = new SelectList(db.ColonyMuhallas.AsNoTracking(), "ColonyName", "ColonyName");
            ViewBag.Floor = new SelectList(db.FloorTypes.AsNoTracking(), "FloorTypeName", "FloorTypeName");
            return View(result);
        }

        public IActionResult PropertyListFilter(string Colony, string City, string Budget, string Floor)
        {
            IQueryable<PropertyDetail> query = db.PropertyDetails.AsNoTracking();
            if (!string.IsNullOrEmpty(Colony))
            {
                query = query.Where(x => x.ColonyName == Colony);
            }
            if (!string.IsNullOrEmpty(City))
            {
                query = query.Where(x => x.CityName == City);
            }
            if (!string.IsNullOrEmpty(Budget))
            {
                int budgetval = Convert.ToInt32(Budget);
                query = query.Where(x => Convert.ToInt32(x.MonthlyRent) >= budgetval);
            }
            if (!string.IsNullOrEmpty(Floor))
            {
                query = query.Where(x => x.FloorTypeName == Floor);
            }
            List<PropertyDetail> result = query.ToList();
            ViewBag.City = new SelectList(db.CityMasters.AsNoTracking(), "Name", "Name");
            ViewBag.Colony = new SelectList(db.ColonyMuhallas.AsNoTracking(), "ColonyName", "ColonyName");
            ViewBag.Floor = new SelectList(db.FloorTypes.AsNoTracking(), "FloorTypeName", "FloorTypeName");
            return View("PropertyList", result);
        }

        public IActionResult PropertyDetails(int Pid = 0)
        {
            PropertyDetail? detail = db.PropertyDetails.Find(Pid);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        public IActionResult Testimonials()
        {
            List<Testimonial> testimonials = db.Testimonials.AsNoTracking().ToList();
            return View(testimonials);
        }

        public IActionResult Tenats()
        {
            return View();
        }

        public IActionResult OwnerLandlords()
        {
            return View();
        }

        public IActionResult ListingWhatsapp()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password, string ReturnUrl)
        {
            UserLogin? myuser = await db.UserLogins.FirstOrDefaultAsync(x => x.Username == Username && x.Password == Password);
            if (myuser != null)
            {
                if (myuser.Role == "User" || myuser.Role == "Admin")
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, myuser.Username),
                        new Claim(ClaimTypes.Role, myuser.Role)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }

                    if (myuser.Role == "User")
                    {
                        return RedirectToAction("Index", "MyUser");
                    }
                    else if (myuser.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    TempData["datachange"] = "Acoount Not Active Contact Admin For Activate Your Account..";
                }
            }
            else
            {
                TempData["datachange"] = "Incorrect username or password.";
            }
            return RedirectToAction("Login", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
