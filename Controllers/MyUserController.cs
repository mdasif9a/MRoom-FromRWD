using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRoom.Data;
using MRoom.Models;

namespace MRoom.Controllers
{
    public class MyUserController : Controller
    {
        private readonly MRoomDbContext db;
        public MyUserController(MRoomDbContext dbContext)
        {
            db = dbContext;
        }


        // Users Login Crud Operations
        public IActionResult Index()
        {
            List<UserLogin> users = db.UserLogins.ToList();
            return View(users);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserLogin myuser)
        {
            if (ModelState.IsValid)
            {
                db.UserLogins.Add(myuser);
                db.SaveChanges();
                TempData["datachange"] = "User is Successfully Saved.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["datachange"] = "User is Not Saved.";
            }
            return View(myuser);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            UserLogin? myuser = db.UserLogins.Find(id);
            if (myuser == null)
            {
                return Content("Nothing Found");
            }
            return View(myuser);
        }

        [HttpPost]
        public IActionResult Edit(UserLogin myuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myuser).State = EntityState.Modified;
                db.SaveChanges();
                TempData["datachange"] = "User is Successfully Updated.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["datachange"] = "User is Not Updated.";
            }
            return View(myuser);
        }

        public IActionResult Delete(int id)
        {
            UserLogin? type = db.UserLogins.Find(id);
            if (type != null)
            {
                db.UserLogins.Remove(type);
                db.SaveChanges();
                TempData["datachange"] = "UserLogin Delete.";
            }
            else
            {
                TempData["datachange"] = "Data Not Delete.";
            }
            return RedirectToAction("Index");
        }
    }
}
