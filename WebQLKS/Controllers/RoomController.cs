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
        DAQLKSEntities2 database = new DAQLKSEntities2();
        // GET: Room
        public ActionResult CategoryRoom()
        {
            var room = database.tbl_LoaiPhong.ToList();
            return View(room);
        }
        //Load Phòng Theo Loại Phòng
        public ActionResult DetailRoom(string MaLoaiPhong)
        {
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
        [HttpGet]
        public ActionResult DatPhong(string maPhong)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DatPhong(string maPhong, int SLK, int SLNN)
        {
            DateTime checkIn = (DateTime)Session["Check-in"];
            DateTime checkOut = (DateTime)Session["Check-out"];
            string maPT = maPhieuThue();
            var phieuThuePhong = new tbl_PhieuThuePhong
            {
                MaPhieuThuePhong = maPT,
                MaPhong = maPhong,
                NgayBatDauThue = checkIn,
                NgayKetThucThue = checkOut,
                SLKhach = SLK,
                SLKhachNuocNgoai = SLNN,
                TrangThai = "Chưa nhận phòng"
            };
            database.tbl_PhieuThuePhong.Add(phieuThuePhong);
            database.SaveChanges();
            return View();
        }
    }
}