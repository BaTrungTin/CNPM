DROP DATABASE IF EXISTS KoiDeliveryOrdering;
CREATE DATABASE IF NOT EXISTS KoiDeliveryOrdering;
USE KoiDeliveryOrdering;

DROP TRIGGER IF EXISTS before_account_insert;
DROP TRIGGER IF EXISTS before_account_update;

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
    CONSTRAINT fk_feedback_OrderID FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);