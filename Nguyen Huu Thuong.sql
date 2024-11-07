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

-- Order Management
CREATE TABLE Order_Management
(
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    ServicePackageID INT NOT NULL,
    VoucherID INT,
    OrderNumber VARCHAR(50) UNIQUE NOT NULL,
    PickupAddress TEXT NOT NULL,
    DeliveryAddress TEXT NOT NULL,
    PickupContact VARCHAR(255) NOT NULL,
    DeliveryContact VARCHAR(255) NOT NULL,
    PickupZoneID INT,
    DeliveryZoneID INT,
    IntZoneID INT,
    PartnerID INT,
    RouteID INT,
    ShippingType ENUM('Domestic', 'International') NOT NULL,
    TotalWeight DECIMAL(10,2) NOT NULL,
    KoiQuantity INT NOT NULL,
    SpecialInstructions TEXT,
    RequiredDocuments TEXT,
    OrderStatus ENUM('Pending', 'Confirmed', 'PickingUp', 'InTransit', 'Delivered', 'Cancelled') DEFAULT 'Pending',
    TotalAmount DECIMAL(10,2) NOT NULL,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    FinalAmount DECIMAL(10,2) NOT NULL,
    CustomsFee DECIMAL(10,2) DEFAULT 0,
    InsuranceFee DECIMAL(10,2) DEFAULT 0,
    HandlingFee DECIMAL(10,2) DEFAULT 0,
    PaymentMethod VARCHAR(50),
    PaymentStatus ENUM('Pending', 'Paid', 'Failed') DEFAULT 'Pending',
    HandledBy INT,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (ServicePackageID) REFERENCES ServicePackage(PackageID),
    FOREIGN KEY (VoucherID) REFERENCES Voucher(VoucherID)
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
