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
        public ActionResult chiTietLoaiDV(string maLoaiDV)
        {
            var ct = db.tbl_DichVu.Where(dv => dv.MaLoaiDV == maLoaiDV).ToList();
            return View(ct);
        }
        public ActionResult detailService(string maDV)
        {
            var ctDV = db.tbl_DichVu.Find(maDV);
            return View(ctDV);
        }
        private string ID()
        {
            var lastID = db.tbl_DichVuDaDat.OrderByDescending(m => m.ID).FirstOrDefault();
            if (lastID != null)
            {
                int iD = int.Parse(lastID.ID.Substring(2));
                int newID = iD + 1;
                return "SD" + newID.ToString();
            }
            return "SD1";

        }
        private string maHoaDon()
        {
            var lastBill = db.tbl_HoaDon.OrderByDescending(p => p.MaHD).FirstOrDefault();
            if (lastBill != null)
            {
                int MHD = int.Parse(lastBill.MaPhieuThuePhong.Substring(2));
                int nextMHD = MHD + 1;
                return "HD" + nextMHD.ToString();
            }
            return "HD1";
        }
        [HttpPost]
        public ActionResult orderService(string maDV)
        {
            if (Session["KH"] == null)
            {
                ViewBag.Message = "Quý Khách Vui Lòng Đăng Nhập Để Đặt Dịch Vụ";
                return RedirectToAction("LoginAcountKH", "LoginAcount");
            }
            var maKH = Session["KH"].ToString();
            string id = ID();
            var donGia = db.tbl_DichVu.Where(dv => dv.MaDV == maDV).Select(dv => dv.DonGia).FirstOrDefault();
            if (donGia == null)
            {
                ViewBag.Message = "Dịch vụ không tồn tại hoặc không có đơn giá.";
                return RedirectToAction("Index", "Home");
            }
            string maHD = db.tbl_HoaDon
                         .Where(hd => hd.MaKH == maKH && hd.TrangThai == "Chưa thanh toán")
                         .OrderByDescending(hd => hd.MaHD)
                         .Select(hd => hd.MaHD)
                         .FirstOrDefault();
            var hoaDon = db.tbl_HoaDon.SingleOrDefault(hd => hd.MaHD == maHD);
            tbl_DichVuDaDat ord_service = new tbl_DichVuDaDat
            {
                ID = id,
                NgaySuDungDV = DateTime.Now,
                MaHD = maHD,
                MaTrangThaiDV = "TT01",
                MaNV = null,
                MaKH = Session["KH"].ToString(),
                MaDV = maDV
            };
            db.tbl_DichVuDaDat.Add(ord_service);
            if (hoaDon != null)
            {
                hoaDon.TongTien += donGia;
            }
            if (hoaDon == null)
            {
                ViewBag.error = "Cần đặt phòng trước khi sử dụng dịch vụ";
                return RedirectToAction("Index", "Home");
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}