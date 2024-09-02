using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRoom.Data;
using MRoom.Models;

namespace MRoom.Controllers
{
    public class LocationMasterController : Controller
    {
        private readonly MRoomDbContext db;

        public LocationMasterController(MRoomDbContext dbContext)
        {
            db = dbContext;
        }


        [NonAction]
        private void MyCountries()
        {
            ViewBag.LCountryName = new SelectList(db.CountryMasters.Where(x => x.Status == "Active").AsNoTracking().ToList(), "Id", "Name");
        }

        public JsonResult GetStates(int countryId)
        {
            List<StateMaster> states = db.StateMasters.Where(x => x.CountryId == countryId).ToList();
            return Json(states);
        }

        public JsonResult GetCities(int countryId, int stateId)
        {
            List<CityMaster> cities = db.CityMasters.Where(x => x.StateId == stateId && x.CountryId == countryId).ToList();
            return Json(cities);
        }

        // Country Crud Operations
        public IActionResult CountryMasterList()
        {
            List<CountryMaster> cities = db.CountryMasters.ToList();
            return View(cities);
        }

        public IActionResult CountryMasterCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CountryMasterCreate(CountryMaster country)
        {
            if (ModelState.IsValid)
            {
                db.CountryMasters.Add(country);
                db.SaveChanges();
                TempData["datachange"] = "Country is Successfully Saved.";
                return RedirectToAction("CountryMasterList");
            }
            else
            {
                TempData["datachange"] = "Country is Not Saved.";
            }
            return View(country);
        }

        public IActionResult CountryMasterEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CountryMaster? country = db.CountryMasters.Find(id);
            if (country == null)
            {
                return Content("Nothing Found");
            }
            return View(country);
        }

        [HttpPost]
        public IActionResult CountryMasterEdit(CountryMaster country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Country is Successfully Updated.";
                return RedirectToAction("CountryMasterList");
            }
            else
            {
                TempData["datachange"] = "Country is Not Updated.";
            }
            return View(country);
        }

        public IActionResult CountryMasterDelete(int id)
        {
            CountryMaster? country = db.CountryMasters.Find(id);
            if (country != null)
            {
                db.CountryMasters.Remove(country);
                db.SaveChanges();
                TempData["datachange"] = "Country Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("CountryMasterList");
        }

        // State Crud Operations
        public IActionResult StateMasterList()
        {
            List<StateMaster> states = (from st in db.StateMasters
                                        join ct in db.CountryMasters on st.CountryId equals ct.Id
                                        select new
                                        StateMaster
                                        {
                                            Id = st.Id,
                                            CountryId = st.CountryId,
                                            Name = st.Name,
                                            Status = st.Status,
                                            CountryName = ct.Name
                                        }).ToList();
            return View(states);
        }

        public IActionResult StateMasterCreate()
        {
            MyCountries();
            return View();
        }

        [HttpPost]
        public IActionResult StateMasterCreate(StateMaster state1)
        {
            if (ModelState.IsValid)
            {
                db.StateMasters.Add(state1);
                db.SaveChanges();
                TempData["datachange"] = "State is Successfully Saved.";
                return RedirectToAction("StateMasterList");
            }
            else
            {
                MyCountries();
                TempData["datachange"] = "State is Not Saved.";
            }
            return View(state1);
        }

        public IActionResult StateMasterEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            StateMaster? country = db.StateMasters.Find(id);
            if (country == null)
            {
                return Content("Nothing Found");
            }
            MyCountries();
            return View(country);
        }

        [HttpPost]
        public IActionResult StateMasterEdit(StateMaster state1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(state1).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "State is Successfully Updated.";
                return RedirectToAction("StateMasterList");
            }
            else
            {
                MyCountries();
                TempData["datachange"] = "State is Not Updated.";
            }
            return View(state1);
        }

        public IActionResult StateMasterDelete(int id)
        {
            StateMaster? country = db.StateMasters.Find(id);
            if (country != null)
            {
                db.StateMasters.Remove(country);
                db.SaveChanges();
                TempData["datachange"] = "State Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("StateMasterList");
        }

        // City Crud Operations
        public IActionResult CityMasterList()
        {
            List<CityMaster> cities = (from cy in db.CityMasters
                                       join st in db.StateMasters on cy.StateId equals st.Id
                                       join ct in db.CountryMasters on cy.CountryId equals ct.Id
                                       select new
                                       CityMaster
                                       {
                                           Id = cy.Id,
                                           StateId = cy.StateId,
                                           CountryId = cy.CountryId,
                                           Name = cy.Name,
                                           Status = cy.Status,
                                           StateName = st.Name,
                                           CountryName = ct.Name,
                                       }).ToList();
            return View(cities);
        }

        public IActionResult CityMasterCreate()
        {
            MyCountries();
            return View();
        }

        [HttpPost]
        public IActionResult CityMasterCreate(CityMaster city)
        {
            if (ModelState.IsValid)
            {
                db.CityMasters.Add(city);
                db.SaveChanges();
                TempData["datachange"] = "City is Successfully Saved.";
                return RedirectToAction("CityMasterList");
            }
            else
            {
                MyCountries();
                TempData["datachange"] = "City is Not Saved.";
            }
            return View(city);
        }

        public IActionResult CityMasterEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CityMaster? city = db.CityMasters.Find(id);
            if (city == null)
            {
                return Content("Nothing Found");
            }
            MyCountries();
            ViewBag.LStateName = new SelectList(db.StateMasters.Where(x => x.Status == "Active" && x.CountryId == city.CountryId).AsNoTracking().ToList(), "Id", "Name");
            return View(city);
        }

        [HttpPost]
        public IActionResult CityMasterEdit(CityMaster city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "City is Successfully Updated.";
                return RedirectToAction("CityMasterList");
            }
            else
            {
                MyCountries();
                ViewBag.LStateName = new SelectList(db.StateMasters.Where(x => x.Status == "Active" && x.CountryId == city.CountryId).AsNoTracking().ToList(), "Id", "Name");
                TempData["datachange"] = "City is Not Updated.";
            }
            return View(city);
        }

        public IActionResult CityMasterDelete(int id)
        {
            CityMaster? city = db.CityMasters.Find(id);
            if (city != null)
            {
                db.CityMasters.Remove(city);
                db.SaveChanges();
                TempData["datachange"] = "City Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("CityMasterList");
        }


        //Colony/Muhalla Crud
        public IActionResult ColonyMuhallaList()
        {
            List<ColonyMuhalla> muhallas = (from nb in db.ColonyMuhallas
                                            join st in db.StateMasters on nb.StateId equals st.Id
                                            join cy in db.CityMasters on nb.CityId equals cy.Id
                                            join ct in db.CountryMasters on nb.CountryId equals ct.Id
                                            select new ColonyMuhalla
                                            {
                                                Id = nb.Id,
                                                CountryId = nb.CountryId,
                                                StateId = nb.StateId,
                                                CityId = nb.CityId,
                                                ColonyName = nb.ColonyName,
                                                Status = nb.Status,
                                                CountryName = ct.Name,
                                                StateName = st.Name,
                                                CityName = cy.Name,
                                                PinCode = nb.PinCode,
                                                Zone = nb.Zone
                                            }).ToList();
            return View(muhallas);
        }

        public IActionResult ColonyMuhallaCreate()
        {
            MyCountries();
            return View();
        }

        [HttpPost]
        public IActionResult ColonyMuhallaCreate(ColonyMuhalla muhalla)
        {
            if (ModelState.IsValid)
            {
                db.ColonyMuhallas.Add(muhalla);
                db.SaveChanges();
                TempData["datachange"] = "Colony-Muhalla is Successfully Saved.";
                return RedirectToAction("ColonyMuhallaList");
            }
            else
            {
                MyCountries();
                TempData["datachange"] = "Colony-Muhalla is Not Saved.";
            }
            return View(muhalla);
        }

        public IActionResult ColonyMuhallaEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ColonyMuhalla? muhalla = db.ColonyMuhallas.Find(id);
            if (muhalla == null)
            {
                return Content("Nothing Found");
            }
            MyCountries();
            ViewBag.LStateName = new SelectList(db.StateMasters.Where(x => x.Status == "Active" && x.CountryId == muhalla.CountryId).AsNoTracking().ToList(), "Id", "Name");
            ViewBag.LCityName = new SelectList(db.CityMasters.Where(x => x.Status == "Active" && x.StateId == muhalla.StateId && x.CountryId == muhalla.CountryId).AsNoTracking().ToList(), "Id", "Name");
            return View(muhalla);
        }

        [HttpPost]
        public IActionResult ColonyMuhallaEdit(ColonyMuhalla muhalla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muhalla).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Colony-Muhalla Successfully Updated.";
                return RedirectToAction("ColonyMuhallaList");
            }
            else
            {
                MyCountries();
                ViewBag.LStateName = new SelectList(db.StateMasters.Where(x => x.Status == "Active" && x.CountryId == muhalla.CountryId).AsNoTracking().ToList(), "Id", "Name");
                ViewBag.LCityName = new SelectList(db.CityMasters.Where(x => x.Status == "Active" && x.StateId == muhalla.StateId && x.CountryId == muhalla.CountryId).AsNoTracking().ToList(), "Id", "Name");
                TempData["datachange"] = "Colony-Muhalla Not Updated.";
            }
            return View(muhalla);
        }

        public IActionResult ColonyMuhallaDelete(int id)
        {
            ColonyMuhalla? colony = db.ColonyMuhallas.Find(id);
            if (colony != null)
            {
                db.ColonyMuhallas.Remove(colony);
                db.SaveChanges();
                TempData["datachange"] = "Colony-Muhalla Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("ColonyMuhallaList");
        }

        //NearBy Crud
        public IActionResult NearByList()
        {
            List<NearBy> nearbies = (from nb in db.NearBies
                                     join st in db.StateMasters on nb.StateId equals st.Id
                                     join cy in db.CityMasters on nb.CityId equals cy.Id
                                     join ct in db.CountryMasters on nb.CountryId equals ct.Id
                                     select new NearBy
                                     {
                                         Id = nb.Id,
                                         CountryId = nb.CountryId,
                                         StateId = nb.StateId,
                                         CityId = nb.CityId,
                                         NearByName = nb.NearByName,
                                         Status = nb.Status,
                                         CountryName = ct.Name,
                                         StateName = st.Name,
                                         CityName = cy.Name
                                     }).ToList();
            return View(nearbies);
        }

        public IActionResult NearByCreate()
        {
            MyCountries();
            return View();
        }

        [HttpPost]
        public IActionResult NearByCreate(NearBy nearby)
        {
            if (ModelState.IsValid)
            {
                db.NearBies.Add(nearby);
                db.SaveChanges();
                TempData["datachange"] = "Near By is Successfully Saved.";
                return RedirectToAction("NearByList");
            }
            else
            {
                MyCountries();
                TempData["datachange"] = "Near By is Not Saved.";
            }
            return View(nearby);
        }

        public IActionResult NearByEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            NearBy? nearby = db.NearBies.Find(id);
            if (nearby == null)
            {
                return Content("Nothing Found");
            }
            MyCountries();
            ViewBag.LStateName = new SelectList(db.StateMasters.Where(x => x.Status == "Active" && x.CountryId == nearby.CountryId).AsNoTracking().ToList(), "Id", "Name");
            ViewBag.LCityName = new SelectList(db.CityMasters.Where(x => x.Status == "Active" && x.StateId == nearby.StateId && x.CountryId == nearby.CountryId).AsNoTracking().ToList(), "Id", "Name");
            return View(nearby);
        }

        [HttpPost]
        public IActionResult NearByEdit(NearBy nearby)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nearby).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Near By is Successfully Updated.";
                return RedirectToAction("NearByList");
            }
            else
            {
                MyCountries();
                ViewBag.LStateName = new SelectList(db.StateMasters.Where(x => x.Status == "Active" && x.CountryId == nearby.CountryId).AsNoTracking().ToList(), "Id", "Name");
                ViewBag.LCityName = new SelectList(db.CityMasters.Where(x => x.Status == "Active" && x.StateId == nearby.StateId && x.CountryId == nearby.CountryId).AsNoTracking().ToList(), "Id", "Name");
                TempData["datachange"] = "Near By is Not Updated.";
            }
            return View(nearby);
        }

        public IActionResult NearByDelete(int id)
        {
            NearBy? type = db.NearBies.Find(id);
            if (type != null)
            {
                db.NearBies.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "Near By Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("NearByList");
        }



    }
}
