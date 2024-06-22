using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class RoomController : Controller
    {
        DAQLKSEntities1 database = new DAQLKSEntities1();
        // GET: Room
        public ActionResult Index()
        {
            var room = database.tbl_Phong.ToList();
            return View(room);
        }


    }
}