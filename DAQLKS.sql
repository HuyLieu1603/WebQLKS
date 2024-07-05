create database DAQLKS 
go

use DAQLKS
go

-----LOẠI KHÁCH HÀNG----

CREATE TABLE tbl_LoaiKhach(
	MaLoaiKH INT PRIMARY KEY,
	TenLoaiKH NVARCHAR(30)
)
insert into tbl_LoaiKhach
values(001,'Ngoại Quốc')
insert into tbl_LoaiKhach
values(002,'Trong Nước')

-----KHÁCH HÀNG-----

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
----------------------LOẠI PHÒNG--------------------------

--CREATE
CREATE TABLE tbl_LoaiPhong(
	MaLoaiPhong CHAR(4) PRIMARY KEY,
	TenLoaiPhong NVARCHAR(20),
	DonGia MONEY
)
--ALTER
ALTER TABLE tbl_LoaiPhong
ADD MoTa NVARCHAR(MAX)

ALTER TABLE tbl_LoaiPhong
ADD img NVARCHAR(MAX)
--INSERT--
INSERT INTO tbl_LoaiPhong
VALUES('PDON',N'Phòng Đơn',150000),
('PDOI',N'Phòng Đôi',170000),
('PVIP',N'Phòng Vip',200000)
--UPDATE--
--CẬP NHẬT BẢNG LOẠI PHÒNG
UPDATE tbl_LoaiPhong
SET MoTa=N'Đây là một loại phòng được thiết kế để phục vụ một khách lưu trú duy nhất. Đây là sự lựa chọn lý tưởng cho những du khách đi công tác, người du lịch một mình hoặc những ai cần một nơi nghỉ ngơi yên tĩnh và thoải mái'
WHERE MaLoaiPhong='PDON'

UPDATE tbl_LoaiPhong
SET MoTa=N'Đây là một loại phòng được thiết kế để phục vụ hai khách lưu trú. Đây là sự lựa chọn phổ biến cho các cặp đôi, bạn bè hoặc đồng nghiệp muốn chia sẻ không gian nghỉ ngơi.'
WHERE MaLoaiPhong='PDOI'

UPDATE tbl_LoaiPhong
SET MoTa=N'Đây là loại phòng cao cấp nhất, được thiết kế để cung cấp trải nghiệm lưu trú sang trọng, tiện nghi và độc đáo. Phòng VIP thường dành cho các khách hàng đặc biệt, như doanh nhân, người nổi tiếng, hoặc những người mong muốn sự thoải mái và dịch vụ tốt nhất.'
WHERE MaLoaiPhong='PVIP'
--Load hình 
UPDATE tbl_LoaiPhong
SET img='PDON.jpg'
WHERE MaLoaiPhong='PDON'

UPDATE tbl_LoaiPhong
SET img='PDOI.jpg'
WHERE MaLoaiPhong='PDOI'

UPDATE tbl_LoaiPhong
SET img='PVIP.jpg'
WHERE MaLoaiPhong='PVIP'

SELECT* FROM tbl_LoaiPhong

-------TRẠNG THÁI PHÒNG-----------------

CREATE TABLE tbl_TrangThaiPhong(
	MaTrangThai CHAR(4) PRIMARY KEY,
	TenTrangThai NVARCHAR(20)
)

-------------PHÒNG-------------------

--CREATE--
CREATE TABLE tbl_Phong(
	MaPhong CHAR(4) PRIMARY KEY,
	SoPhong INT,
	MaLoaiPhong CHAR(4) FOREIGN KEY REFERENCES tbl_LoaiPhong(MaLoaiPhong),
	MaTrangThai CHAR(4) FOREIGN KEY REFERENCES tbl_TrangThaiPhong(MaTrangThai)
)
--ALTER--
ALTER TABLE tbl_Phong
ADD HinhAnh NVARCHAR(MAX)
--INSERT--
INSERT INTO tbl_Phong VALUES
('P100',100,'PDON',NULL,NULL),
('P101',101,'PDON',NULL,NULL),
('P102',102,'PDON',NULL,NULL),
('P103',103,'PDOI',NULL,NULL),
('P200',200,'PDOI',NULL,NULL),
('P201',201,'PDOI',NULL,NULL),
('P202',202,'PVIP',NULL,NULL),
('P203',203,'PVIP',NULL,NULL),
('P204',204,'PVIP',NULL,NULL)


--------CHỨC VỤ--------------

--CREATE--
CREATE TABLE tbl_ChucVu(
	MaCV CHAR(4) PRIMARY KEY,
	TenChucVu NVARCHAR(30)
)

-----NHÂN VIÊN----------------

CREATE TABLE tbl_NhanVien(
	MaNV VARCHAR(10) PRIMARY KEY,
	HoTen NVARCHAR(30),
	TaiKhoan NVARCHAR(30),
	MatKhau VARCHAR(30),
	Email VARCHAR(30),
	NgaySinh DATE,
	MaCV CHAR(4) FOREIGN KEY REFERENCES tbl_ChucVu(MaCV)
)

----PHIẾU THUÊ PHÒNG--------

--CREATE--
CREATE TABLE tbl_PhieuThuePhong(
	MaPhieuThuePhong VARCHAR(10) PRIMARY KEY,
	NgayBatDauThue DATE,
	MaPhong CHAR(4) FOREIGN KEY REFERENCES tbl_Phong(MaPhong)
)
--ALTER--
ALTER TABLE tbl_PhieuThuePhong
ADD SLKhach INT

ALTER TABLE tbl_PhieuThuePhong
ADD SLKhachNuocNgoai INT

ALTER TABLE tbl_PhieuThuePhong
ADD NgayKetThucThue DATE, TrangThai NVARCHAR(MAX)

-------CHI TIẾT PHIẾU THUÊ-------

--CREATE--
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
--ALTER--
ALTER TABLE tbl_ChiTieuPhieuThue
DROP COLUMN SoLuongKhach 
ALTER TABLE tbl_ChiTieuPhieuThue
DROP COLUMN SoLuongKhachNuocNgoai

--------HÓA ĐƠN---------

CREATE TABLE tbl_HoaDon(
	MaHD VARCHAR(10) PRIMARY KEY,
	NgayThanhToan DATE,
	TongTien MONEY,
	MaKH VARCHAR(10) FOREIGN KEY REFERENCES tbl_KhachHang(MaKH),
	MaPhieuThuePhong VARCHAR(10) FOREIGN KEY REFERENCES tbl_PhieuThuePhong(MaPhieuThuePhong),
	MaNV VARCHAR(10) FOREIGN KEY REFERENCES tbl_NhanVien(MaNV)
)

------LOẠI DỊCH VỤ-----------------------------

CREATE TABLE tbl_LoaiDichVu(
	MaLoaiDV CHAR(4) PRIMARY KEY,
	TenLoaiDV NVARCHAR(20)
)
--INSERT--
INSERT INTO tbl_LoaiDichVu VALUES
('DV01',N'Dịch vụ dọn dẹp'),
('DV02',N'Dịch vụ ăn uống'),
('DV03',N'Dịch vụ spa'),
('DV04',N'Dịch vụ đưa rước')

---TRẠNG THÁI DỊCH VỤ---------------------------

CREATE TABLE tbl_TrangThaiDichVu(
	MaTrangThaiDV CHAR(4) PRIMARY KEY,
	TenTrangThai NVARCHAR(20)
)

----DỊCH VỤ---------

CREATE TABLE tbl_DichVu(
	MaDV CHAR(4) PRIMARY KEY,
	TenDV NVARCHAR(MAx),
	DonGia MONEY,
	MaLoaiDV CHAR(4) FOREIGN KEY REFERENCES tbl_LoaiDichVu(MaLoaiDV)
)
--INSERT--
INSERT INTO tbl_DichVu VALUES
('DA01',N'Salad cá ngừ',NULL,'DV02'),
('DA02',N'Súp gà bắp non',NULL,'DV02'),
('DA03',N'Bò kho bánh mì',NULL,'DV02'),
('DA04',N'Tiramisu',NULL,'DV02'),
('DA05',N'Bánh Cupcake',NULL,'DV02'),
('DA06',N'Chè Khúc Bạch',NULL,'DV02'),
('DA07',N'Khoai Tây Nướng Ô Liu',NULL,'DV02'),
('DA08',N'Cocottes Nướng',NULL,'DV02')
INSERT INTO tbl_DichVu VALUES
('TU01',N'Capucino',NULL,'DV02'),
('TU02',N'Expresso',NULL,'DV02'),
('TU03',N'Cafe Trứng',NULL,'DV02'),
('TU04',N'Nước Ép Cam',NULL,'DV02'),
('TU05',N'Nước Ép Dưa Hấu',NULL,'DV02'),
('TU06',N'Sinh Tố Dâu',NULL,'DV02'),
('TU07',N'Tiger Nâu',NULL,'DV02'),
('TU08',N'Rượu Vang Đỏ',NULL,'DV02')
INSERT INTO tbl_DichVu VALUES
('DD01',N'Dọn dẹp phòng',NULL,'DV01')
INSERT INTO tbl_DichVu VALUES
('DR01',N'Đưa rước sân bay',NULL,'DV04')
INSERT INTO tbl_DichVu VALUES
('LD01',N'Massage body',NULL,'DV03'),
('LD02',N'Massage trị liệu cổ vai gáy',NULL,'DV03'),
('LD03',N'Massage trị liệu thái dương',NULL,'DV03'),
('LD04',N'Chăm sóc da mặt',NULL,'DV03'),
('LD05',N'Tẩy tế bào chết toàn thân',NULL,'DV03'),
('LD06',N'Tắm trắng',NULL,'DV03'),
('LD07',N'Triệt lông body',NULL,'DV03'),
('LD08',N'Triệt lông vùng nách',NULL,'DV03'),
('LD09',N'Triệt lông tay,chân',NULL,'DV03')
--ALTER--
ALTER TABLE tbl_DichVu
ADD img NVARCHAR(MAX)
ALTER TABLE tbl_DichVu
ALTER COLUMN TenDV NVARCHAR(50)

ALTER TABLE tbl_DichVu
ADD MoTa NVARCHAR(MAX)
--UPDATE--
UPDATE tbl_DichVu
SET MoTa=N'Món salad kết hợp cá ngừ tươi hoặc đóng hộp với các loại rau củ như xà lách, cà chua, dưa chuột, hành tây, và được trộn với sốt chanh hoặc mayonnaise để tăng hương vị.'
WHERE MaDV='DA01'

UPDATE tbl_DichVu
SET MoTa=N'Súp gà bổ dưỡng với thịt gà mềm, bắp non ngọt ngào, thường được nấu cùng với các loại rau củ như cà rốt, hành tây, và nêm nếm gia vị vừa ăn.'
WHERE MaDV='DA02'

UPDATE tbl_DichVu
SET MoTa=N'Món thịt bò kho đậm đà hương vị, nấu với cà rốt, khoai tây và các loại gia vị đặc trưng, thường được ăn kèm với bánh mì giòn.'
WHERE MaDV='DA03'

UPDATE tbl_DichVu
SET MoTa=N'Món tráng miệng nổi tiếng của Ý, làm từ lớp bánh quy nhúng cà phê, phủ kem mascarpone và rắc bột cacao lên trên.'
WHERE MaDV='DA04'

UPDATE tbl_DichVu
SET MoTa=N'Những chiếc bánh nhỏ xinh xắn, mềm mịn và thơm ngon, thường được trang trí với kem bơ, đường icing và các loại hạt, kẹo.'
WHERE MaDV='DA05'

UPDATE tbl_DichVu
SET MoTa=N'Món chè thanh mát với những miếng khúc bạch (thạch làm từ kem sữa) dẻo dai, thường được ăn kèm với trái cây và nước đường phèn.'
WHERE MaDV='DA06'

UPDATE tbl_DichVu
SET MoTa=N'Khoai tây cắt miếng hoặc lát, nướng giòn với dầu ô liu và các loại thảo mộc như rosemary, thêm một chút muối và tiêu.'
WHERE MaDV='DA07'

UPDATE tbl_DichVu
SET MoTa=N'Món ăn nướng trong nồi đất hoặc nồi gang nhỏ, thường là các loại thực phẩm như thịt, rau củ, phô mai được nướng chín tới, giữ nguyên hương vị thơm ngon.'
WHERE MaDV='DA08'

UPDATE tbl_DichVu
SET DonGia=120000
WHERE MaDV='DA01'

UPDATE tbl_DichVu
SET DonGia=40000
WHERE MaDV='DA02'

UPDATE tbl_DichVu
SET DonGia=80000
WHERE MaDV='DA03'

UPDATE tbl_DichVu
SET DonGia=70000
WHERE MaDV='DA04'

UPDATE tbl_DichVu
SET DonGia=30000
WHERE MaDV='DA05'

UPDATE tbl_DichVu
SET DonGia=35000
WHERE MaDV='DA06'

UPDATE tbl_DichVu
SET DonGia=45000
WHERE MaDV='DA07'

------DỊCH VỤ ĐÃ ĐẶT-----------

CREATE TABLE tbl_DichVuDaDat(
	ID VARCHAR(10) PRIMARY KEY,
	NgaySuDungDV DATE,
	MaHD VARCHAR(10) FOREIGN KEY REFERENCES tbl_HoaDon(MaHD),
	MaTrangThaiDV char(4) FOREIGN KEY REFERENCES tbl_TrangThaiDichVu(MaTrangThaiDV),
	MaNV VARCHAR(10) FOREIGN KEY REFERENCES tbl_NhanVien(MaNV),
	MaKH VARCHAR(10) FOREIGN KEY REFERENCES tbl_KhachHang(MaKH),
	MaDV CHAR(4) FOREIGN KEY REFERENCES tbl_DichVu(MaDV),
)

---CHI TIẾT PHÒNG--

CREATE TABLE tbl_ChiTietPhong(
	MaPhong CHAR(4) FOREIGN KEY REFERENCES tbl_Phong(MaPhong),
	MaLoaiPhong CHAR(4) FOREIGN KEY REFERENCES tbl_LoaiPhong(MaLoaiPhong),
	TienIch NVARCHAR(20),
	HinhAnh NVARCHAR(MAX),
	DienTich INT
	
)
--ALTER--
ALTER TABLE tbl_ChiTietPhong
DROP COLUMN DienTich

ALTER TABLE tbl_ChiTietPhong
ALTER COLUMN MaPhong CHAR(4) NOT NULL;

ALTER TABLE tbl_ChiTietPhong
ALTER COLUMN MaLoaiPhong CHAR(4) NOT NULL;

SELECT * FROM tbl_ChiTietPhong
--INSERT--
INSERT INTO tbl_ChiTietPhong VALUES
('P100','PDON',N'Wifi Miễn Phí',NULL),
('P100','PDON',N'Máy sấy tóc',NULL),
('P100','PDON',N'View thành phố',NULL),
('P100','PDON',N'Ổ điện gần giường',NULL),
('P100','PDON',N'Neflix',NULL)
INSERT INTO tbl_ChiTietPhong VALUES
('P101','PDON',N'Wifi Miễn Phí',NULL),
('P101','PDON',N'Máy sấy tóc',NULL),
('P101','PDON',N'View thành phố',NULL),
('P101','PDON',N'Ổ điện gần giường',NULL),
('P101','PDON',N'Neflix',NULL)
INSERT INTO tbl_ChiTietPhong VALUES
('P102','PDON',N'Wifi Miễn Phí',NULL),
('P102','PDON',N'Máy sấy tóc',NULL),
('P102','PDON',N'View thành phố',NULL),
('P102','PDON',N'Ổ điện gần giường',NULL),
('P102','PDON',N'Neflix',NULL)
INSERT INTO tbl_ChiTietPhong VALUES
('P103','PDOI',N'Wifi Miễn Phí',NULL),
('P103','PDOI',N'Máy sấy tóc',NULL),
('P103','PDOI',N'View thành phố',NULL),
('P103','PDOI',N'Ổ điện gần giường',NULL),
('P103','PDOI',N'Neflix',NULL)
INSERT INTO tbl_ChiTietPhong VALUES
('P200','PDOI',N'Wifi Miễn Phí',NULL),
('P200','PDOI',N'Máy sấy tóc',NULL),
('P200','PDOI',N'View thành phố',NULL),
('P200','PDOI',N'Ổ điện gần giường',NULL),
('P200','PDOI',N'Neflix',NULL)
INSERT INTO tbl_ChiTietPhong VALUES
('P201','PDOI',N'Wifi Miễn Phí',NULL),
('P201','PDOI',N'Máy sấy tóc',NULL),
('P201','PDOI',N'View thành phố',NULL),
('P201','PDOI',N'Ổ điện gần giường',NULL),
('P201','PDOI',N'Neflix',NULL)
INSERT INTO tbl_ChiTietPhong VALUES
('P202','PVIP',N'Wifi Miễn Phí',NULL),
('P202','PVIP',N'Máy sấy tóc',NULL),
('P202','PVIP',N'View thành phố',NULL),
('P202','PVIP',N'Ổ điện gần giường',NULL),
('P202','PVIP',N'Neflix',NULL)

SELECT MoTa
FROM tbl_LoaiPhong
WHERE MaLoaiPhong='PDON'
SELECT TienIch
FROM tbl_ChiTietPhong
WHERE MaLoaiPhong='PDON'
GROUP BY TienIch
SELECT * FROM tbl_ChiTietPhong
select * from tbl_LoaiPhong

----THỐNG KÊ----
CREATE TABLE tbl_ThongKe(
	MaThongKe INT PRIMARY KEY,
	TenThongKe NVARCHAR(50),
	Ngay DATE,
	TongThuNhap MONEY,
	MaNV VARCHAR(10) FOREIGN KEY REFERENCES tbl_NhanVien(MaNV)
)

--CHI TIẾT THỐNG KÊ--
CREATE TABLE tbl_ChiTietThongKe(
	MaChiTiet INT PRIMARY KEY,
	MaThongKe INT FOREIGN KEY REFERENCES tbl_ThongKe(MaThongKe),
	MaHD VARCHAR(10) FOREIGN KEY REFERENCES tbl_HoaDon(MaHD)
)
