using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRoom.Data;
using MRoom.Models;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MRoom.Controllers
{
    public class ProMainController : Controller
    {
        private readonly MRoomDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProMainController(MRoomDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            db = dbContext;
            _hostingEnvironment = hostEnvironment;
        }
        public IActionResult ProAllList()
        {
            var result = db.PropertyDetails
                           .AsNoTracking()
                           .ToList();
            return View(result);
        }


        public IActionResult ProSaleList()
        {
            List<PropertyDetail> properties = db.PropertyDetails.Where(x => x.PropertyFor == "Sell").AsNoTracking().ToList();
            return View(properties);
        }

        public IActionResult ProRentList()
        {
            List<PropertyDetail> properties = db.PropertyDetails.Where(x => x.PropertyFor == "Rent").AsNoTracking().ToList();
            return View(properties);
        }

        public IActionResult PropertyCreate(string TypeFor = "")
        {
            PropertyDetail property = new PropertyDetail();
            ViewBag.LMyUsers = new SelectList(db.UserLogins.AsNoTracking().ToList(), "Id", "Username");
            ViewBag.LPropertyType = new SelectList(db.PropertyTypes.AsNoTracking().ToList(), "Id", "PropertyTypeName");
            ViewBag.LBHK = new SelectList(db.BHKTypes.Where(x => x.Status == "Active").AsNoTracking().ToList(), "BHKName", "BHKName");
            ViewBag.LToiletType = new SelectList(db.ToiletTypes.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LParkingType = new SelectList(db.ParkingTypes.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LParkingVisitors = new SelectList(db.ParkingVisitors.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LFloor = new SelectList(db.FloorTypes.Where(x => x.Status == "Active").AsNoTracking().ToList(), "FloorTypeName", "FloorTypeName");
            ViewBag.LFirstPriority = new SelectList(db.FirstPriorities.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LCountry = new SelectList(db.CountryMasters.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LReligion = new SelectList(db.Religions.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LFurnished = new SelectList(db.FurnishedTypes.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LWater = new SelectList(db.WaterSupplies.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LLpg = new SelectList(db.Lpgs.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LElectricity = new SelectList(db.Electricities.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LStair = new SelectList(db.Stairs.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LRoof = new SelectList(db.Roofs.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LCooking = new SelectList(db.CookingItems.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Name", "Name");
            ViewBag.LNearBies = new SelectList(db.NearBies.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Id", "NearByName");

            if (!string.IsNullOrEmpty(TypeFor))
            {
                property.PropertyFor = TypeFor;
            }
            return View(property);
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

        [HttpPost]
        public IActionResult PropertyCreate(PropertyDetail property, IFormFile imageInput1, IFormFile? imageInput2, IFormFile? imageInput3, IFormFile? imageInput4, IFormFile? imageInput5, IFormFile? imageInput6)
        {
            List<PD_Near> pD_Nears = [];
            if (ModelState.IsValid)
            {
                property.Image1 = SaveFile(imageInput1, "PropertyImages");
                property.Image2 = SaveFile(imageInput2, "PropertyImages");
                property.Image3 = SaveFile(imageInput3, "PropertyImages");
                property.Image4 = SaveFile(imageInput4, "PropertyImages");
                property.Image5 = SaveFile(imageInput5, "PropertyImages");
                property.Image6 = SaveFile(imageInput6, "PropertyImages");
                db.PropertyDetails.Add(property);
                //db.SaveChanges();
                //pD_Nears.AddRange(from item1 in property.NearBies
                //                  let d_Near = new PD_Near { NearById = item1, PropertyId = property.Id }
                //                  select d_Near);
                //db.PD_Nears.AddRange(pD_Nears);
                db.SaveChanges();
                TempData["datachange"] = "Property is Successfully Saved.";
                return RedirectToAction("ProAllList");
            }
            else
            {
                TempData["datachange"] = "Property Type is Not Saved.";
            }
            return View(property);
        }

        public JsonResult GetPropertySub(int main)
        {
            List<PropertyVariant> variants = db.PropertyVariants.Where(x => x.PropertyTypeId == main).ToList();
            return Json(variants);
        }

        public JsonResult GetStates(string country)
        {
            var states = db.StateMasters
                   .Where(st => db.CountryMasters.Any(ct => ct.Id == st.CountryId && ct.Name == country))
                   .Select(st => st.Name)
                   .ToList();
            return Json(states);
        }

        public JsonResult GetCities(string statename)
        {
            var cities = db.CityMasters
                   .Where(cty => db.StateMasters.Any(st => st.Id == cty.StateId && st.Name == statename))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(cities);
        }

        public JsonResult GetColony(string cityname)
        {
            var colonies = db.ColonyMuhallas
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.ColonyName)
                   .ToList();
            return Json(colonies);
        }

        public JsonResult GetNearBy(string cityname)
        {
            var nears = db.NearBies
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => new { cty.Id, cty.NearByName })
                   .ToList();
            return Json(nears);
        }

        public JsonResult GetRailway(string cityname)
        {
            var results = db.RailwayStations
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }

        public JsonResult GetBus(string cityname)
        {
            var results = db.BusStands
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetSchoolGov(string cityname)
        {
            var results = db.SchoolGovs
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetSchoolPvt(string cityname)
        {
            var results = db.SchoolPvts
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetHospitalGov(string cityname)
        {
            var results = db.HospitalGovs
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetHospitalPvt(string cityname)
        {
            var results = db.HospitalPvts
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetBankPvt(string cityname)
        {
            var results = db.BankPvts
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetBankGov(string cityname)
        {
            var results = db.BankGovs
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetMarket(string cityname)
        {
            var results = db.Markets
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetPublicTpt(string cityname)
        {
            var results = db.PublicTpts
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
        public JsonResult GetDmOffice(string cityname)
        {
            var results = db.DmOffices
                   .Where(cty => db.CityMasters.Any(st => st.Id == cty.CityId && st.Name == cityname))
                   .Select(cty => cty.Name)
                   .ToList();
            return Json(results);
        }
    }
}
