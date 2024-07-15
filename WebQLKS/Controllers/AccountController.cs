using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult ChiTietPhieuThue(string maPT)
        {
            var phieuThue = db.tbl_PhieuThuePhong.Where(a => a.MaPhieuThuePhong == maPT).FirstOrDefault();
            return View(phieuThue);
        }
        public ActionResult ChiTietDichVu(string maDV)
        {
            var chitiet = db.tbl_DichVu.Where(a => a.MaDV == maDV).FirstOrDefault();
            return View(chitiet);
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
        public ActionResult UserInfor()
        {
            var user = Session["KH"].ToString();
            if (user == null)
            {
                return RedirectToAction("LoginAcountKH", "LoginAcount");
            }
            else
            {
                var khachhang = db.tbl_KhachHang.Where(s => s.MaKH == user).FirstOrDefault();
                return View(khachhang);
            }
        }
        public ActionResult EditUser(string makh)
        {
            var user = db.tbl_KhachHang.Where(s => s.MaKH == makh).FirstOrDefault();
            ViewBag.khachhang = user;
            return View(db.tbl_KhachHang.Where(s => s.MaKH == makh).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditUser(string makh, tbl_KhachHang kh)
        {
            db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserInfor", "Account");
        }

        public ActionResult HuyDatPhong(string maPT)
        {
            if (ModelState.IsValid)
            {
                var phieuthue = db.tbl_PhieuThuePhong.Where(s => s.MaPhieuThuePhong == maPT).FirstOrDefault();
                if (phieuthue.TrangThai == "Chưa xác nhận")
                {
                    phieuthue.TrangThai = "Đã hủy";
                    db.Entry(phieuthue).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("historyOrdRoom", "Account");
            }
            return View();
        }
        public ActionResult HuyDichVu(string maDV)
        {
            if (ModelState.IsValid)
            {
                var phieudichvu = db.tbl_DichVuDaDat.Where(s => s.MaDV == maDV).FirstOrDefault();
                if (phieudichvu.MaTrangThaiDV == "TT01")
                {
                    phieudichvu.MaTrangThaiDV = "TT04";
                    db.Entry(phieudichvu).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("historyService", "Account");
            }
            return View();
        }
        public ActionResult XemHoaDon()
        {
            if (Session["KH"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var maKH = Session["KH"].ToString();
            var HD = db.tbl_HoaDon.Where(i => i.MaKH == maKH).ToList();
            return View(HD);
        }
        public ActionResult ChiTietHoaDon(string maHD)
        {
            var detailBill = db.tbl_HoaDon.Where(i => i.MaHD == maHD).FirstOrDefault();
            return View(detailBill);
        }
    }
}