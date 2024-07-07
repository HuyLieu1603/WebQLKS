﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
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
        //DANH SÁCH PHÒNG
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
                    room.HinhAnh = fileName;
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
            var item = db.tbl_Phong.Where(i => i.MaPhong.Equals(maPhong)).FirstOrDefault();
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
                if (img != null && img.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(img.FileName);
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
            var roomType = db.tbl_LoaiPhong.Where(r => r.MaLoaiPhong == maLoaiPhong).FirstOrDefault();
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }
        [HttpPost, ActionName("XoaLoaiPhong")]
        public ActionResult DeleteConfirmed(string maLoaiPhong)
        {
            try
            {
                var roomType = db.tbl_LoaiPhong.Where(r => r.MaLoaiPhong == maLoaiPhong).FirstOrDefault();
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
            var roomType = db.tbl_LoaiPhong.Where(r => r.MaLoaiPhong == maLoaiPhong).FirstOrDefault();
            return View(roomType);
        }
        [HttpPost]
        public ActionResult ChinhSuaLoaiPhong(string maLoaiPhong, tbl_LoaiPhong roomType)
        {
            db.Entry(roomType).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhMucPhong");
        }
        public ActionResult ChiTietLoaiPhong(string maLoaiPhong)
        {
            var roomType = db.tbl_LoaiPhong.Where(r => r.MaLoaiPhong == maLoaiPhong).FirstOrDefault();
            return View(roomType);

        }
        //MENU
        private string maMonAn()
        {
            var lastBooking = db.tbl_DichVu.Where(p => p.MaDV.StartsWith("DA")).OrderByDescending(p => p.MaDV).FirstOrDefault();
            if (lastBooking != null)
            {
                int maDV = int.Parse(lastBooking.MaDV.Substring(3));
                int nextMPT = maDV + 1;
                if (nextMPT >= 10)
                {
                    return "DA" + nextMPT.ToString();
                }
                return "DA0" + nextMPT.ToString();
            }
            return "DA01";
        }
        public ActionResult ThucDon()
        {
            ViewBag.Current = "ThucDon";
            var menu = db.tbl_DichVu.Where(m => m.MaLoaiDV == "DV02").ToList();
            return View(menu);
        }
        [HttpGet]
        public ActionResult ThemMon()
        {
            var maDV = maMonAn();
            ViewBag.maDV = maDV;
            return View();
        }
        [HttpPost]
        public ActionResult ThemMon(tbl_DichVu monAn, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    // Đặt tên file
                    var fileName = Path.GetFileName(img.FileName);

                    // Đặt đường dẫn để lưu trữ ảnh
                    var path = Path.Combine(Server.MapPath("~/Content/Service/menu/"), fileName);

                    // Lưu ảnh vào thư mục ~/Content/Room/img/
                    img.SaveAs(path);

                    // Lưu đường dẫn ảnh vào model
                    monAn.img = fileName;
                    monAn.MaDV = maMonAn();
                    monAn.MaLoaiDV = "DV02";
                }
                db.tbl_DichVu.Add(monAn);
                db.SaveChanges();
                return RedirectToAction("ThucDon");

            }
            return View();
        }
        [HttpGet]
        public ActionResult SuaMon(string maDV)
        {
            var monAn = db.tbl_DichVu.Where(m => m.MaDV == maDV).FirstOrDefault();
            return View(monAn);
        }
        [HttpPost]
        public ActionResult SuaMon(tbl_DichVu monAn, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var mon = db.tbl_DichVu.FirstOrDefault(m => m.MaDV == monAn.MaDV);
                if (mon != null)
                {
                    if (img != null && img.ContentLength > 0)
                    {
                        // Đặt tên file
                        var fileName = Path.GetFileName(img.FileName);

                        // Đặt đường dẫn để lưu trữ ảnh
                        var path = Path.Combine(Server.MapPath("~/Content/Service/menu/"), fileName);

                        // Lưu ảnh vào thư mục ~/Content/Service/menu/
                        img.SaveAs(path);

                        // Lưu đường dẫn ảnh vào model
                        mon.img = fileName;
                    }

                    // Cập nhật các trường khác của model
                    mon.TenDV = monAn.TenDV;
                    mon.DonGia = monAn.DonGia;
                    mon.MaLoaiDV = monAn.MaLoaiDV;
                    mon.MoTa = monAn.MoTa;

                    // Đánh dấu thực thể là đã sửa đổi
                    db.Entry(mon).State = System.Data.Entity.EntityState.Modified;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    return RedirectToAction("ThucDon");
                }
                else
                {
                    // Xử lý trường hợp không tìm thấy thực thể
                    ModelState.AddModelError("", "Không tìm thấy món ăn cần cập nhật.");
                }
            }

            // Nếu có lỗi, hiển thị lại view cùng với thông báo lỗi
            return View(monAn);
        }

        public ActionResult ChiTietMon(string maDV)
        {
            var monAn = db.tbl_DichVu.Where(m => m.MaDV == maDV).FirstOrDefault();
            return View(monAn);
        }

        [HttpGet]
        public ActionResult XoaMon(string maDV)
        {
            if (maDV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mon = db.tbl_DichVu.Where(r => r.MaDV == maDV).FirstOrDefault();
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }
        [HttpPost, ActionName("XoaMon")]
        public ActionResult XacNhanXoaMon(string maDV)
        {
            try
            {
                var mon = db.tbl_DichVu.Where(r => r.MaDV == maDV).FirstOrDefault();
                db.tbl_DichVu.Remove(mon);
                db.SaveChanges();
                return RedirectToAction("ThucDon");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
        //SPA
        public ActionResult Spa()
        {
            var spa = db.tbl_DichVu.Where(s => s.MaLoaiDV == "DV03").ToList();
            return View(spa);
        }

        public ActionResult ChiTietSpa(string maDV)
        {
            var spa = db.tbl_DichVu.Where(m => m.MaDV == maDV).FirstOrDefault();
            return View(spa);
        }

        [HttpGet]
        public ActionResult ChinhSuaSpa(string maDV)
        {
            var spa = db.tbl_DichVu.Where(m => m.MaDV == maDV).FirstOrDefault();
            return View(spa);
        }
        [HttpPost]
        public ActionResult ChinhSuaSpa(tbl_DichVu spa, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var mon = db.tbl_DichVu.FirstOrDefault(m => m.MaDV == spa.MaDV);
                if (mon != null)
                {
                    if (img != null && img.ContentLength > 0)
                    {
                        // Đặt tên file
                        var fileName = Path.GetFileName(img.FileName);

                        // Đặt đường dẫn để lưu trữ ảnh
                        var path = Path.Combine(Server.MapPath("~/Content/Service/spa/"), fileName);

                        // Lưu ảnh vào thư mục ~/Content/Service/menu/
                        img.SaveAs(path);

                        // Lưu đường dẫn ảnh vào model
                        mon.img = fileName;
                    }

                    // Cập nhật các trường khác của model
                    mon.TenDV = spa.TenDV;
                    mon.DonGia = spa.DonGia;
                    mon.MaLoaiDV = spa.MaLoaiDV;
                    mon.MoTa = spa.MoTa;

                    // Đánh dấu thực thể là đã sửa đổi
                    db.Entry(mon).State = System.Data.Entity.EntityState.Modified;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    return RedirectToAction("Spa");
                }
                else
                {
                    // Xử lý trường hợp không tìm thấy thực thể
                    ModelState.AddModelError("", "Không tìm thấy món ăn cần cập nhật.");
                }
            }

            // Nếu có lỗi, hiển thị lại view cùng với thông báo lỗi
            return View(spa);
        }
        [HttpGet]
        public ActionResult XoaSpa(string maDV)
        {
            if (maDV == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spa = db.tbl_DichVu.Where(r => r.MaDV == maDV).FirstOrDefault();
            if (spa == null)
            {
                return HttpNotFound();
            }
            return View(spa);
        }
        [HttpPost, ActionName("XoaSpa")]
        public ActionResult XacNhanXoaSpa(string maDV)
        {
            try
            {
                var spa = db.tbl_DichVu.Where(r => r.MaDV == maDV).FirstOrDefault();
                db.tbl_DichVu.Remove(spa);
                db.SaveChanges();
                return RedirectToAction("Spa");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
        private string maSpa()
        {
            var lastBooking = db.tbl_DichVu.Where(p => p.MaDV.StartsWith("LD")).OrderByDescending(p => p.MaDV).FirstOrDefault();
            if (lastBooking != null)
            {
                int maDV = int.Parse(lastBooking.MaDV.Substring(3));
                int nextMPT = maDV + 1;
                if (nextMPT >= 10)
                {
                    return "LD" + nextMPT.ToString();
                }
                return "LD0" + nextMPT.ToString();
            }
            return "LD01";
        }
        [HttpGet]
        public ActionResult ThemSpa()
        {
            var maDV = maSpa();
            ViewBag.maDV = maDV;
            return View();
        }
        [HttpPost]
        public ActionResult ThemSpa(tbl_DichVu spa, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    // Đặt tên file
                    var fileName = Path.GetFileName(img.FileName);

                    // Đặt đường dẫn để lưu trữ ảnh
                    var path = Path.Combine(Server.MapPath("~/Content/Service/spa/"), fileName);

                    // Lưu ảnh vào thư mục ~/Content/Room/img/
                    img.SaveAs(path);

                    // Lưu đường dẫn ảnh vào model
                    spa.img = fileName;
                    spa.MaDV = maMonAn();
                    spa.MaLoaiDV = "DV03";
                }
                db.tbl_DichVu.Add(spa);
                db.SaveChanges();
                return RedirectToAction("Spa");

            }
            return View();
        }


        //DỌN DẸP
        public ActionResult YeuCauDonDep()
        {
            var lstRequest = db.tbl_DichVu.Where(r => r.MaDV == "TT03").ToList();
            return View(lstRequest);
        }

        //ĐƯA RƯỚC


        //DANH SÁCH PHIẾU THUÊ PHÒNG
        public ActionResult lstRoomOrder()
        {
            ViewBag.Current = "LstRoomOrder";
            var lstOrder = db.tbl_PhieuThuePhong.ToList();
            return View(lstOrder);
        }
        //Chỉnh sửa phiếu thuê phòng
        [HttpGet]
        public ActionResult ChinhSuaPhieuThue(string maPhieuThue)
        {
            List<string> lstTrangThai = new List<string>()
            {
                "Chưa nhận phòng",
                "Đã nhận phòng",
                "Đã hủy",
                "Chưa xác nhận"
            };
            var maPhong = db.tbl_Phong.ToList();
            ViewBag.mphong = new SelectList(maPhong, "MaPhong", "SoPhong");
            ViewBag.TT = new SelectList(lstTrangThai);
            var phieuThue = db.tbl_PhieuThuePhong.Where(i => i.MaPhieuThuePhong == maPhieuThue).FirstOrDefault();
            return View(phieuThue);
        }
        [HttpPost]
        public ActionResult ChinhSuaPhieuThue(string maPhieuThue, tbl_PhieuThuePhong phieuThue)
        {
            if (phieuThue.TrangThai != "Chưa nhận phòng")
            {
                return RedirectToAction("KhongCoQuyen", "BaoLoi");
            }
            var maPhong = phieuThue.MaPhong;
            var maLoaiPhong = db.tbl_Phong.Where(i => i.MaPhong == maPhong).Select(i => i.MaLoaiPhong).FirstOrDefault();
            var donGia = db.tbl_LoaiPhong.Where(i => i.MaLoaiPhong == maLoaiPhong).Select(i => i.DonGia).FirstOrDefault();
            tbl_HoaDon hd = db.tbl_HoaDon.Where(i => i.MaPhieuThuePhong == maPhieuThue).FirstOrDefault();
            hd.TongTien = donGia;
            db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
            db.Entry(phieuThue).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("lstRoomOrder");
        }

        public ActionResult ChiTietPhieuThue(string maPhieuThue)
        {
            var detail = db.tbl_PhieuThuePhong.Where(i => i.MaPhieuThuePhong == maPhieuThue).FirstOrDefault();
            return View(detail);
        }

        [HttpGet]
        public ActionResult XoaPhieuThue(string maPhieuThue)
        {
            if (maPhieuThue == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var phieuThue = db.tbl_PhieuThuePhong.Where(r => r.MaPhieuThuePhong == maPhieuThue).FirstOrDefault();
            if (phieuThue == null)
            {
                return HttpNotFound();
            }
            return View(phieuThue);
        }
        [HttpPost, ActionName("XoaPhieuThue")]
        public ActionResult XacNhanXoaPhieuThue(string maPhieuThue)
        {
            try
            {
                var phieuThue = db.tbl_PhieuThuePhong.Where(r => r.MaPhieuThuePhong == maPhieuThue).FirstOrDefault();
                db.tbl_PhieuThuePhong.Remove(phieuThue);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhong");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
        //KIỂM TRA PHÒNG TRỐNG
        [HttpGet]
        public JsonResult HienThiPhongTrong(DateTime dateS, DateTime dateE)
        {
            var room = db.tbl_Phong.Where(t => !(db.tbl_PhieuThuePhong
                    .Where(m => (m.TrangThai == "Chưa nhận phòng" || m.TrangThai == "Đã nhận phòng" || m.TrangThai == "Chưa xác nhận")
                                && m.NgayBatDauThue < dateE && m.NgayKetThucThue > dateS))
                    .Select(m => m.MaPhong)
                    .ToList().Contains(t.MaPhong)).Select(p => new
                    {
                        p.MaPhong,
                        p.SoPhong
                    })
        .ToList();
            return Json(room, JsonRequestBehavior.AllowGet);
        }

    }
}