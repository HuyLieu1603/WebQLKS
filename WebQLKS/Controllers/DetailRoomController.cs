using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class DetailRoomController : Controller
    {
        DAQLKSEntities1 db = new DAQLKSEntities1();
        // GET: DetailRoom
        public ActionResult Index()
        {
            var roomDetail = db.tbl_Phong.ToList();
            return View(roomDetail);
        }
    }
}