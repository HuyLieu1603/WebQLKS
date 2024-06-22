create database DAQLKS 
go

use DAQLKS
go

--CREATE TABLE--
--TẠO BẢNG LOẠI KHÁCH HÀNG
CREATE TABLE tbl_LoaiKhach(
	MaLoaiKH INT PRIMARY KEY,
	TenLoaiKH NVARCHAR(30)
)
--TẠO BẢNG KHÁCH HÀNG
CREATE TABLE tbl_KhachHang(
	MaKH VARCHAR(10) PRIMARY KEY,
	HoTen NVARCHAR(30),
	TaiKhoan NVARCHAR(30),
	MatKhau VARCHAR(30),
	Email NVARCHAR(30),
	SDT VARCHAR(13),
	NgaySinh Date,
	CCCD CHAR(12),
	DiaChi NVARCHAR(50),
	MaLoaiKH INT FOREIGN KEY REFERENCES tbl_LoaiKhach(MaLoaiKH)
)
--TẠO BẢNG LOẠI PHÒNG
CREATE TABLE tbl_LoaiPhong(
	MaLoaiPhong CHAR(4) PRIMARY KEY,
	TenLoaiPhong NVARCHAR(20),
	DonGia MONEY
)
--TẠO BẢNG TRẠNG THÁI PHÒNG
CREATE TABLE tbl_TrangThaiPhong(
	MaTrangThai CHAR(4) PRIMARY KEY,
	TenTrangThai NVARCHAR(20)
)
--TẠO BẢNG PHÒNG
CREATE TABLE tbl_Phong(
	MaPhong CHAR(4) PRIMARY KEY,
	SoPhong INT,
	MaLoaiPhong CHAR(4) FOREIGN KEY REFERENCES tbl_LoaiPhong(MaLoaiPhong),
	MaTrangThai CHAR(4) FOREIGN KEY REFERENCES tbl_TrangThaiPhong(MaTrangThai)
)
--TẠO BẢNG CHỨC VỤ
CREATE TABLE tbl_ChucVu(
	MaCV CHAR(4) PRIMARY KEY,
	TenChucVu NVARCHAR(30)
)
--TẠO BẢNG NHÂN VIÊN
CREATE TABLE tbl_NhanVien(
	MaNV VARCHAR(10) PRIMARY KEY,
	HoTen NVARCHAR(30),
	TaiKhoan NVARCHAR(30),
	MatKhau VARCHAR(30),
	Email VARCHAR(30),
	NgaySinh DATE,
	MaCV CHAR(4) FOREIGN KEY REFERENCES tbl_ChucVu(MaCV)
)
--TẠO BẢNG PHIẾU THUÊ PHÒNG
CREATE TABLE tbl_PhieuThuePhong(
	MaPhieuThuePhong VARCHAR(10) PRIMARY KEY,
	NgayBatDauThue DATE,
	MaPhong CHAR(4) FOREIGN KEY REFERENCES tbl_Phong(MaPhong)
)
--TẠO BẢNG CHI TIẾT PHIẾU THUÊ
CREATE TABLE tbl_ChiTieuPhieuThue(
	MaChiTiet VARCHAR(10) PRIMARY KEY,
	HoTenKH NVARCHAR(30),
	NgayBatDauThue DATE,
	SoLuongKhach INT,
	SoLuongKhachNuocNgoai INT,
	CCCD CHAR(12),
	DiaChi NVARCHAR(50),
	MaPhieuThue VARCHAR(10) FOREIGN KEY REFERENCES tbl_PhieuThuePhong(MaPhieuThuePhong),
)
--TẠO BẢNG HÓA ĐƠN
CREATE TABLE tbl_HoaDon(
	MaHD VARCHAR(10) PRIMARY KEY,
	NgayThanhToan DATE,
	TongTien MONEY,
	MaKH VARCHAR(10) FOREIGN KEY REFERENCES tbl_KhachHang(MaKH),
	MaPhieuThuePhong VARCHAR(10) FOREIGN KEY REFERENCES tbl_PhieuThuePhong(MaPhieuThuePhong),
	MaNV VARCHAR(10) FOREIGN KEY REFERENCES tbl_NhanVien(MaNV)
)
--TẠO BẢNG LOẠI DỊCH VỤ
CREATE TABLE tbl_LoaiDichVu(
	MaLoaiDV CHAR(4) PRIMARY KEY,
	TenLoaiDV NVARCHAR(20)
)
--TẠO BẢNG TRẠNG THÁI DỊCH VỤ
CREATE TABLE tbl_TrangThaiDichVu(
	MaTrangThaiDV CHAR(4) PRIMARY KEY,
	TenTrangThai NVARCHAR(20)
)
--TẠO BẢNG DỊCH VỤ
CREATE TABLE tbl_DichVu(
	MaDV CHAR(4) PRIMARY KEY,
	TenDV NVARCHAR(20),
	DonGia MONEY,
	MaLoaiDV CHAR(4) FOREIGN KEY REFERENCES tbl_LoaiDichVu(MaLoaiDV)
)
--TẠO BẢNG DỊCH VỤ ĐÃ ĐẶT
CREATE TABLE tbl_DichVuDaDat(
	ID VARCHAR(10) PRIMARY KEY,
	NgaySuDungDV DATE,
	MaHD VARCHAR(10) FOREIGN KEY REFERENCES tbl_HoaDon(MaHD),
	MaTrangThaiDV char(4) FOREIGN KEY REFERENCES tbl_TrangThaiDichVu(MaTrangThaiDV),
	MaNV VARCHAR(10) FOREIGN KEY REFERENCES tbl_NhanVien(MaNV),
	MaKH VARCHAR(10) FOREIGN KEY REFERENCES tbl_KhachHang(MaKH),
	MaDV CHAR(4) FOREIGN KEY REFERENCES tbl_DichVu(MaDV),
)

--ALTER TABLE--
ALTER TABLE tbl_Phong
ADD HinhAnh NVARCHAR(MAX)



