CREATE DATABASE qlyvanchuyencakoi;

-- Table for user information
CREATE TABLE info_user (
    id_user INT PRIMARY KEY,
    name TEXT CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
    user_name VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    age INT,
    email VARCHAR(255),
    number_phone VARCHAR(20) NOT NULL,
    address TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    access VARCHAR(50)
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for product information
CREATE TABLE product(
    id_product INT PRIMARY KEY,
    product_name TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    image VARCHAR(50),
    price DOUBLE,
    note TEXT CHARACTER SET utf8 COLLATE utf8_bin
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for shipper information
CREATE TABLE shipper(
    id_shipper INT PRIMARY KEY,
    name_shipper TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    number_phone VARCHAR(20),
    license_plate VARCHAR(20)
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for information on shipped products
CREATE TABLE info_product (
    id_product INT PRIMARY KEY,
    name_product TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    shipping_fee FLOAT,
    price FLOAT,
    all_price FLOAT,
    id_user_1 INT,
    address TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    shipping_method ENUM('GHTK', 'GHN', 'HT'),
    note TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    status_shipping ENUM('DELIVERING', 'DELIVERED'),
    about_shipper INT,
    CONSTRAINT fk_user FOREIGN KEY (id_user_1) REFERENCES info_user(id_user),
    CONSTRAINT fk_shipper FOREIGN KEY (about_shipper) REFERENCES shipper(id_shipper)
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for user feedback
CREATE TABLE feed_back (
    id_feed_back INT PRIMARY KEY,
    id_product INT,
    feed_back ENUM('very good', 'good', 'normal', 'bad', 'very bad'),
    CONSTRAINT fk_product FOREIGN KEY (id_product) REFERENCES info_product(id_product)
);

-- Trigger to check feedback status before inserting
DELIMITER //
CREATE TRIGGER check_feedback_before_insert 
BEFORE INSERT ON feed_back
FOR EACH ROW
BEGIN
    DECLARE shipping_status ENUM('DELIVERING', 'DELIVERED');
    -- Fetch the shipping status of the product
    SELECT status_shipping INTO shipping_status
    FROM info_product
    WHERE id_product = NEW.id_product;
    
    -- Check the shipping status, if not 'DELIVERED' raise an error
    IF shipping_status != 'DELIVERED' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Feedback can only be added if the product is delivered.';
    END IF;
END //
DELIMITER ;
