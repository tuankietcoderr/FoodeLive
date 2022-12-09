CREATE DATABASE QUANLYNHAHANG
USE QUANLYNHAHANG
set dateformat dmy

CREATE TABLE NHANVIEN
(
	MANV CHAR(4) NOT NULL,
	HOTEN NVARCHAR(40),
	GIOITINH VARCHAR(10),
	NGSINH SMALLDATETIME,
	DCHI VARCHAR(50),
	LUONG MONEY,
	SODT VARCHAR(20),
	NGVL SMALLDATETIME,
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
	NGHD SMALLDATETIME,
	MABANAN CHAR(4),
	MANV CHAR(4),
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


/*Giá bán của sản phẩm từ 25000 đồng trở lên*/
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




/*Trị giá của một hóa đơn là tổng thành tiền (số lượng*đơn giá) của các chi tiết thuộc hóa đơn đó.
*/

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


INSERT INTO NHANVIEN(MANV,HOTEN,SODT,NGVL) VALUES ('NV01','Nguyen Nhu Nhut','0927345678','13/4/2006')
INSERT INTO NHANVIEN (MANV, HOTEN, SODT, NGVL) VALUES ('NV02', 'Le Thi Phi Yen', '0987567390', '21/4/2006')
INSERT INTO NHANVIEN (MANV, HOTEN, SODT, NGVL) VALUES ('NV03', 'Nguyen Van B', '0997047382', '27/4/2006')
INSERT INTO NHANVIEN (MANV, HOTEN, SODT, NGVL) VALUES ('NV04', 'Ngo Thanh Tuan', '0913758498', '24/6/2006')
INSERT INTO NHANVIEN (MANV, HOTEN, SODT, NGVL) VALUES ('NV05', 'Nguyen Thi Truc Thanh', '0918590387', '20/7/2006')
