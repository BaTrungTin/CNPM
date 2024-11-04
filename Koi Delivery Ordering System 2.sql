CREATE DATABASE IF NOT EXISTS Koi_Delivery_Ordering;
USE Koi_Delivery_Ordering;

DROP DATABASE Koi_Delivery_Ordering;

-- Xóa triggers nếu đã tồn tại
DROP TRIGGER IF EXISTS before_account_insert; 
DROP TRIGGER IF EXISTS before_account_update;

-- Tạo TRIGGER kiểm tra định dạng mật khẩu khi INSERT 
DELIMITER //
CREATE TRIGGER before_account_insert
BEFORE INSERT ON Account
FOR EACH ROW
BEGIN
    CASE 
        WHEN NEW.Role = 'Staff' THEN
            IF NOT (NEW.Password REGEXP '^STF[a-zA-Z0-9]{6,}01$') THEN
                SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Staff password must start with STF and end with 01';
            END IF;
        WHEN NEW.Role = 'Driver' THEN
            IF NOT (NEW.Password REGEXP '^DRV[a-zA-Z0-9]{6,}02$') THEN
                SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Driver password must start with DRV and end with 02';
            END IF;
        WHEN NEW.Role = 'Manager' THEN
            IF NOT (NEW.Password REGEXP '^MGR[a-zA-Z0-9]{6,}03$') THEN
                SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Manager password must start with MGR and end with 03';
            END IF;
        WHEN NEW.Role = 'Customer' THEN
            IF LENGTH(NEW.Password) < 6 THEN
                SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Customer password must be at least 6 characters long';
            END IF;
    END CASE;
END//

-- Tạo TRIGGER kiểm tra định dạng mật khẩu khi UPDATE
CREATE TRIGGER before_account_update
BEFORE UPDATE ON Account
FOR EACH ROW
BEGIN
    IF NEW.Password != OLD.Password THEN
        CASE 
            WHEN NEW.Role = 'Staff' THEN
                IF NOT (NEW.Password REGEXP '^STF[a-zA-Z0-9]{6,}01$') THEN
                    SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Staff password must start with STF and end with 01';
                END IF;
            WHEN NEW.Role = 'Driver' THEN
                IF NOT (NEW.Password REGEXP '^DRV[a-zA-Z0-9]{6,}02$') THEN
                    SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Driver password must start with DRV and end with 02';
                END IF;
            WHEN NEW.Role = 'Manager' THEN
                IF NOT (NEW.Password REGEXP '^MGR[a-zA-Z0-9]{6,}03$') THEN
                    SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Manager password must start with MGR and end with 03';
                END IF;
            WHEN NEW.Role = 'Customer' THEN
                IF LENGTH(NEW.Password) < 6 THEN
                    SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Customer password must be at least 6 characters long';
                END IF;
        END CASE;
    END IF;
END//

CREATE TRIGGER before_staff_insert 
BEFORE INSERT ON Staff
FOR EACH ROW
BEGIN
    DECLARE role ENUM('Customer', 'Staff', 'Driver', 'Manager');
    SELECT Role INTO role FROM Account WHERE AccountID = NEW.AccountID;
    IF role NOT IN ('Staff', 'Manager') THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Account must be Staff or Manager to be associated with Staff';
    END IF;
END//

CREATE TRIGGER before_staff_update
BEFORE UPDATE ON Staff
FOR EACH ROW
BEGIN
    DECLARE role ENUM('Customer', 'Staff', 'Driver', 'Manager');
    SELECT Role INTO role FROM Account WHERE AccountID = NEW.AccountID;
    IF role NOT IN ('Staff', 'Manager') THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Account must be Staff or Manager to be associated with Staff';
    END IF;
END//

CREATE TRIGGER before_driver_insert 
BEFORE INSERT ON Driver
FOR EACH ROW
BEGIN
    DECLARE role ENUM('Customer', 'Staff', 'Driver', 'Manager');
    SELECT Role INTO role FROM Account WHERE AccountID = NEW.AccountID;
    IF role != 'Driver' THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Account must be Driver to be associated with Driver';
    END IF;
END//

CREATE TRIGGER before_driver_update
BEFORE UPDATE ON Driver
FOR EACH ROW
BEGIN
    DECLARE role ENUM('Customer', 'Staff', 'Driver', 'Manager');
    SELECT Role INTO role FROM Account WHERE AccountID = NEW.AccountID;
    IF role != 'Driver' THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Account must be Driver to be associated with Driver';
    END IF;
END//

DELIMITER ;

-- Website Content Management
CREATE TABLE Content 
(
    ContentID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL,
    Type ENUM('Blog', 'News', 'FAQ', 'Policy', 'Service', 'About') NOT NULL,
    Status ENUM('Draft', 'Published', 'Archived') DEFAULT 'Draft',
    Author VARCHAR(100) NOT NULL,
    PublishDate DATETIME,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Account Management
CREATE TABLE Account 
(
    AccountID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Role ENUM('Customer', 'Staff', 'Driver', 'Manager') DEFAULT 'Customer',
    AccountStatus TINYINT DEFAULT 1,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Bảng Quản Lý Khách Hàng
CREATE TABLE Customer
(
    CustomerID INT AUTO_INCREMENT PRIMARY KEY,
    Fullname VARCHAR(255) NULL,
    Email VARCHAR(255) NULL,
    Phone VARCHAR(15) NULL,
    Address VARCHAR(255) NULL,
    AccountID INT,
    CONSTRAINT fk_Customer_Account FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

-- Bảng Lịch Sử Đặt Đơn
CREATE TABLE Order_History 
(
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderTime DATETIME NOT NULL,
    OrderStatus VARCHAR(50) NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    PaymentMethod VARCHAR(50),
    Address VARCHAR(255) NULL,
    LastUpdate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_order_customerid FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- Service Package Management
CREATE TABLE ServicePackage 
(
    PackageID INT AUTO_INCREMENT PRIMARY KEY,
    PackageName VARCHAR(100) NOT NULL,
    Description TEXT,
    ShippingType ENUM('Domestic', 'International') NOT NULL,
    BasePrice DECIMAL(10,2) NOT NULL,
    Status TINYINT DEFAULT 1
);

-- Bảng Quản Lý Giá
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

-- Voucher Management
CREATE TABLE Voucher 
(
    VoucherID INT AUTO_INCREMENT PRIMARY KEY,
    VoucherCode VARCHAR(50) UNIQUE NOT NULL,
    DiscountType ENUM('Percentage', 'Fixed') NOT NULL,
    DiscountValue DECIMAL(10,2) NOT NULL,
    MinimumOrderValue DECIMAL(10,2),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    UsageLimit INT,
    UsageCount INT DEFAULT 0,
    Status TINYINT DEFAULT 1
);

-- Bảng Sắp Xếp Vận Chuyển
CREATE TABLE Order_Management 
(
  OrderID INT NOT NULL,
  CustomerID INT NOT NULL,
  ServicePackageID VARCHAR(45) NOT NULL,
  VoucherID INT NULL,
  DeliveryVehicle VARCHAR(45) NOT NULL,
  DeliveryDate DATETIME NOT NULL,
  PickupDate DATETIME NOT NULL,
  OrderStatus ENUM('Pending', 'Confirmed', 'PickingUp', 'InTransit', 'Delivered', 'Cancelled') DEFAULT 'Pending',
  DeliveryAddress VARCHAR(255) NOT NULL,
  PickupAddress VARCHAR(255) NOT NULL,
  PRIMARY KEY (OrderID),
  FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
  FOREIGN KEY (VoucherID) REFERENCES Voucher(VoucherID)
);

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

-- Bảng quản lý Quốc gia
CREATE TABLE Country 
(
    CountryID INT AUTO_INCREMENT PRIMARY KEY,
    CountryCode VARCHAR(3) NOT NULL UNIQUE,
    CountryName VARCHAR(100) NOT NULL,
    Status TINYINT DEFAULT 1,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Bảng quản lý Tỉnh/Thành phố
CREATE TABLE Province 
(
    ProvinceID INT AUTO_INCREMENT PRIMARY KEY,
    CountryID INT NOT NULL,
    ProvinceCode VARCHAR(5) NOT NULL UNIQUE,
    ProvinceName VARCHAR(100) NOT NULL,
    Status TINYINT DEFAULT 1,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (CountryID) REFERENCES Country(CountryID)
);

-- Bảng quản lý Quận/Huyện
CREATE TABLE District 
(
    DistrictID INT AUTO_INCREMENT PRIMARY KEY,
    ProvinceID INT NOT NULL,
    DistrictCode VARCHAR(5) NOT NULL UNIQUE,
    DistrictName VARCHAR(100) NOT NULL,
    Status TINYINT DEFAULT 1,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (ProvinceID) REFERENCES Province(ProvinceID)
);

-- Bảng quản lý Phường/Xã
CREATE TABLE Ward 
(
    WardID INT AUTO_INCREMENT PRIMARY KEY,
    DistrictID INT NOT NULL,
    WardCode VARCHAR(5) NOT NULL UNIQUE,
    WardName VARCHAR(100) NOT NULL,
    Status TINYINT DEFAULT 1,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (DistrictID) REFERENCES District(DistrictID)
);

-- Review Management
CREATE TABLE Review 
(
    ReviewID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    CustomerID INT NOT NULL,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    Comment TEXT,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (OrderID) REFERENCES Order_Management(OrderID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- Customer Support
CREATE TABLE CustomerSupport 
(
    SupportID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    CustomerID INT NOT NULL,
    IssueType VARCHAR(100) NOT NULL,
    Description TEXT,
    Status ENUM('Open', 'InProgress', 'Resolved', 'Closed') DEFAULT 'Open',
    HandledBy INT,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ResolvedDate DATETIME,
    FOREIGN KEY (OrderID) REFERENCES Order_Management(OrderID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (HandledBy) REFERENCES Staff(StaffID)
);

-- Report Management
CREATE TABLE Report 
(
    ReportID INT AUTO_INCREMENT PRIMARY KEY,
    ReportType ENUM('Sales', 'Delivery', 'Customer', 'Driver', 'Performance') NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    ReportData JSON,
    CreatedBy INT,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CreatedBy) REFERENCES Staff(StaffID)
);

-- Bảng Quản Lý Feedback
CREATE TABLE FeedBack 
(
    FeedBackID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderID INT NOT NULL,
    CommentID VARCHAR(50) NULL,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    Comment VARCHAR(255) NULL,
    CreatedDate DATETIME NULL,
    CreatedBy VARCHAR(50) NULL,
    CONSTRAINT fk_feedback_CustomerID FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT fk_feedback_OrderID FOREIGN KEY (OrderID) REFERENCES Order_History(OrderID)
);