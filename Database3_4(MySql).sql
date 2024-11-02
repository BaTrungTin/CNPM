CREATE DATABASE QuanLy;
USE QuanLy;

-- FeedBack
CREATE TABLE FeedBack 
(
    MaKH INT NOT NULL,
    MaDH INT NOT NULL,
    MaRate VARCHAR(50) NULL,
    Rating INT NULL,
    Comment CHAR(10) NULL,
    CreDate DATETIME NULL,
    CreBy CHAR(10) NULL,
    PRIMARY KEY (MaKH)
);

-- KhachHang
CREATE TABLE KhachHang 
(
    MaKH INT NULL,
    TenKH VARCHAR(255) NULL,
    Email VARCHAR(255) NULL,
    DienThoai INT NULL,
    DiaChi VARCHAR(255) NULL,
    OrderTime DATETIME NULL,
    OrderBy VARCHAR(250) NULL,
    LsOrder DATETIME NULL
);