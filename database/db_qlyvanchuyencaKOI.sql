CREATE DATABASE qlyvanchuyencakoi;

-- Table for user information
CREATE TABLE qlyvanchuyencakoi.info_user (
    id_user INT PRIMARY KEY AUTO_INCREMENT,
    name TEXT CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
    user_name VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    age INT NOT NULL,
    email VARCHAR(255) NOT NULL,
    number_phone VARCHAR(20) NOT NULL,
    address TEXT CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
    access VARCHAR(50) default('user'),
    UNIQUE(password)
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for product information
CREATE TABLE qlyvanchuyencakoi.product(
    id_product INT PRIMARY KEY NOT NULL,
    product_name TEXT CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
    image VARCHAR(50) NOT NULL,
    price float NOT NULL,
    note TEXT CHARACTER SET utf8 COLLATE utf8_bin
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for shipper information
CREATE TABLE qlyvanchuyencakoi.shipper(
    id_shipper INT PRIMARY KEY NOT NULL,
    name_shipper TEXT CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
    number_phone VARCHAR(20) NOT NULL,
    license_plate VARCHAR(20) NOT NULL
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for information on shipped products
CREATE TABLE qlyvanchuyencakoi.info_product (
    id_info_product INT PRIMARY KEY NOT NULL,
    about_product int PRIMARY KEY NOT NULL,
    shipping_fee FLOAT NOT NULL, 
    all_price FLOAT NOT NULL,
    id_user_1 INT PRIMARY KEY NOT NULL,
    shipping_method ENUM('GHTK', 'GHN', 'HT') default('GHTK') NOT NULL,
    note TEXT CHARACTER SET utf8 COLLATE utf8_bin,
    status_shipping ENUM('DELIVERING', 'DELIVERED') NOT NULL,
    about_shipper INT NOT NULL,
    CONSTRAINT fk_product FOREIGN KEY (about_product) REFERENCES product(id_product),
    CONSTRAINT fk_user FOREIGN KEY (id_user_1) REFERENCES info_user(id_user),
    CONSTRAINT fk_shipper FOREIGN KEY (about_shipper) REFERENCES shipper(id_shipper)
) CHARACTER SET utf8 COLLATE utf8_bin;

-- Table for user feedback
CREATE TABLE qlyvanchuyencakoi.feed_back (
    id_feed_back INT PRIMARY KEY NOT NULL,
    id_product INT NOT NULL,
    feed_back ENUM('very good', 'good', 'normal', 'bad', 'very bad'),
    CONSTRAINT fk_product FOREIGN KEY (id_product) REFERENCES info_product(id_product)
);

-- Trigger to check feedback status before inserting
DELIMITER //
CREATE TRIGGER qlyvanchuyencakoi.check_feedback_before_insert 
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
