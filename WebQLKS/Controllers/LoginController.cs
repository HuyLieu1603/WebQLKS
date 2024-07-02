using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class LoginController : Controller
    {
        DAQLKSEntities db = new DAQLKSEntities();
        // GET: Login
        public ActionResult LoginAcountKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccountKH(tbl_KhachHang kh)
        {
            var checkkh = db.tbl_KhachHang.Where(s=>s.TaiKhoan==kh.TaiKhoan&&s.MatKhau==kh.MatKhau).FirstOrDefault();
            
            if (checkkh ==null)
            {
                ViewBag.ErroInfo = "Sai tai khoan";
                return View("LoginAcountKH");
            }
            else 
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["TaiKhoan"]=kh.TaiKhoan;
                Session["MatKhau"]=kh.MatKhau;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LoginAcountNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccountNV(tbl_NhanVien nv)
        {
            
            var checknv = db.tbl_NhanVien.Where(s => s.TaiKhoan == nv.TaiKhoan && s.MatKhau == nv.MatKhau).FirstOrDefault();
            if (checknv == null)
            {
                ViewBag.ErroInfo = "Sai tai khoan";
                return View("LoginAcount");
            }
            else 
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["TaiKhoan"] = nv.TaiKhoan;
                Session["MatKhau"] = nv.MatKhau;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult RegisterKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterKH(tbl_KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                var checkTK = db.tbl_KhachHang.Where(s=>s.TaiKhoan==kh.TaiKhoan).FirstOrDefault();
                if (checkTK == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_KhachHang.Add(kh);
                    db.SaveChanges();
                    return RedirectToAction("LoginAcountKH", "Login");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tai khoan da co nguoi dang ky";
                    return View();
                }
            }
            return View();
        }
        public ActionResult RegisterNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNV(tbl_NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                var checkTK = db.tbl_NhanVien.Where(s => s.TaiKhoan == nv.TaiKhoan).FirstOrDefault();
                if (checkTK == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_NhanVien.Add(nv);
                    db.SaveChanges();
                    return RedirectToAction("LoginAcountNV", "Login");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tai khoan da co nguoi dang ky";
                    return View();
                }
            }
            return View();
        }
    }
}