-- ====================================
-- DATABASE: TouchShop
-- ====================================
CREATE DATABASE TouchShop;
USE TouchShop;

-- ====================================
-- 1. Customers
-- ====================================
CREATE TABLE Customers (
    customer_id INT identity(1,1) PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ====================================
-- 2. Admins
-- ====================================
CREATE TABLE Admins (
    admin_id INT IDENTITY(1,1) PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(20) NOT NULL DEFAULT 'Manager',
    created_at DATETIME DEFAULT GETDATE(),
    CONSTRAINT chk_role CHECK (role IN ('SuperAdmin', 'Manager', 'Support'))
);


-- ====================================
-- 3. Categories
-- ====================================
CREATE TABLE Categories (
    category_id INT identity(1,1) PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

-- ====================================
-- 4. Products
-- ====================================
CREATE TABLE Products (
    product_id INT identity(1,1) PRIMARY KEY,
    category_id INT,
    product_name VARCHAR(150) NOT NULL,
    description TEXT,
    price DECIMAL(10,2) NOT NULL,
    stock_quantity INT DEFAULT 0,
    created_by_admin INT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (category_id) REFERENCES Categories(category_id),
    FOREIGN KEY (created_by_admin) REFERENCES Admins(admin_id)
);

-- ====================================
-- 5. ProductImages
-- ====================================
CREATE TABLE ProductImages (
    image_id INT identity(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    image_url VARCHAR(255) NOT NULL,
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- ====================================
-- 6. Addresses
-- ====================================
CREATE TABLE Addresses (
    address_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    address_line VARCHAR(255) NOT NULL,
    city VARCHAR(100) NOT NULL,
    state VARCHAR(100),
    postal_code VARCHAR(20) NOT NULL,
    country VARCHAR(100) NOT NULL,
    address_type VARCHAR(20) NOT NULL DEFAULT 'Shipping',
    CONSTRAINT chk_address_type CHECK (address_type IN ('Shipping', 'Billing')),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id) ON DELETE CASCADE
);


-- ====================================
-- 7. Carts
-- ====================================
CREATE TABLE Carts (
    cart_id INT identity(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

-- ====================================
-- 8. CartItems
-- ====================================
CREATE TABLE CartItems (
    cart_item_id INT identity(1,1) PRIMARY KEY,
    cart_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT DEFAULT 1,
    FOREIGN KEY (cart_id) REFERENCES Carts(cart_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- ====================================
-- 9. Orders
-- ====================================
CREATE TABLE Orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    address_id INT NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    payment_method VARCHAR(20) NOT NULL DEFAULT 'COD',
    status VARCHAR(20) NOT NULL DEFAULT 'Pending',
    created_at DATETIME DEFAULT GETDATE(),
    CONSTRAINT chk_payment_method CHECK (payment_method IN ('COD', 'Online')),
    CONSTRAINT chk_order_status CHECK (status IN ('Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled')),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY (address_id) REFERENCES Addresses(address_id)
);


-- ====================================
-- 10. OrderItems
-- ====================================
CREATE TABLE OrderItems (
    order_item_id INT identity(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- ====================================
-- 11. Reviews
-- ====================================
CREATE TABLE Reviews (
    review_id INT identity(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- ====================================
-- 12. Wishlists
-- ====================================
CREATE TABLE Wishlists (
    wishlist_id INT identity(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- ====================================
-- 13. Coupons
-- ====================================
CREATE TABLE Coupons (
    coupon_id INT identity(1,1) PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    discount_percent DECIMAL(5,2) DEFAULT 0.00,
    valid_from DATETIME NOT NULL,
    valid_to DATETIME NOT NULL,
    created_by_admin INT,
    FOREIGN KEY (created_by_admin) REFERENCES Admins(admin_id) ON DELETE SET NULL
);

-- ====================================
-- 14. Shipping
-- ====================================
CREATE TABLE Shipping (
    shipping_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    courier_name VARCHAR(100),
    tracking_number VARCHAR(100),
    shipping_status VARCHAR(20) NOT NULL DEFAULT 'Pending',
    shipped_at DATETIME,
    delivered_at DATETIME,
    CONSTRAINT chk_shipping_status CHECK (shipping_status IN ('Pending', 'In Transit', 'Delivered')),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id) ON DELETE CASCADE
);


-- ====================================
-- 15. Reports
-- ====================================
CREATE TABLE Reports (
    report_id INT IDENTITY(1,1) PRIMARY KEY,
    report_type VARCHAR(20) NOT NULL,
    generated_by INT,
    generated_at DATETIME DEFAULT GETDATE(),
    file_path VARCHAR(255),
    CONSTRAINT chk_report_type CHECK (report_type IN ('Sales', 'Inventory', 'Customer')),
    FOREIGN KEY (generated_by) REFERENCES Admins(admin_id) ON DELETE SET NULL
);

