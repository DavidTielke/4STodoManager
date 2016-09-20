using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class TodoController : BaseController
    {
        private readonly ITodoManager _manager;
        private readonly ITodoRepository _repository;

        public TodoController(ITodoManager manager, ITodoRepository repository)
        {
            _manager = manager;
            _repository = repository;
        }

        public ActionResult Index(int page = 1)
        {
            var itemsPerPage = 5;
            var allUndone = _manager.GetAllUndone().Skip((page-1) * itemsPerPage).Take(itemsPerPage);
            ViewBag.PaginationInfo = new PaginationInfo
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = _manager.GetAllUndone().Count()
            };

            return View(allUndone);
        }

        public ActionResult Create()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Update(int id)
        {
            var model = _manager.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult DeleteConfirm(int id)
        {
            var model = _manager.GetById(id);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult TodayTasks()
        {
            var model = _manager.GetTodayTasks();
            return PartialView("TaskShortTable", model);
        }

        [ChildActionOnly]
        public ActionResult TomorrowTasks()
        {
            var model = _manager.GetTomorrowTasks();
            return PartialView("TaskShortTable", model);
        }
    }
}