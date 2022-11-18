using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class UserController : Controller
    {

        ToDoAppDBEntities db = new ToDoAppDBEntities();
        // GET: User
        public ActionResult Index()
        {

            User g = Session["userLogin"] as User;
            if (g != null)
            {
                User data = db.User.SingleOrDefault(x => x.Id == g.Id);

                return View(data);
            }
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult Index(User model)
        {

            User data = db.User.SingleOrDefault(x => x.Id == model.Id);
            if (data != null)
            {
                data.Fullname = model.Fullname;
                data.Username = model.Username;
                data.Password = model.Password;
                db.SaveChanges();
                Session["userLogin"] = data;
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                User data = db.User.SingleOrDefault(x => x.Id == id);
                if (data != null)
                {
                    return View(data);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            User data = db.User.SingleOrDefault(x => x.Id == model.Id);
            if (data != null)
            {
                data.Fullname = model.Fullname;
                data.Username = model.Username;
                data.Password = model.Password;
                db.SaveChanges();
                Session["userLogin"] = data;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            User data = db.User.SingleOrDefault(x => x.Id == id);
            if (data != null)
            {
                db.User.Remove(data);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Login");

        }

    }
}