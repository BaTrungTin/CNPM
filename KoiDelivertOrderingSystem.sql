DROP DATABASE IF EXISTS KoiDeliveryOrdering;
CREATE DATABASE IF NOT EXISTS KoiDeliveryOrdering;
USE KoiDeliveryOrdering;

-- Bảng Hồ Sơ Nhân Viên
CREATE TABLE Staff 
(
  StaffID INT AUTO_INCREMENT PRIMARY KEY,
  AccountID INT NOT NULL,
  FullName VARCHAR(45) NOT NULL,
  Department ENUM('Sales', 'Support', 'Management') NOT NULL,
  Phone VARCHAR(15) NOT NULL,
  Email VARCHAR(255) NOT NULL,
  Status TINYINT DEFAULT 1,
  FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

-- Quản lý nhân viên giao hàng
CREATE TABLE Driver 
(
    DriverID INT AUTO_INCREMENT PRIMARY KEY,
    AccountID INT,
    FullName VARCHAR(255) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    LicenseNumber VARCHAR(50) NOT NULL,
    VehicleType VARCHAR(50),
    DriverStatus ENUM('Available', 'Busy', 'Offline') DEFAULT 'Offline',
    Rating DECIMAL(3,2) DEFAULT 0,
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);




-- Quản lý Bảng Giá
CREATE TABLE PriceList 
(
    PriceID INT AUTO_INCREMENT PRIMARY KEY,
    ServicePackageID INT,
    WeightRange VARCHAR(50) NOT NULL,
    Distance VARCHAR(50) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    EffectiveDate DATETIME NOT NULL,
    EndDate DATETIME,
    Status TINYINT DEFAULT 1,
    FOREIGN KEY (ServicePackageID) REFERENCES ServicePackage(PackageID)
);

