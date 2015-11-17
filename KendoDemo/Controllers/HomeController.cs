using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Demo()
        {
            ViewBag.slist = new List<SelectListItem>() {
              new SelectListItem() {
                Text = "纯棉", Value = "1"
              },
              new SelectListItem() {
                Text = "亚麻", Value = "2"
              },
              new SelectListItem() {
                Text = "丝绸", Value = "3"
              },
              new SelectListItem() {
                Text = "lanbu", Value = "4"
              }
          };
            return View();
        }
    }
}
