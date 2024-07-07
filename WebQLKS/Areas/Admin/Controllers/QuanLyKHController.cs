using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebQLKS.App_Start;
using WebQLKS.Models;

namespace WebQLKS.Areas.Admin.Controllers
{
    [AdminAuthorize(maCV = "QLKH")]
    public class QuanLyKHController : Controller
    {
        DAQLKSEntities db = new DAQLKSEntities();
        // GET: Admin/QuanLyKH
        public ActionResult DanhSachKH()
        {
            ViewBag.Current = "DanhSachKH";
            var khachhang = db.tbl_KhachHang.ToList();
            return View(khachhang);
        }
        public ActionResult ChiTietKH(string maKH)
        {
            var user = maKH;
            if (user == null)
            {
                return RedirectToAction("DanhSachKH", "QuanLyKH");
            }
            else
            {
                var khachhang = db.tbl_KhachHang.Where(s => s.MaKH == user).FirstOrDefault();
                return View(khachhang);
            }
        }
        public ActionResult XoaKH(string maKH)
        {
            return View(db.tbl_KhachHang.Where(s=>s.MaKH==maKH).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult XoaKH(string maKH, tbl_KhachHang kh)
        {
            try
            {
                kh = db.tbl_KhachHang.Where(s=>s.MaKH==maKH).FirstOrDefault();
                db.tbl_KhachHang.Remove(kh);
                db.SaveChanges();
                return RedirectToAction("DanhSachKH");
            }
            catch
            {
                return Content("Gặp lỗi! Không thể xóa khách hàng.");
            }
        }
    }
}