CREATE TABLE info_user (
    id_user INT PRIMARY KEY,
    name TEXT CHARACTER SET utf8 COLLATE utf_bin NOT NULL,
    user_name VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    age INT,
    email VARCHAR(255),
    number_phone VARCHAR(20),
    address TEXT CHARACTER SET utf8 COLLATE utf_bin,
    access VARCHAR(50)
)CHARACTER SET utf8 COLLATE utf_bin;

CREATE TABLE product(
	id_product int PRIMARY KEY,
	produc_name TEXT CHARACTER SET utf8 COLLATE utf_bin,
	image VARCHAR(50),
	price double,
	note TEXT CHARACTER SET utf8 COLLATE utf_bin
)CHARACTER SET utf8 COLLATE utf_bin;

CREATE TABLE shipper(
	id_shiper int primary key,
	name_shipper text,
	number_phone text,
	license_plate int
)CHARACTER SET utf8 COLLATE utf_bin;

CREATE Table info_product
(
  id_product int PRIMARY KEY,
  name_product TEXT CHARACTER SET utf8 COLLATE utf_bin,
  shipping_fee float,
  price float,
  all_price float,
  id_user_1 int PRIMARY KEY,
  address TEXT CHARACTER SET utf8 COLLATE utf_bin,
  shipping_method enum('GHTK','GHN','HT'),
  note TEXT CHARACTER SET utf8 COLLATE utf_bin,
  status_shipping enum('DELIVERING','DELIVERED'),
  about_shipper INT PRIMARY KEY,
  CONSTRAINT fk_user FOREIGN KEY (id_user_1) REFERENCES info_user(id_user), -- gọi đến id người dùng 
  CONSTRAINT fk_shipper FOREIGN KEY (about_shipper) REFERENCES shipper(id_shipper) 
)CHARACTER SET utf8 COLLATE utf_bin;

create table feed_back
(
  id_feed_back INT PRIMARY KEY,
    id_product INT,
    feed_back enum('verry good','good','normal','bad','verry bad'),
	CONSTRAINT fk_product FOREIGN KEY (id_product) REFERENCES info_product(id_product)
);

DELIMITER //
CREATE TRIGGER check_feedback_before_insert
BEFORE INSERT ON feed_back
FOR EACH ROW
BEGIN
    DECLARE shipping_status ENUM('DELIVERING', 'DELIVERED');
    -- Lấy trạng thái vận chuyển của sản phẩm
    SELECT status_shipping INTO shipping_status 
    FROM info_product 
    WHERE id_product = NEW.id_product;

    -- Kiểm tra trạng thái, nếu không phải 'DELIVERED' thì thông báo lỗi
    IF shipping_status != 'DELIVERED' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Feedback can only be added if the product is delivered.';
    END IF;
END //
DELIMITER ;

