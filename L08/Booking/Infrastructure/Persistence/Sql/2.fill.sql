USE `booking`;

INSERT INTO restaurants (name, address, phone, created_at) VALUES
('Ocean Breeze', '123 Seaside Blvd, Miami, FL', '305-555-1234', NOW()),
('Mountain View', '456 Summit Road, Denver, CO', '720-555-5678', NOW()),
('City Lights', '789 Downtown Ave, New York, NY', '212-555-9101', NOW());


INSERT INTO tables (restaurant_id, table_number, capacity, location, created_at) VALUES
(1, 1, 4, 'Main Hall', NOW()),
(1, 2, 2, 'Terrace', NOW()),
(1, 3, 6, 'Main Hall', NOW()),
(1, 4, 2, 'Private Room', NOW()),
(2, 1, 4, 'Main Hall', NOW()),
(2, 2, 2, 'Balcony', NOW()),
(2, 3, 6, 'Main Hall', NOW()),
(2, 4, 8, 'Banquet Room', NOW()),
(3, 1, 4, 'Main Hall', NOW()),
(3, 2, 2, 'Rooftop', NOW()),
(3, 3, 6, 'Main Hall', NOW());