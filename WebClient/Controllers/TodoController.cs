﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoManager _manager;
        private readonly ITodoRepository _repository;

        public TodoController(ITodoManager manager, ITodoRepository repository)
        {
            _manager = manager;
            _repository = repository;
        }

        public ActionResult Index()
        {
            var allUndone = _manager.GetAllUndone();

            return View(allUndone);
        }
        
        public ActionResult Create()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        public ActionResult Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(item);
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
    }
}