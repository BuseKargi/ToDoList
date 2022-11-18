using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TaskController : Controller
    {
        ToDoAppDBEntities db = new ToDoAppDBEntities();
        public ActionResult Index()
        {
            User checkSession = Session["userLogin"] as User;
            if (checkSession != null)
            {
                var data = new TaskStatusList()
                {
                    Status = db.Status.Where(x => x.StatusTypeId == 2).ToList(),
                    Task = db.Task.Where(x => x.UserId == checkSession.Id).ToList()
                };
                List<Task> data3 = db.Task.Where(x => x.UserId == checkSession.Id).ToList();
                return View(data);
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult Create()
        {
            User checkSession = Session["userLogin"] as User;
            if (checkSession != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public JsonResult Create(Task model)
        {
            User checkSession = Session["userLogin"] as User;
            model.CreatedDate = DateTime.Now;
            model.StatusId = 1;
            model.UserId = checkSession.Id;
            db.Task.Add(model);
            db.SaveChanges();
            return Json(new { Success = true, message = "Your task added successfully." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            Task data = db.Task.SingleOrDefault(x => x.Id == id);
            if (id.HasValue)
            {
                ViewBag.StatusId = new SelectList(db.Status.Where(x => x.StatusTypeId == 2), "Id", "Name", data.StatusId);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Task model)
        {
            Task data = db.Task.FirstOrDefault(x => x.Id == model.Id);
            ViewBag.StatusId = new SelectList(db.Status.Where(x => x.StatusTypeId == 2), "Id", "Name", data.StatusId);
            if (data != null)
            {
                data.Title = model.Title;
                data.Description = model.Description;
                data.StatusId = model.StatusId;
                db.SaveChanges();
                TempData["toast"]= Json(new { Success = true, message = "Your task added successfully." }, JsonRequestBehavior.AllowGet);
                RedirectToAction("Index");
            }
            return Json(new { Success = false, message = "Data not found" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Task data = db.Task.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    db.Task.Remove(data);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}