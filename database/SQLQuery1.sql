CREATE TABLE [Login](
	ID VARCHAR(255) NOT NULL PRIMARY KEY,
	Password VARCHAR(255) NOT NULL
);

CREATE TABLE [Member](
	ID INT IDENTITY (1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	Contact_No VARCHAR(12) NOT NULL
);
DROP TABLE [Member];
CREATE TABLE [Book](
	ID INT IDENTITY (1,1) PRIMARY KEY, 
	Title VARCHAR (255) NOT NULL,
	Publisher VARCHAR (255) NOT NULL,
	Author VARCHAR (255) NOT NULL,
	ISBN VARCHAR (255) NOT NULL,
	Category VARCHAR (255) NOT NULL,
	QTY INT NOT NULL
);

CREATE TABLE [Borrowing]
(
	ID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	BookID INT NOT NULL,
	MemberID INT NOT NULL,
	BorrowDate DATE NOT NULL,
	ReturnDate DATE NOT NULL,

	FOREIGN KEY (BookID) REFERENCES Book(ID),
	FOREIGN KEY (MemberID) REFERENCES Member(ID)

);
DROP TABLE [Borrowing];
CREATE TABLE [Return]
(
	ID INT IDENTITY (1,1) PRIMARY KEY,
	BorrowID INT,
	BookID INT,
	MemberID INT,
	BorrowDate DATE,
	ReturnedDate DATE

	FOREIGN KEY (MemberID) REFERENCES Member(ID),
	FOREIGN KEY (BookID) REFERENCES Book(ID)
);
DROP TABLE [Return];

INSERT INTO [Login] VALUES
('kenulafernando','@hasith@'),
('hasithkenula','kenula123');

INSERT INTO [Member] (Name , Email , Contact_No) VALUES
('Test User','testuser@gmail.com','0701231234');


INSERT INTO [Book] (Author,Category,Publisher ,ISBN ,Title,QTY) VALUES 
('Frank Herbert','Science fiction','Ace Books','9780593099322','Dune',10);

INSERT INTO [Borrowing] VALUES (3,5,'2023-6-22','2023-6-29');

SELECT SUM(QTY) FROM Book;
SELECT * FROM [Book];
SELECT * FROM [Login];
SELECT * FROM [Member];
SELECT * FROM [Return];
SELECT * FROM [Borrowing];

SELECT COUNT(DISTINCT MemberID) 
FROM Borrowing;

SELECT COUNT(*) FROM [Member] WHERE ID = 1;

SELECT * FROM sys.tables WHERE name = 'Borrowing';