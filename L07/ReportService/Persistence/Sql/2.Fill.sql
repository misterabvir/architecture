-- Вставка данных в таблицу Company
INSERT INTO Company (Name, Address, TaxId) VALUES
('OptoTrade Inc.', '123 Trade Ave, Suite 456', 'TAX123456'),
('Global Wholesale LLC', '789 Market St, Floor 10', 'TAX789101');

-- Вставка данных в таблицу ProductCategory
INSERT INTO ProductCategory (Name, CompanyId) VALUES
('Electronics', 1),
('Household', 1),
('Groceries', 2),
('Office Supplies', 2);

-- Вставка данных в таблицу Product
INSERT INTO Product (Name, Price, Quantity, CategoryId) VALUES
('Smartphone', 299.99, 150, 1),
('Laptop', 899.99, 50, 1),
('Vacuum Cleaner', 149.99, 100, 2),
('LED Bulb', 4.99, 500, 2),
('Pasta', 1.99, 2000, 3),
('Printer Paper', 5.99, 1000, 4);

-- Вставка данных в таблицу Report
INSERT INTO Report (Title, CompanyId) VALUES
('Q1 2024 Financial Report', 1),
('Monthly Sales Report - July 2024', 2);

-- Вставка данных в таблицу ReportTable
INSERT INTO ReportTable (Title, ReportId) VALUES
('Q1 Sales Data', 1),
('July Sales by Category', 2);

-- Вставка данных в таблицу ReportTableRow (для простоты строки хранятся в формате CSV)
INSERT INTO ReportTableRow (TableId, RowData) VALUES
(1, 'Product A,100,299.99'),
(1, 'Product B,50,899.99'),
(2, 'Electronics,1500'),
(2, 'Household,1000');

-- Вставка данных в таблицу Chart
INSERT INTO Chart (Title, Type, Labels, 'Values', ReportId) VALUES
('Sales Growth', 'Line', '["January", "February", "March"]', '[5000, 7000, 8500]', 1),
('Category Breakdown', 'Pie', '["Electronics", "Household", "Groceries"]', '[40, 35, 25]', 2);

-- Вставка данных в таблицу FinancialSummary
INSERT INTO FinancialSummary (Revenue, Expenses, ReportId) VALUES
(25000.00, 15000.00, 1),
(12000.00, 8000.00, 2);
