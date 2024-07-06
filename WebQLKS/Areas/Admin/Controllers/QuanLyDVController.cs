using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Areas.Admin.Controllers
{
    public class QuanLyDVController : Controller
    {
        DAQLKSEntities db = new DAQLKSEntities();
        // GET: QuanLyDV
        public ActionResult DanhSachPhong()
        {
            var room = db.tbl_Phong.ToList();
            return View(room);
        }
        //Thêm Phòng
        [HttpGet]
        public ActionResult ThemPhong()
        {

            return View();
        }
       /* [HttpPost]
        public ActionResult ThemPhong()
        {

            return View(); 
        }*/

        public ActionResult DanhMucPhong()
        {
            var roomType = db.tbl_LoaiPhong.ToList();
            return View(roomType);
        }

        public ActionResult ThucDon()
        {
            var menu = db.tbl_DichVu.Where(m => m.MaLoaiDV == "DV02").ToList();
            return View(menu);
        }
        public ActionResult Spa()
        {
            var spa = db.tbl_DichVu.Where(s => s.MaLoaiDV == "DV03").ToList();
            return View(spa);
        }
        public ActionResult YeuCauDonDep()
        {
            var lstRequest = db.tbl_Phong.Where(r => r.MaTrangThai == "TT03").ToList();
            return View(lstRequest);
        }
    }
}