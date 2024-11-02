CREATE DATABASE Koi_Shipping;

CREATE SCHEMA Koi_fish;

CREATE TABLE `koi_fish`.`bang_gia` (
  `Ma_khach_hang` INT NOT NULL,
  `Ma_don_hang` INT NOT NULL,
  `Loai_hinh_van_chuyen` VARCHAR(45) NOT NULL,
  `Chi phi` INT NOT NULL,
  `Phu_phi` VARCHAR(45) NULL,
  `Thoi_han_thanh_toan` DATETIME NOT NULL,
  `Hinh thuc thanh toan` VARCHAR(45) NOT NULL,
  `Tong_gia_tri` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Ma_don_hang`));
  
CREATE TABLE `sap_xep_van_chuyen` (
  `Ma_don_hang` int NOT NULL,
  `Ma_van_chuyen` varchar(45) NOT NULL,
  `Phuong_tien_van_chuyen` varchar(45) NOT NULL,
  `Ngay_giao` datetime NOT NULL,
  `Ngay_nhan` datetime NOT NULL,
  `Trang_thai_don_hang` varchar(45) NOT NULL,
  `Dia_chi_giao_hang` varchar(45) NOT NULL,
  `Dia_chi_nhan_hang` varchar(45) NOT NULL,
  PRIMARY KEY (`Ma_don_hang`));
  
CREATE TABLE `koi_fish`.`ho_so_nhan_vien` (
  `ma_nhan_vien` INT NOT NULL,
  `ten_nhan_vien` VARCHAR(45) NOT NULL,
  `thong_tin_lien_lac` VARCHAR(45) NOT NULL,
  `ngay_tham_gia` DATETIME NOT NULL,
  `thanh_tich` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ma_nhan_vien`));

CREATE TABLE `koi_fish`.`dashboard & report` (
  `ma_bao_cao` INT NOT NULL,
  `loai_bao_cao` VARCHAR(45) NOT NULL,
  `ngay_tao` DATETIME NOT NULL,
  `thong_tin` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ma_bao_cao`));	
