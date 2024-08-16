
-- Таблица для хранения информации о компании
CREATE TABLE Company (
    CompanyId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Address TEXT NOT NULL,
    TaxId TEXT NOT NULL
);

-- Таблица для хранения товарных категорий
CREATE TABLE ProductCategory (
    CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    CompanyId INTEGER,
    FOREIGN KEY (CompanyId) REFERENCES Company(CompanyId)
);

-- Таблица для хранения товаров
CREATE TABLE Product (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Quantity INTEGER NOT NULL,
    CategoryId INTEGER,
    FOREIGN KEY (CategoryId) REFERENCES ProductCategory(CategoryId)
);

-- Таблица для хранения отчетов
CREATE TABLE Report (
    ReportId INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    CompanyId INTEGER,
    FOREIGN KEY (CompanyId) REFERENCES Company(CompanyId)
);

-- Таблица для хранения таблиц, включенных в отчеты
CREATE TABLE ReportTable (
    TableId INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    ReportId INTEGER,
    FOREIGN KEY (ReportId) REFERENCES Report(ReportId)
);

-- Таблица для хранения строк данных в таблицах отчетов
CREATE TABLE ReportTableRow (
    RowId INTEGER PRIMARY KEY AUTOINCREMENT,
    TableId INTEGER,
    RowData TEXT NOT NULL,  -- JSON или CSV для хранения строки данных
    FOREIGN KEY (TableId) REFERENCES ReportTable(TableId)
);

-- Таблица для хранения графиков, включенных в отчеты
CREATE TABLE Chart (
    ChartId INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Type TEXT NOT NULL,
    Labels TEXT NOT NULL,  -- JSON для хранения меток оси X
    'Values' TEXT NOT NULL,  -- JSON для хранения значений данных
    ReportId INTEGER,
    FOREIGN KEY (ReportId) REFERENCES Report(ReportId)
);

-- Таблица для хранения финансовых данных
CREATE TABLE FinancialSummary (
    SummaryId INTEGER PRIMARY KEY AUTOINCREMENT,
    Revenue DECIMAL(10, 2) NOT NULL,
    Expenses DECIMAL(10, 2) NOT NULL,
    ReportId INTEGER,
    FOREIGN KEY (ReportId) REFERENCES Report(ReportId)
);
