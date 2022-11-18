using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class LoginController : Controller
    {
        ToDoAppDBEntities db = new ToDoAppDBEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User model)
        {
            User checkUser = db.User
                .SingleOrDefault(x => x.Username == model.Username
                && x.Password == model.Password);

            if (checkUser != null)
            {
                //tum kullanicilarin datasini  sessiona atadik
                Session["userLogin"] = checkUser;
            
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }

        public ActionResult LogOut()
        {
            User checkSession = Session["userLogin"] as User;
            if (checkSession != null)
            {
                Session.Clear();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            User checkUser = db.User.SingleOrDefault(x => x.Username == model.Username);
            if (checkUser!=null)
            {
                ViewBag.UsernameError = checkUser.Username + " Username Is Already Used";
                return View(model);
            }
            model.StatusId = 5;
            db.User.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}