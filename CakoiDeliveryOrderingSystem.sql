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

-- Quản lý Voucher
CREATE TABLE Voucher 
(
    VoucherID INT AUTO_INCREMENT PRIMARY KEY,
    VoucherCode VARCHAR(50) UNIQUE NOT NULL,
    DiscountType ENUM('Percentage', 'Fixed') NOT NULL,
    DiscountValue DECIMAL(10,2) NOT NULL,
    MinimumOrderValue DECIMAL(10,2),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Status TINYINT DEFAULT 1
);

-- Quản lý Đơn Hàng
CREATE TABLE Orders 
(
    OrderID INT AUTO_INCREMENT PRIMARY KEY,    
	Weight DECIMAL(10, 2) NOT NULL,
    Quantity INT NOT NULL,
    CustomerID INT NOT NULL,
    ServicePackageID INT NOT NULL,
    VoucherID INT,
    DriverID INT,
    DeliveryVehicle VARCHAR(45),
    DeliveryDate DATETIME,
    PickupDate DATETIME,
    OrderStatus ENUM('Pending', 'Confirmed', 'PickingUp', 'InTransit', 'Delivered', 'Cancelled') DEFAULT 'Pending',
    DeliveryAddress VARCHAR(255),
    PickupAddress VARCHAR(255),
    TotalPrice DECIMAL(10, 2) NOT NULL,
    PaymentMethod VARCHAR(50),
    LastUpdate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (VoucherID) REFERENCES Voucher(VoucherID),
    FOREIGN KEY (DriverID) REFERENCES Driver(DriverID)
);

-- Quản lý Đơn Vị Vận Chuyển
CREATE TABLE ServicePackage 
(
    PackageID INT AUTO_INCREMENT PRIMARY KEY,
    PackageName VARCHAR(100) NOT NULL,
    Description TEXT,
    ShippingType ENUM('Domestic', 'International') NOT NULL,
    BasePrice DECIMAL(10,2) NOT NULL,
    Status TINYINT DEFAULT 1
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

-- Theo Dõi Vận Chuyển
CREATE TABLE Shipment_Tracking
(
    TrackingID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    Status ENUM('OrderPlaced', 'PickedUp', 'InTransit', 'OutForDelivery', 'Delivered', 'Returned', 'Cancelled') NOT NULL,
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    Location VARCHAR(255),
    Notes TEXT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
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
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
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

CREATE TABLE Driver_Assignment
(
    AssignmentID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    DriverID INT NOT NULL,
    AssignedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Status ENUM('Assigned', 'InTransit', 'Completed', 'Cancelled') DEFAULT 'Assigned',
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (DriverID) REFERENCES Driver(DriverID),
    CONSTRAINT unique_order_driver UNIQUE (OrderID, DriverID)
);

-- Quản lý phương thức thanh toán 
CREATE TABLE Payment
(
    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    PaymentMethod ENUM('CreditCard', 'DebitCard', 'PayPal', 'CashOnDelivery', 'BankTransfer') NOT NULL,
    PaymentStatus ENUM('Pending', 'Completed', 'Failed', 'Refunded') DEFAULT 'Pending',
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Amount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

ALTER TABLE CustomerSupport 
ADD DriverID INT, 
ADD CONSTRAINT fk_customersupport_driver FOREIGN KEY (DriverID) REFERENCES Driver(DriverID);
