using System;
using System.Collections.Generic;
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
                tenPhong = ten,
                donGia = donGia,
                MoTa = moTa,
                TienIch = tienIch
            };
            return View(viewModel);
        }
    }
}