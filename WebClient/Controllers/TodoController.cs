using System;
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

        public TodoController(ITodoManager manager)
        {
            _manager = manager;
        }

        public ActionResult Index()
        {
            var allUndone = _manager.GetAllUndone();

            return View(allUndone);
        }
    }
}