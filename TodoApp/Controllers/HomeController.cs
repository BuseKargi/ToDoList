using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        ToDoAppDBEntities db = new ToDoAppDBEntities();
        public ActionResult Index()
        {
            TempData["userNumber"] = db.User.Count();
            TempData["customerNumber"] = db.User.Where(x=>x.StatusId==5).Count();

            User checkSession = Session["userLogin"] as User;
            if (checkSession != null)
            {
                var data = new TaskStatusList()
                {
                    Status = db.Status.Where(x => x.Id == checkSession.StatusId).ToList(),
                    Task = db.Task.ToList()
                };
                List<Task> data3 = db.Task.Where(x => x.UserId == checkSession.Id).ToList();
                return View(data);
            }
            return RedirectToAction("Index", "Login");
        }
    }
}