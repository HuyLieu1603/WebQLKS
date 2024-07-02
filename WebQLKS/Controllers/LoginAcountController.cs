using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class LoginAcountController : Controller
    {
        DAQLKSEntities2 db = new DAQLKSEntities2();
        // GET: Login
        
        public ActionResult LoginAcountKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAcountKH(tbl_KhachHang kh)
        {
            var checkkh = db.tbl_KhachHang.Where(s => s.TaiKhoan == kh.TaiKhoan && s.MatKhau == kh.MatKhau).FirstOrDefault();

            if (checkkh == null)
            {
                ViewBag.ErroInfo = "Sai tai khoan";
                return View("LoginAcountKH");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["MaKhang"]=kh.MaKH;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LoginAcountNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAcountNV(tbl_NhanVien nv)
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
        private string MaKhachHang()
        {
            var CheckMa = db.tbl_KhachHang.OrderByDescending(p => p.MaKH).FirstOrDefault();
            if (CheckMa != null)
            {
                int MKH = int.Parse(CheckMa.MaKH.Substring(2));
                int nextMKH = MKH + 1;
                return "KH" + nextMKH.ToString();
            }
            return "KH1";
        }
        public ActionResult RegisterKH()
        {
            var loaiKhachHangs = db.tbl_LoaiKhach.ToList();
            ViewBag.MaLoaiKH = new SelectList(loaiKhachHangs, "MaLoaiKH", "TenLoaiKH");
            return View();
        }
       
        [HttpPost]
        public ActionResult RegisterKH( string HoTen,string TaiKhoan,string Email, string SDT,string CCCD, string DiaChi,DateTime NgaySinh,
            string MatKhau, string MaLoaiKH)
        {
            
            if (ModelState.IsValid)
            {
                string makhachH = MaKhachHang();
                tbl_KhachHang khachhang = new tbl_KhachHang()
                    {
                        MaKH = makhachH,
                        HoTen = HoTen,
                        TaiKhoan = TaiKhoan,
                        MatKhau = MatKhau,
                        Email =  Email,
                        SDT = SDT,
                        NgaySinh = NgaySinh,
                        CCCD = CCCD,
                        DiaChi = DiaChi,
                        MaLoaiKH = int.Parse(MaLoaiKH)
                    };
                var checkTK = db.tbl_KhachHang.Where(s => s.TaiKhoan == khachhang.TaiKhoan).FirstOrDefault();
                if (checkTK == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_KhachHang.Add(khachhang);
                    db.SaveChanges();
                    return RedirectToAction("LoginAcountKH");
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