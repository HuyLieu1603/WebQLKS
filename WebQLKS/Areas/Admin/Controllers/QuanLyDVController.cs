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
    [AdminAuthorize(maCV = "QLDV")]
    public class QuanLyDVController : Controller
    {
        DAQLKSEntities db = new DAQLKSEntities();
        // GET: QuanLyDV
        public ActionResult DanhSachPhong()
        {
            ViewBag.Current = "DanhSachPhong";
            var room = db.tbl_Phong.ToList();
            return View(room);
        }
        //Thêm Phòng
        [HttpGet]
        public ActionResult ThemPhong()
        {
            var maTT = db.tbl_TrangThaiPhong.ToList();
            var maLoai = db.tbl_LoaiPhong.ToList();
            ViewBag.maTT = new SelectList(maTT, "MaTrangThai", "TenTrangThai"); ;
            ViewBag.MaLoai = new SelectList(maLoai, "MaLoaiPhong", "TenLoaiPhong"); ;
            return View();
        }
        [HttpPost]
        public ActionResult ThemPhong(tbl_Phong room, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    // Đặt tên file
                    var fileName = Path.GetFileName(img.FileName);

                    // Đặt đường dẫn để lưu trữ ảnh
                    var path = Path.Combine(Server.MapPath("~/Content/Room/img/"), fileName);

                    // Lưu ảnh vào thư mục ~/Content/Room/img/
                    img.SaveAs(path);

                    // Lưu đường dẫn ảnh vào model
                    room.HinhAnh =fileName;
                }
                db.tbl_Phong.Add(room);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhong");

            }
            return View();
        }
        [HttpGet]
        public ActionResult XoaPhong(string maPhong)
        {
            if (maPhong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomType = db.tbl_Phong.Where(r => r.MaPhong == maPhong).FirstOrDefault();
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }
        [HttpPost, ActionName("XoaPhong")]
        public ActionResult XacNhanXoaPhong(string maPhong)
        {
            try
            {
                var roomType = db.tbl_Phong.Where(r => r.MaPhong == maPhong).FirstOrDefault();
                db.tbl_Phong.Remove(roomType);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhong");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
        [HttpGet]
        public ActionResult ChinhSuaPhong(string maPhong)
        {
            var maTT = db.tbl_TrangThaiPhong.ToList();
            var maLoai = db.tbl_LoaiPhong.ToList();
            ViewBag.maTT = new SelectList(maTT, "MaTrangThai", "TenTrangThai"); ;
            ViewBag.MaLoai = new SelectList(maLoai, "MaLoaiPhong", "TenLoaiPhong"); ;
            var roomType = db.tbl_Phong.Where(r => r.MaPhong == maPhong).FirstOrDefault();
            return View(roomType);
        }
        [HttpPost]
        public ActionResult ChinhSuaPhong(string maPhong, tbl_Phong roomType)
        {
            db.Entry(roomType).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachPhong");
        }
        public ActionResult ChiTietPhong(string maPhong)
        {
            var item = db.tbl_Phong.Where(i=>i.MaPhong.Equals(maPhong)).FirstOrDefault();
            return View(item);
        }

        //DANH MỤC PHÒNG
        public ActionResult DanhMucPhong()
        {
            ViewBag.Current = "DanhMucPhong";
            var roomType = db.tbl_LoaiPhong.ToList();
            return View(roomType);
        }
        [HttpGet]
        public ActionResult ThemLoaiPhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiPhong(tbl_LoaiPhong type, HttpPostedFileBase img)
        { 
            if (ModelState.IsValid)
            {
                if(img != null && img.ContentLength > 0)
                {
                    var fileName = Path .GetFileName(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Home/Image/"), fileName);
                    img.SaveAs(path);
                    type.img = fileName;
                }
                db.tbl_LoaiPhong.Add(type);
                db.SaveChanges();
                return RedirectToAction("DanhMucPhong");
            }
            return View(); 
        }
        [HttpGet]
        public ActionResult XoaLoaiPhong(string maLoaiPhong)
        {
            if (maLoaiPhong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roomType = db.tbl_LoaiPhong.Where(r=>r.MaLoaiPhong==maLoaiPhong).FirstOrDefault();
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }
        [HttpPost,ActionName("XoaLoaiPhong")]
        public ActionResult DeleteConfirmed(string maLoaiPhong)
        {
            try
            {
                var roomType=db.tbl_LoaiPhong.Where(r=>r.MaLoaiPhong==maLoaiPhong).FirstOrDefault();
                db.tbl_LoaiPhong.Remove(roomType);
                db.SaveChanges();
                return RedirectToAction("DanhMucPhong");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
        [HttpGet]
        public ActionResult ChinhSuaLoaiPhong(string maLoaiPhong)
        {
            var roomType = db.tbl_LoaiPhong.Where(r=>r.MaLoaiPhong==maLoaiPhong).FirstOrDefault();
            return View(roomType);
        }
        [HttpPost]
        public ActionResult ChinhSuaLoaiPhong(string maLoaiPhong,tbl_LoaiPhong roomType)
        {
            db.Entry(roomType).State=System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhMucPhong");
        }
        public ActionResult ChiTietLoaiPhong(string maLoaiPhong)
        {
            var roomType = db.tbl_LoaiPhong.Where(r=>r.MaLoaiPhong==maLoaiPhong).FirstOrDefault();
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