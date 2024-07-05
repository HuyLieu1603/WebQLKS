using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebQLKS.Models;

namespace WebQLKS.Controllers
{
    public class RoomController : Controller
    {
        DAQLKSEntities database = new DAQLKSEntities();
        // GET: Room
        public ActionResult CategoryRoom()
        {
            var room = database.tbl_LoaiPhong.ToList();
            var tienIchDict = new Dictionary<string, List<string>>();
            foreach (var item in room)
            {
                var maLoaiPhong = item.MaLoaiPhong;
                var lstTienIch = database.tbl_ChiTietPhong.Where(ct => ct.MaLoaiPhong == maLoaiPhong).Select(ct => ct.TienIch).Distinct().ToList();
                tienIchDict[maLoaiPhong] = lstTienIch;
            }
            ViewBag.TienIch = tienIchDict;
            return View(room);
        }
        //Load Phòng Theo Loại Phòng
        public ActionResult DetailRoom(string MaLoaiPhong)
        {
            ViewBag.imgLoaiPhong = database.tbl_Phong.Where(ha => ha.MaLoaiPhong == MaLoaiPhong).ToList();
            if ((MaLoaiPhong.ToString().Trim() == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ten = database.tbl_LoaiPhong.Where(r => r.MaLoaiPhong == MaLoaiPhong).Select(r => r.TenLoaiPhong).FirstOrDefault();
            var donGia = database.tbl_LoaiPhong.Where(dg => dg.MaLoaiPhong == MaLoaiPhong).Select(dg => dg.DonGia).FirstOrDefault();
            var moTa = database.tbl_LoaiPhong.Where(lp => lp.MaLoaiPhong == MaLoaiPhong).Select(lp => lp.MoTa).FirstOrDefault();
            if (moTa == null)
            {
                return HttpNotFound("Không tìm thấy thông tin phòng");
            }
            var tienIch = database.tbl_ChiTietPhong.Where(ct => ct.MaLoaiPhong == MaLoaiPhong).Select(ct => ct.TienIch).Distinct().ToList();
            var viewModel = new RoomDetailViewModel
            {
                maPhong = MaLoaiPhong,
                tenPhong = ten,
                donGia = donGia,
                MoTa = moTa,
                TienIch = tienIch
            };
            return View(viewModel);
        }
        // GET: TimPhong/Create
        [HttpGet]
        public ActionResult TimPhong(string MaLoaiPhong)
        {
            var model = new BookingViewModel
            {
                MaLoaiPhong = MaLoaiPhong,
                dateStart = DateTime.Now,
                dateEnd = DateTime.Now.AddDays(1)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult TimPhong(string dateStart, string dateEnd, string MaLoaiPhong)
        {
            List<tbl_Phong> lst = new List<tbl_Phong>();

            Session["Check-in"] = dateStart;
            Session["Check-out"] = dateEnd;
            if (string.IsNullOrEmpty(dateStart) || string.IsNullOrEmpty(dateEnd))
            {
                lst = database.tbl_Phong.ToList();
            }
            else
            {
                DateTime dateS, dateE;
                bool isDateStartValid = DateTime.TryParseExact(dateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateS);
                bool isDateEndValid = DateTime.TryParseExact(dateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateE);

                if (!isDateStartValid || !isDateEndValid)
                {
                    ModelState.AddModelError("", "Định dạng ngày không hợp lệ. Vui lòng nhập ngày theo định dạng yyyy/MM/dd.");
                    return View(new BookingViewModel { dateStart = DateTime.Now, dateEnd = DateTime.Now.AddDays(1) });
                }

                dateS = dateS.AddHours(12);
                dateE = dateE.AddHours(12);

                lst = database.tbl_Phong.Where(t => !(database.tbl_PhieuThuePhong
                    .Where(m => (m.TrangThai == "Chưa nhận phòng" || m.TrangThai == "Đang nhận phòng")
                                && m.NgayBatDauThue < dateE && m.NgayKetThucThue > dateS))
                    .Select(m => m.MaPhong)
                    .ToList().Contains(t.MaPhong) && t.MaLoaiPhong == MaLoaiPhong).ToList();

                ViewData["test"] = lst;
            }

            return View("DanhSachPhongTrong", lst);
        }
        private string maPhieuThue()
        {
            var lastBooking = database.tbl_PhieuThuePhong.OrderByDescending(p => p.MaPhieuThuePhong).FirstOrDefault();
            if (lastBooking != null)
            {
                int MPT = int.Parse(lastBooking.MaPhieuThuePhong.Substring(2));
                int nextMPT = MPT + 1;
                return "PT" + nextMPT.ToString();
            }
            return "PT1";
        }
        private string maHoaDon()
        {
            var lastBill = database.tbl_HoaDon.OrderByDescending(p => p.MaHD).FirstOrDefault();
            if (lastBill != null)
            {
                int MHD = int.Parse(lastBill.MaPhieuThuePhong.Substring(2));
                int nextMHD = MHD + 1;
                return "HD" + nextMHD.ToString();
            }
            return "HD1";
        }
        [HttpGet]
        public ActionResult DatPhong(string maPhong)
        {
            if (Session["KH"] == null)
            {
                return RedirectToAction("LoginAcountKH", "LoginAcount");
            }
            ViewBag.MP = maPhong;
            return View();
        }
        [HttpPost]
        public ActionResult DatPhong(string maPhong, int SLK, int SLNN)
        {
            try
            {
                DateTime checkIn = DateTime.Parse(Session["Check-in"].ToString());
                DateTime checkOut = DateTime.Parse(Session["Check-out"].ToString());
                string maPT = maPhieuThue();
                string maHD = maHoaDon();
                var donGia = (from lp in database.tbl_LoaiPhong
                              join p in database.tbl_Phong on lp.MaLoaiPhong equals p.MaLoaiPhong
                              where p.MaPhong == maPhong
                              select lp.DonGia).FirstOrDefault();
                tbl_PhieuThuePhong phieuThuePhong = new tbl_PhieuThuePhong
                {
                    MaPhieuThuePhong = maPT,
                    MaPhong = maPhong,
                    NgayBatDauThue = checkIn.Date,
                    NgayKetThucThue = checkOut.Date,
                    SLKhach = SLK,
                    SLKhachNuocNgoai = SLNN,
                    TrangThai = "Chưa nhận phòng",
                    MaKH = Session["KH"].ToString()
                };

                tbl_HoaDon hoaDon = new tbl_HoaDon
                {
                    MaHD = maHD,
                    NgayThanhToan = null,
                    TongTien = donGia,
                    MaKH = Session["KH"].ToString(),
                    MaPhieuThuePhong = maPT,
                    MaNV = null,
                    TrangThai = "Chưa thanh toán"
                };
                database.tbl_HoaDon.Add(hoaDon);
                database.tbl_PhieuThuePhong.Add(phieuThuePhong);
                database.SaveChanges();

                ViewBag.Message = "Đặt phòng thành công!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
            }

            return View();
        }
    }
}