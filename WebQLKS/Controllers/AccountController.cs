﻿using System;
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
            return View(db.tbl_KhachHang.Where(s=>s.MaKH==makh).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditUser(string makh, tbl_KhachHang kh)
        {
            db.Entry(kh).State=System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserInfor", "Account");
        }
    }
}