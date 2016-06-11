using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENEStock.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public string TreeMenu()
        {
            var node = new ENEStock.Common.Format.TreeNode();
            node.Checked = true;
            node.text = "实时ENE";
            node.id = 1;
            node.attributes = new { href = "/data/Index" };
            return "[" + ENEStock.Common.JsonHelper.SerializeObject(node) + "]";
        }

    }
}
