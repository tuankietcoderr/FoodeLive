CREATE DATABASE QUANLYNHAHANG
USE QUANLYNHAHANG
set dateformat dmy

CREATE TABLE NHANVIEN
(
	MANV CHAR(4) NOT NULL,
	HOTEN NVARCHAR(40),
	GIOITINH NVARCHAR(10),
	NGSINH SMALLDATETIME,
	DCHI NVARCHAR(50),
	LUONG MONEY,
	SODT VARCHAR(20),
	NGVL SMALLDATETIME,
	TENNGUOIDUNG CHAR(20),
	MATKHAU CHAR(30)
	CONSTRAINT PK_NHANVIEN PRIMARY KEY (MANV),
)

CREATE TABLE MONAN
(
	MAMONAN CHAR(4) NOT NULL,
	TENMONAN NVARCHAR(40),
	GIA MONEY,
	CONSTRAINT PK_MAMONAN PRIMARY KEY (MAMONAN),
)

CREATE TABLE BANAN
(
	MABANAN CHAR(4) NOT NULL,
	LOAI NVARCHAR(40),
	CONSTRAINT PK_MABANAN PRIMARY KEY (MABANAN),
)

create table HOADON
(
	SOHD int NOT NULL ,
	MABANAN CHAR(4),
	MANV CHAR(4),
	NGHD SMALLDATETIME,
	TRIGIA MONEY ,
	CONSTRAINT PK_SOHD PRIMARY KEY (SOHD),
	CONSTRAINT FK_MABANAN FOREIGN KEY (MABANAN) REFERENCES BANAN (MABANAN),
	CONSTRAINT FK_MANV FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
)

CREATE TABLE CTHD
(
	SOHD INT NOT NULL,
	MAMONAN CHAR(4) NOT NULL,
	SL INT,
	CONSTRAINT PK_SOHD_MAMONAN PRIMARY KEY (SOHD,MAMONAN),
	CONSTRAINT FK_SOHD FOREIGN KEY (SOHD) REFERENCES HOADON (SOHD),
	CONSTRAINT FK_MAMONAN FOREIGN KEY (MAMONAN) REFERENCES MONAN (MAMONAN),
)

/*Trigger*/
ALTER TABLE MONAN
ADD CONSTRAINT GIA_CHECK CHECK(GIA>25000)

ALTER TABLE NHANVIEN ADD CONSTRAINT NGVL_CHECK CHECK(NGVL>NGSINH)

ALTER TABLE CTHD ADD CONSTRAINT SL_CHECK CHECK(SL>=1)

CREATE TRIGGER nghd_ngvl_hoad_insert
ON hoadon 
AFTER INSERT 
AS 
	DECLARE @ng_hoadon smalldatetime 
	DECLARE @ng_vaolam smalldatetime 
	SELECT @ng_hoadon=nghd, @ng_vaolam=ngvl
	FROM NHANVIEN, inserted
	WHERE NHANVIEN.MANV=inserted.MANV
IF @ng_hoadon< @ng_vaolam
BEGIN
	rollback transaction
	print N'Ngày hóa đơn phải lớn hơn ngày vào làm'
END;


CREATE TRIGGER nghd_ngvl_hoad_update
ON hoadon 
AFTER UPDATE
AS 
IF (UPDATE (manv) OR UPDATE (nghd))
BEGIN
	DECLARE @ng_hoadon smalldatetime 
	DECLARE @ng_vaolam smalldatetime 
	SELECT @ng_hoadon=nghd, @ng_vaolam=ngvl
	FROM NHANVIEN, inserted
	WHERE NHANVIEN.MANV=inserted.MANV
	IF @ng_hoadon< @ng_vaolam
	BEGIN
		rollback transaction
		print  N'Ngày hóa đơn phải lớn hơn ngày vào làm'
	END
END


CREATE TRIGGER nghd_ngvl_nhanvien_update
ON nhanvien
AFTER UPDATE
AS 
	DECLARE @ng_vaolam smalldatetime, @manhvien char(4)
	SELECT @ng_vaolam=ngvl, @manhvien=manv
	FROM inserted
IF (UPDATE (ngvl))
BEGIN
	IF (EXISTS (SELECT * 
	FROM hoadon 
	WHERE manv=@manhvien AND @ng_vaolam>nghd))
	BEGIN
		rollback transaction
	print N' Thao tác sửa ngày vào làm phải nhỏ hơnn gày hóa đơn'
END
END;



CREATE TRIGGER trg_del_CTHD ON CTHD
FOR Delete
AS
BEGIN
	IF ((SELECT COUNT(*) FROM deleted WHERE SoHD = deleted.SoHD)
		= (SELECT COUNT(*) FROM HoaDon, deleted WHERE deleted.SoHD = HoaDon.SoHD))
	BEGIN
		PRINT N'Error: Mỗi một hóa đơn phải có ít nhất một chi tiết hóa đơn'
		ROLLBACK TRANSACTION
	END
END


CREATE TRIGGER trigia_hoad_insert
ON hoadon 
AFTER INSERT 
AS 
	DECLARE @trigia_hoadon money 
	DECLARE @sl_cthd int
	DECLARE @gia_monan money
	SELECT @trigia_hoadon=TRIGIA, @sl_cthd=SL,@gia_monan=GIA
	FROM CTHD, MONAN,inserted
	WHERE CTHD.MAMONAN=MONAN.MAMONAN AND CTHD.SOHD=inserted.SOHD
IF @trigia_hoadon!= SUM(@sl_cthd*@gia_monan)
BEGIN
	rollback transaction
	print N'Trị giá của một hóa đơn là tổng thành tiền (số lượng*đơn giá) của các chi tiết thuộc hóa đơn đó.'
END;

/*Insert*/
INSERT INTO NHANVIEN(MANV,HOTEN,GIOITINH,NGSINH,DCHI,LUONG,SODT,NGVL,TENNGUOIDUNG, MATKHAU) VALUES ('NV01',N'Nguyễn Như Nhựt',N'Nam','1/1/1990',N'Kp6, P.Linh Trung,TP.Thủ Đức',200000,'0927345678','13/4/2006','user01','user01')
INSERT INTO NHANVIEN (MANV,HOTEN,GIOITINH,NGSINH,DCHI,LUONG,SODT,NGVL,TENNGUOIDUNG, MATKHAU) VALUES ('NV02', N'Lê Thị Phi Yến',N'Nữ','1/2/1990',N'Kp6, P.Linh Đông,TP.Thủ Đức',200000, '0987567390', '21/4/2006','user02','user02')
INSERT INTO NHANVIEN (MANV,HOTEN,GIOITINH,NGSINH,DCHI,LUONG,SODT,NGVL,TENNGUOIDUNG, MATKHAU) VALUES ('NV03', N'Nguyễn Văn B',N'Nam','1/3/1990',N'Kp6, P.Linh Nam,TP.Thủ Đức',400000, '0997047382', '27/4/2006','admin','admin')
INSERT INTO NHANVIEN (MANV,HOTEN,GIOITINH,NGSINH,DCHI,LUONG,SODT,NGVL,TENNGUOIDUNG, MATKHAU) VALUES ('NV04', N'Ngô Thanh Tuấn',N'Nam','1/4/1990',N'Kp6, P.Linh Xuân,TP.Thủ Đức',200000, '0913758498', '24/6/2006','user04','user04')
INSERT INTO NHANVIEN (MANV,HOTEN,GIOITINH,NGSINH,DCHI,LUONG,SODT,NGVL,TENNGUOIDUNG, MATKHAU) VALUES ('NV05', N'Nguyễn Thị Trúc Thanh',N'Nữ','1/5/1990',N'Kp6, P.Linh Tây,TP.Thủ Đức',200000, '0918590387', '20/7/2006','user05','user05')

INSERT INTO MONAN(MAMONAN,TENMONAN,GIA) VALUES ('M01',N'Súp cá hồi',50000)
INSERT INTO MONAN(MAMONAN,TENMONAN,GIA) VALUES ('M02',N'Rau bí xào',26000)
INSERT INTO MONAN(MAMONAN,TENMONAN,GIA) VALUES ('M03',N'Nộm rau má',26500)
INSERT INTO MONAN(MAMONAN,TENMONAN,GIA) VALUES ('M04',N'Khoai tây chiên',30000)
INSERT INTO MONAN(MAMONAN,TENMONAN,GIA) VALUES ('M05',N'Bánh bao chiên',30500)

INSERT INTO BANAN(MABANAN,LOAI) VALUES ('B01',N'2 chỗ')
INSERT INTO BANAN(MABANAN,LOAI) VALUES ('B02',N'4 chỗ')
INSERT INTO BANAN(MABANAN,LOAI) VALUES ('B03',N'6 chỗ')
INSERT INTO BANAN(MABANAN,LOAI) VALUES ('B04',N'8 chỗ')
INSERT INTO BANAN(MABANAN,LOAI) VALUES ('B05',N'10 chỗ')

INSERT INTO HOADON(SOHD,MABANAN,MANV,NGHD,TRIGIA) VALUES (1001,'B01','NV01','1/1/2008',100000)

INSERT INTO CTHD(SOHD,MAMONAN,SL) VALUES(1001,'M01',2)






