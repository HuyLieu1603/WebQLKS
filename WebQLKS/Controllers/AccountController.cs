using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class AccountController : Controller
    {
        DAQLKSEntities db = new DAQLKSEntities();
        // GET: Account
        public ActionResult historyService()
        {
            var maKH = Session["KH"].ToString();
            var service = db.tbl_DichVuDaDat.Where(s => s.MaKH == maKH).ToList();
            if (service == null)
            {
                ViewBag.Notification = "Quý khách chưa sử dụng dịch vụ nào";
                return RedirectToAction("historyService", "Account");
            }
            return View(service);
        }
        public ActionResult historyOrdRoom()
        {
            var maKH = Session["KH"].ToString();
            var HD = db.tbl_PhieuThuePhong.Where(hd => hd.MaKH == maKH).ToList();
            if (HD == null)
            {
                ViewBag.Notification = "Quý khách chưa đặt phòng nào";
                return RedirectToAction("historyOrdRoom", "Account");
            }
            return View(HD);
        }
        public ActionResult Bill()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}