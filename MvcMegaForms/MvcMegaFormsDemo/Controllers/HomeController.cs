using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcMegaFormsDemo.Models;

namespace MvcMegaFormsDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new ComplicatedForm();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ComplicatedForm model)
        {
            if (ModelState.IsValid)
            {
                TempData.Add("success", "Successfully submitted form!");
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
