

-- -----------------------------------------------------
-- Schema booking
-- -----------------------------------------------------
DROP DATABASE IF EXISTS `booking`;

CREATE DATABASE IF NOT EXISTS `booking`;

USE `booking` ;

CREATE TABLE restaurants (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    address VARCHAR(255),
    phone VARCHAR(20),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tables (
    id INT AUTO_INCREMENT PRIMARY KEY,
    restaurant_id INT NOT NULL,
    table_number INT NOT NULL,
    capacity INT NOT NULL,
    location VARCHAR(255),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (restaurant_id) REFERENCES restaurants(id)
);

DROP TABLE reservations;

INSERT INTO reservations (table_id, reservation_date, customer_name, customer_email, customer_phone, guest_count)
VALUES(3, '2024-12-31', "name", "email", "Phone", 2);
SELECT LAST_INSERT_ID();

UPDATE TABLE reservations 
SET 
    status = 'confirmed'
WHERE id = @ReservationId
;