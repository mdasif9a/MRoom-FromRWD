using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRoom.Data;
using MRoom.Models;

namespace MRoom.Controllers
{
    public class ProMasterController : Controller
    {
        private readonly MRoomDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProMasterController(MRoomDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            db = dbContext;
            _hostingEnvironment = hostEnvironment;
        }

        // Property Type Crud Operations
        public IActionResult PropertyTypeList()
        {
            List<PropertyType> properties = db.PropertyTypes.ToList();
            return View(properties);
        }


        public IActionResult PropertyTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PropertyTypeCreate(PropertyType property)
        {
            if (ModelState.IsValid)
            {
                db.PropertyTypes.Add(property);
                db.SaveChanges();
                TempData["datachange"] = "Property Type is Successfully Saved.";
                return RedirectToAction("PropertyTypeList");
            }
            else
            {
                TempData["datachange"] = "Property Type is Not Saved.";
            }
            return View(property);
        }

        public IActionResult PropertyTypeEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PropertyType? property = db.PropertyTypes.Find(id);
            if (property == null)
            {
                return Content("Nothing Found");
            }
            return View(property);
        }

        [HttpPost]
        public IActionResult PropertyTypeEdit(PropertyType property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Property Type is Successfully Updated.";
                return RedirectToAction("PropertyTypeList");
            }
            else
            {
                TempData["datachange"] = "Property Type is Not Updated.";
            }
            return View(property);
        }

        public IActionResult PropertyTypeDelete(int id)
        {
            PropertyType? type = db.PropertyTypes.Find(id);
            if (type != null)
            {
                db.PropertyTypes.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "PropertyType Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("PropertyTypeList");
        }

        // Property Variant Crud Operations
        public IActionResult PropertyVariantList()
        {
            List<PropertyVariant> properties = (from pv in db.PropertyVariants
                                                join pt in db.PropertyTypes on pv.PropertyTypeId equals pt.Id
                                                select new PropertyVariant
                                                {
                                                    Id = pv.Id,
                                                    PropertyTypeId = pv.PropertyTypeId,
                                                    PropertyTypeName = pt.PropertyTypeName,
                                                    PropertyVariantName = pv.PropertyVariantName,
                                                    Status = pv.Status
                                                }).ToList();
            return View(properties);
        }


        public IActionResult PropertyVariantCreate()
        {
            ViewBag.LPropertyType = new SelectList(db.PropertyTypes
                .Where(x => x.Status == "Active")
                .AsNoTracking()
                .ToList(), "Id", "PropertyTypeName");
            return View();
        }

        [HttpPost]
        public IActionResult PropertyVariantCreate(PropertyVariant property)
        {
            if (ModelState.IsValid)
            {
                db.PropertyVariants.Add(property);
                db.SaveChanges();
                TempData["datachange"] = "Property Variant is Successfully Saved.";
                return RedirectToAction("PropertyVariantList");
            }
            else
            {
                ViewBag.LPropertyType = new SelectList(db.PropertyTypes
                .Where(x => x.Status == "Active")
                .AsNoTracking()
                .ToList(), "Id", "PropertyTypeName");
                TempData["datachange"] = "Property Variant is Not Saved.";
            }
            return View(property);
        }

        public IActionResult PropertyVariantEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PropertyVariant? property = db.PropertyVariants.Find(id);
            if (property == null)
            {
                return Content("Nothing Found");
            }
            ViewBag.LPropertyType = new SelectList(db.PropertyTypes
                .Where(x => x.Status == "Active")
                .AsNoTracking()
                .ToList(), "Id", "PropertyTypeName");
            return View(property);
        }

        [HttpPost]
        public IActionResult PropertyVariantEdit(PropertyVariant property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Property Variant is Successfully Updated.";
                return RedirectToAction("PropertyVariantList");
            }
            else
            {
                ViewBag.LPropertyType = new SelectList(db.PropertyTypes
                .Where(x => x.Status == "Active")
                .AsNoTracking()
                .ToList(), "Id", "PropertyTypeName");
                TempData["datachange"] = "Property Variant is Not Updated.";
            }
            return View(property);
        }

        public IActionResult PropertyVariantDelete(int id)
        {
            PropertyVariant? type = db.PropertyVariants.Find(id);
            if (type != null)
            {
                db.PropertyVariants.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "PropertyVariant Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("PropertyVariantList");
        }


        // BHK Type Crud Operations
        public IActionResult BHKTypeList()
        {
            List<BHKType> properties = db.BHKTypes.ToList();
            return View(properties);
        }


        public IActionResult BHKTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BHKTypeCreate(BHKType property)
        {
            if (ModelState.IsValid)
            {
                db.BHKTypes.Add(property);
                db.SaveChanges();
                TempData["datachange"] = "BHK Type is Successfully Saved.";
                return RedirectToAction("BHKTypeList");
            }
            else
            {
                TempData["datachange"] = "BHK Type is Not Saved.";
            }
            return View(property);
        }

        public IActionResult BHKTypeEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            BHKType? property = db.BHKTypes.Find(id);
            if (property == null)
            {
                return Content("Nothing Found");
            }
            return View(property);
        }

        [HttpPost]
        public IActionResult BHKTypeEdit(BHKType property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "BHK Type is Successfully Updated.";
                return RedirectToAction("BHKTypeList");
            }
            else
            {
                TempData["datachange"] = "BHK Type is Not Updated.";
            }
            return View(property);
        }

        public IActionResult BHKTypeDelete(int id)
        {
            BHKType? type = db.BHKTypes.Find(id);
            if (type != null)
            {
                db.BHKTypes.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "BHKType Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("BHKTypeList");
        }




        // Parking Type Crud Operations
        public IActionResult ParkingTypeList()
        {
            List<ParkingType> properties = db.ParkingTypes.ToList();
            return View(properties);
        }


        public IActionResult ParkingTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ParkingTypeCreate(ParkingType property)
        {
            if (ModelState.IsValid)
            {
                db.ParkingTypes.Add(property);
                db.SaveChanges();
                TempData["datachange"] = "Parking Type is Successfully Saved.";
                return RedirectToAction("ParkingTypeList");
            }
            else
            {
                TempData["datachange"] = "Parking Type is Not Saved.";
            }
            return View(property);
        }

        public IActionResult ParkingTypeEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ParkingType? property = db.ParkingTypes.Find(id);
            if (property == null)
            {
                return Content("Nothing Found");
            }
            return View(property);
        }

        [HttpPost]
        public IActionResult ParkingTypeEdit(ParkingType property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Parking Type is Successfully Updated.";
                return RedirectToAction("ParkingTypeList");
            }
            else
            {
                TempData["datachange"] = "Parking Type is Not Updated.";
            }
            return View(property);
        }

        public IActionResult ParkingTypeDelete(int id)
        {
            ParkingType? type = db.ParkingTypes.Find(id);
            if (type != null)
            {
                db.ParkingTypes.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "ParkingType Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("ParkingTypeList");
        }


        // Floor Type Crud Operations
        public IActionResult FloorTypeList()
        {
            List<FloorType> properties = db.FloorTypes.ToList();
            return View(properties);
        }


        public IActionResult FloorTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FloorTypeCreate(FloorType property)
        {
            if (ModelState.IsValid)
            {
                db.FloorTypes.Add(property);
                db.SaveChanges();
                TempData["datachange"] = "Floor Type is Successfully Saved.";
                return RedirectToAction("FloorTypeList");
            }
            else
            {
                TempData["datachange"] = "Floor Type is Not Saved.";
            }
            return View(property);
        }

        public IActionResult FloorTypeEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            FloorType? property = db.FloorTypes.Find(id);
            if (property == null)
            {
                return Content("Nothing Found");
            }
            return View(property);
        }

        [HttpPost]
        public IActionResult FloorTypeEdit(FloorType property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Floor Type is Successfully Updated.";
                return RedirectToAction("FloorTypeList");
            }
            else
            {
                TempData["datachange"] = "Floor Type is Not Updated.";
            }
            return View(property);
        }

        public IActionResult FloorTypeDelete(int id)
        {
            FloorType? type = db.FloorTypes.Find(id);
            if (type != null)
            {
                db.FloorTypes.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "FloorType Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("FloorTypeList");
        }

        // Crud For Furnished Item
        public IActionResult FurnishedItemList()
        {
            List<FurnishedType> furnishedItems = db.FurnishedTypes.ToList();
            return View(furnishedItems);
        }

        public IActionResult FurnishedItemCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FurnishedItemCreate(FurnishedType furnishedItem)
        {
            if (ModelState.IsValid)
            {
                db.FurnishedTypes.Add(furnishedItem);
                db.SaveChanges();
                TempData["datachange"] = "Furnished Item is Successfully Saved.";
                return RedirectToAction("FurnishedItemList");
            }
            else
            {
                TempData["datachange"] = "Furnished Item is Not Saved.";
            }
            return View(furnishedItem);
        }

        public IActionResult FurnishedItemEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            FurnishedType? furnishedItem = db.FurnishedTypes.Find(id);
            if (furnishedItem == null)
            {
                return NotFound();
            }
            return View(furnishedItem);
        }

        [HttpPost]
        public IActionResult FurnishedItemEdit(FurnishedType furnishedItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(furnishedItem).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Furnished Item is Successfully Updated.";
                return RedirectToAction("FurnishedItemList");
            }
            else
            {
                TempData["datachange"] = "Furnished Item is Not Updated.";
            }
            return View(furnishedItem);
        }

        public IActionResult FurnishedItemDelete(int id)
        {
            FurnishedType? furnishedItem = db.FurnishedTypes.Find(id);
            if (furnishedItem != null)
            {
                db.FurnishedTypes.Remove(furnishedItem);
                db.SaveChanges();
                TempData["datachange"] = "Furnished Item Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("FurnishedItemList");
        }


        //stair crud
        public IActionResult StairList()
        {
            List<Stair> stairs = db.Stairs.ToList();
            return View(stairs);
        }

        public IActionResult StairCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StairCreate(Stair stair)
        {
            if (ModelState.IsValid)
            {
                db.Stairs.Add(stair);
                db.SaveChanges();
                TempData["datachange"] = "Stair is Successfully Saved.";
                return RedirectToAction("StairList");
            }
            else
            {
                TempData["datachange"] = "Stair is Not Saved.";
            }
            return View(stair);
        }

        public IActionResult StairEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Stair? stair = db.Stairs.Find(id);
            if (stair == null)
            {
                return NotFound();
            }
            return View(stair);
        }

        [HttpPost]
        public IActionResult StairEdit(Stair stair)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stair).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Stair is Successfully Updated.";
                return RedirectToAction("StairList");
            }
            else
            {
                TempData["datachange"] = "Stair is Not Updated.";
            }
            return View(stair);
        }

        public IActionResult StairDelete(int id)
        {
            Stair? stair = db.Stairs.Find(id);
            if (stair != null)
            {
                db.Stairs.Remove(stair);
                db.SaveChanges();
                TempData["datachange"] = "Stair Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("StairList");
        }

        //Roof Crud
        public IActionResult RoofList()
        {
            List<Roof> roofs = db.Roofs.ToList();
            return View(roofs);
        }

        public IActionResult RoofCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RoofCreate(Roof roof)
        {
            if (ModelState.IsValid)
            {
                db.Roofs.Add(roof);
                db.SaveChanges();
                TempData["datachange"] = "Roof is Successfully Saved.";
                return RedirectToAction("RoofList");
            }
            else
            {
                TempData["datachange"] = "Roof is Not Saved.";
            }
            return View(roof);
        }

        public IActionResult RoofEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Roof? roof = db.Roofs.Find(id);
            if (roof == null)
            {
                return NotFound();
            }
            return View(roof);
        }

        [HttpPost]
        public IActionResult RoofEdit(Roof roof)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roof).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Roof is Successfully Updated.";
                return RedirectToAction("RoofList");
            }
            else
            {
                TempData["datachange"] = "Roof is Not Updated.";
            }
            return View(roof);
        }

        public IActionResult RoofDelete(int id)
        {
            Roof? roof = db.Roofs.Find(id);
            if (roof != null)
            {
                db.Roofs.Remove(roof);
                db.SaveChanges();
                TempData["datachange"] = "Roof Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("RoofList");
        }

        //first Priority crud
        public IActionResult FirstPriorityList()
        {
            List<FirstPriority> firstPriorities = db.FirstPriorities.ToList();
            return View(firstPriorities);
        }

        public IActionResult FirstPriorityCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FirstPriorityCreate(FirstPriority firstPriority)
        {
            if (ModelState.IsValid)
            {
                db.FirstPriorities.Add(firstPriority);
                db.SaveChanges();
                TempData["datachange"] = "First Priority is Successfully Saved.";
                return RedirectToAction("FirstPriorityList");
            }
            else
            {
                TempData["datachange"] = "First Priority is Not Saved.";
            }
            return View(firstPriority);
        }

        public IActionResult FirstPriorityEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            FirstPriority? firstPriority = db.FirstPriorities.Find(id);
            if (firstPriority == null)
            {
                return NotFound();
            }
            return View(firstPriority);
        }

        [HttpPost]
        public IActionResult FirstPriorityEdit(FirstPriority firstPriority)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firstPriority).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "First Priority is Successfully Updated.";
                return RedirectToAction("FirstPriorityList");
            }
            else
            {
                TempData["datachange"] = "First Priority is Not Updated.";
            }
            return View(firstPriority);
        }

        public IActionResult FirstPriorityDelete(int id)
        {
            FirstPriority? firstPriority = db.FirstPriorities.Find(id);
            if (firstPriority != null)
            {
                db.FirstPriorities.Remove(firstPriority);
                db.SaveChanges();
                TempData["datachange"] = "First Priority Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("FirstPriorityList");
        }

        //Religion Crud

        public IActionResult ReligionList()
        {
            List<Religion> religions = db.Religions.ToList();
            return View(religions);
        }

        public IActionResult ReligionCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReligionCreate(Religion religion)
        {
            if (ModelState.IsValid)
            {
                db.Religions.Add(religion);
                db.SaveChanges();
                TempData["datachange"] = "Religion is Successfully Saved.";
                return RedirectToAction("ReligionList");
            }
            else
            {
                TempData["datachange"] = "Religion is Not Saved.";
            }
            return View(religion);
        }

        public IActionResult ReligionEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Religion? religion = db.Religions.Find(id);
            if (religion == null)
            {
                return NotFound();
            }
            return View(religion);
        }

        [HttpPost]
        public IActionResult ReligionEdit(Religion religion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(religion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Religion is Successfully Updated.";
                return RedirectToAction("ReligionList");
            }
            else
            {
                TempData["datachange"] = "Religion is Not Updated.";
            }
            return View(religion);
        }

        public IActionResult ReligionDelete(int id)
        {
            Religion? religion = db.Religions.Find(id);
            if (religion != null)
            {
                db.Religions.Remove(religion);
                db.SaveChanges();
                TempData["datachange"] = "Religion Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("ReligionList");
        }

        //Cooking Items Crud

        public IActionResult CookingItemList()
        {
            List<CookingItem> cookingItems = db.CookingItems.ToList();
            return View(cookingItems);
        }

        public IActionResult CookingItemCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CookingItemCreate(CookingItem cookingItem)
        {
            if (ModelState.IsValid)
            {
                db.CookingItems.Add(cookingItem);
                db.SaveChanges();
                TempData["datachange"] = "Cooking Item is Successfully Saved.";
                return RedirectToAction("CookingItemList");
            }
            else
            {
                TempData["datachange"] = "Cooking Item is Not Saved.";
            }
            return View(cookingItem);
        }

        public IActionResult CookingItemEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CookingItem? cookingItem = db.CookingItems.Find(id);
            if (cookingItem == null)
            {
                return NotFound();
            }
            return View(cookingItem);
        }

        [HttpPost]
        public IActionResult CookingItemEdit(CookingItem cookingItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cookingItem).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Cooking Item is Successfully Updated.";
                return RedirectToAction("CookingItemList");
            }
            else
            {
                TempData["datachange"] = "Cooking Item is Not Updated.";
            }
            return View(cookingItem);
        }

        public IActionResult CookingItemDelete(int id)
        {
            CookingItem? cookingItem = db.CookingItems.Find(id);
            if (cookingItem != null)
            {
                db.CookingItems.Remove(cookingItem);
                db.SaveChanges();
                TempData["datachange"] = "Cooking Item Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("CookingItemList");
        }

        //Water Supply Crud
        public IActionResult WaterSupplyList()
        {
            List<WaterSupply> waterSupplies = db.WaterSupplies.ToList();
            return View(waterSupplies);
        }

        public IActionResult WaterSupplyCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WaterSupplyCreate(WaterSupply waterSupply)
        {
            if (ModelState.IsValid)
            {
                db.WaterSupplies.Add(waterSupply);
                db.SaveChanges();
                TempData["datachange"] = "Water Supply is Successfully Saved.";
                return RedirectToAction("WaterSupplyList");
            }
            else
            {
                TempData["datachange"] = "Water Supply is Not Saved.";
            }
            return View(waterSupply);
        }

        public IActionResult WaterSupplyEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            WaterSupply? waterSupply = db.WaterSupplies.Find(id);
            if (waterSupply == null)
            {
                return NotFound();
            }
            return View(waterSupply);
        }

        [HttpPost]
        public IActionResult WaterSupplyEdit(WaterSupply waterSupply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waterSupply).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Water Supply is Successfully Updated.";
                return RedirectToAction("WaterSupplyList");
            }
            else
            {
                TempData["datachange"] = "Water Supply is Not Updated.";
            }
            return View(waterSupply);
        }

        public IActionResult WaterSupplyDelete(int id)
        {
            WaterSupply? waterSupply = db.WaterSupplies.Find(id);
            if (waterSupply != null)
            {
                db.WaterSupplies.Remove(waterSupply);
                db.SaveChanges();
                TempData["datachange"] = "Water Supply Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("WaterSupplyList");
        }

        //Lpgs Crud
        public IActionResult LpgList()
        {
            List<Lpg> lpgs = db.Lpgs.ToList();
            return View(lpgs);
        }

        public IActionResult LpgCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LpgCreate(Lpg lpg)
        {
            if (ModelState.IsValid)
            {
                db.Lpgs.Add(lpg);
                db.SaveChanges();
                TempData["datachange"] = "LPG is Successfully Saved.";
                return RedirectToAction("LpgList");
            }
            else
            {
                TempData["datachange"] = "LPG is Not Saved.";
            }
            return View(lpg);
        }

        public IActionResult LpgEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lpg? lpg = db.Lpgs.Find(id);
            if (lpg == null)
            {
                return NotFound();
            }
            return View(lpg);
        }

        [HttpPost]
        public IActionResult LpgEdit(Lpg lpg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lpg).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "LPG is Successfully Updated.";
                return RedirectToAction("LpgList");
            }
            else
            {
                TempData["datachange"] = "LPG is Not Updated.";
            }
            return View(lpg);
        }

        public IActionResult LpgDelete(int id)
        {
            Lpg? lpg = db.Lpgs.Find(id);
            if (lpg != null)
            {
                db.Lpgs.Remove(lpg);
                db.SaveChanges();
                TempData["datachange"] = "LPG Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("LpgList");
        }

        //Electricy Crud

        public IActionResult ElectricityList()
        {
            List<Electricity> electricities = db.Electricities.ToList();
            return View(electricities);
        }

        public IActionResult ElectricityCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ElectricityCreate(Electricity electricity)
        {
            if (ModelState.IsValid)
            {
                db.Electricities.Add(electricity);
                db.SaveChanges();
                TempData["datachange"] = "Electricity is Successfully Saved.";
                return RedirectToAction("ElectricityList");
            }
            else
            {
                TempData["datachange"] = "Electricity is Not Saved.";
            }
            return View(electricity);
        }

        public IActionResult ElectricityEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Electricity? electricity = db.Electricities.Find(id);
            if (electricity == null)
            {
                return NotFound();
            }
            return View(electricity);
        }

        [HttpPost]
        public IActionResult ElectricityEdit(Electricity electricity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(electricity).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "Electricity is Successfully Updated.";
                return RedirectToAction("ElectricityList");
            }
            else
            {
                TempData["datachange"] = "Electricity is Not Updated.";
            }
            return View(electricity);
        }

        public IActionResult ElectricityDelete(int id)
        {
            Electricity? electricity = db.Electricities.Find(id);
            if (electricity != null)
            {
                db.Electricities.Remove(electricity);
                db.SaveChanges();
                TempData["datachange"] = "Electricity Deleted.";
            }
            else
            {
                TempData["datachange"] = "Data Not Deleted.";
            }
            return RedirectToAction("ElectricityList");
        }

        // Parking Visitor Type Crud Operations
        public IActionResult ParkingVisitorList()
        {
            List<ParkingVisitor> parkingVisitors = db.ParkingVisitors.ToList();
            return View(parkingVisitors);
        }


        public IActionResult ParkingVisitorCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ParkingVisitorCreate(ParkingVisitor parking)
        {
            if (ModelState.IsValid)
            {
                db.ParkingVisitors.Add(parking);
                db.SaveChanges();
                TempData["datachange"] = "ParkingVisitor is Successfully Saved.";
                return RedirectToAction("ParkingVisitorList");
            }
            else
            {
                TempData["datachange"] = "ParkingVisitor is Not Saved.";
            }
            return View(parking);
        }

        public IActionResult ParkingVisitorEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ParkingVisitor? parking = db.ParkingVisitors.Find(id);
            if (parking == null)
            {
                return Content("Nothing Found");
            }
            return View(parking);
        }

        [HttpPost]
        public IActionResult ParkingVisitorEdit(ParkingVisitor parking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parking).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "ParkingVisitor is Successfully Updated.";
                return RedirectToAction("ParkingVisitorList");
            }
            else
            {
                TempData["datachange"] = "ParkingVisitor is Not Updated.";
            }
            return View(parking);
        }

        public IActionResult ParkingVisitorDelete(int id)
        {
            ParkingVisitor? type = db.ParkingVisitors.Find(id);
            if (type != null)
            {
                db.ParkingVisitors.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "ParkingVisitor Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("ParkingVisitorList");
        }


        // Toilet Type Crud Operations
        public IActionResult ToiletTypeList()
        {
            List<ToiletType> toiletTypes = db.ToiletTypes.ToList();
            return View(toiletTypes);
        }


        public IActionResult ToiletTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ToiletTypeCreate(ToiletType toilet)
        {
            if (ModelState.IsValid)
            {
                db.ToiletTypes.Add(toilet);
                db.SaveChanges();
                TempData["datachange"] = "ToiletType is Successfully Saved.";
                return RedirectToAction("ToiletTypeList");
            }
            else
            {
                TempData["datachange"] = "ToiletType is Not Saved.";
            }
            return View(toilet);
        }

        public IActionResult ToiletTypeEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ToiletType? toilet = db.ToiletTypes.Find(id);
            if (toilet == null)
            {
                return Content("Nothing Found");
            }
            return View(toilet);
        }

        [HttpPost]
        public IActionResult ToiletTypeEdit(ToiletType toilet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toilet).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "ToiletType is Successfully Updated.";
                return RedirectToAction("ToiletTypeList");
            }
            else
            {
                TempData["datachange"] = "ToiletType is Not Updated.";
            }
            return View(toilet);
        }

        public IActionResult ToiletTypeDelete(int id)
        {
            ToiletType? toilet = db.ToiletTypes.Find(id);
            if (toilet != null)
            {
                db.ToiletTypes.Remove(toilet);
                db.SaveChanges();
                TempData["datachange"] = "ToiletType Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("ToiletTypeList");
        }


        // Elevator Type Crud Operations
        public IActionResult ElevatorTypeList()
        {
            List<ElevatorType> elevatorTypes = db.ElevatorTypes.ToList();
            return View(elevatorTypes);
        }


        public IActionResult ElevatorTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ElevatorTypeCreate(ElevatorType elevator)
        {
            if (ModelState.IsValid)
            {
                db.ElevatorTypes.Add(elevator);
                db.SaveChanges();
                TempData["datachange"] = "ElevatorType is Successfully Saved.";
                return RedirectToAction("ElevatorTypeList");
            }
            else
            {
                TempData["datachange"] = "ElevatorType is Not Saved.";
            }
            return View(elevator);
        }

        public IActionResult ElevatorTypeEdit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ElevatorType? elevator = db.ElevatorTypes.Find(id);
            if (elevator == null)
            {
                return Content("Nothing Found");
            }
            return View(elevator);
        }

        [HttpPost]
        public IActionResult ElevatorTypeEdit(ElevatorType elevator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elevator).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "ElevatorType is Successfully Updated.";
                return RedirectToAction("ElevatorTypeList");
            }
            else
            {
                TempData["datachange"] = "ElevatorType is Not Updated.";
            }
            return View(elevator);
        }

        public IActionResult ElevatorTypeDelete(int id)
        {
            ElevatorType? elevator = db.ElevatorTypes.Find(id);
            if (elevator != null)
            {
                db.ElevatorTypes.Remove(elevator);
                db.SaveChanges();
                TempData["datachange"] = "ElevatorType Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("ElevatorTypeList");
        }
    }
}
