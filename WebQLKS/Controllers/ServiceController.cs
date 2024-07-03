using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class ServiceController : Controller
    {
        DAQLKSEntities db = new DAQLKSEntities();
        // GET: Service
        public ActionResult Index()
        {
            var DV = db.tbl_LoaiDichVu.ToList();
            return View(DV);
        }

        public ActionResult detailService(string maDV)
        {
            var ctDV = db.tbl_DichVu.Find(maDV);
            return View(ctDV);
        }
    }
}