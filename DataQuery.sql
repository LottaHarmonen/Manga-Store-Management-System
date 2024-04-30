USE BookStore;

-- Insert authors for each book
INSERT INTO Authors (AuthorId, Firstname, Lastname, Date_Of_Birth)
VALUES 
(1, 'Akira', 'Toriyama', '1955-04-05'), -- Dragon Ball Z
(2, 'Eiichiro', 'Oda', '1975-01-01'), -- One Piece
(3, 'Naoko', 'Takeuchi', '1967-03-15'), -- Sailor Moon
(4, 'Masashi', 'Kishimoto', '1974-11-08'), -- Naruto
(5, 'Hajime', 'Isayama', '1986-08-29'), -- Attack on Titan
(6, 'Kohei', 'Horikoshi', '1986-11-20'), -- My Hero Academia
(7, 'Koyoharu', 'Gotouge', '1988-05-05'); -- Demon Slayer

-- Insert books into the Books table
INSERT INTO Books (ISBN, Name, Language)
VALUES 
(9781421526928, 'Dragon Ball Z', 'English'),
(9781421526898, 'One Piece', 'English'),
(9781612620055, 'Sailor Moon', 'English'),
(9781421525822, 'Naruto', 'English'),
(9781632369498, 'Attack on Titan', 'English'),
(9781974710095, 'My Hero Academia', 'English'),
(9781974728687, 'Demon Slayer', 'English');


-- Insert illustrators into the Illustrators table
INSERT INTO [dbo].[Illustrators] ([IllustratorId], [Firstname], [Lastname], [Date_Of_Birth])
VALUES 
(1, 'Illustrator', 'One', '1980-01-01'),
(2, 'Illustrator', 'Two', '1975-05-15'),
(3, 'Illustrator', 'Three', '1990-10-20'),
(4, 'Illustrator', 'Four', '1990-06-20'),
(5, 'Illustrator', 'Five', '1988-12-10'),
(6, 'Illustrator', 'Six', '1995-04-30');

-- Insert demographic data into the Demographics table
INSERT INTO [dbo].[Demographics] ([DemographicId], [Name])
VALUES 
(1, 'Shonen'),
(2, 'Seinen'),
(3, 'Shojo');

-- Insert price groups into the Price_Groups table
INSERT INTO [dbo].[Price_Groups] ([Price_GroupId], [Price], [Currency])
VALUES 
(1, 50, 'USD'),
(2, 60, 'USD'),
(3, 70, 'USD');

-- Insert suppliers into the Suppliers table
INSERT INTO [dbo].[Suppliers] ([SupplierId], [Name], [Contact_Number])
VALUES 
(1, 'Supplier One', 1111111111),
(2, 'Supplier Two', 2222222222),
(3, 'Supplier Three', 3333333333);

-- Insert both stores into the Stores table
INSERT INTO Stores (StoreId, Name, Streetname, Building_Number, Postal_Code, City)
VALUES 
(1, 'First Store', 'First Street', 123, 12345, 'First City'),
(2, 'Second Store', 'Second Street', 456, 54321, 'Second City');

INSERT INTO Stock_Balance (StoreId, ISBN, Quantity)
VALUES 
(1, 9781421526928, 50), -- Dragon Ball Z
(1, 9781421526898, 100), -- One Piece
(1, 9781612620055, 60), -- Sailor Moon
(1, 9781421525822, 72), -- Naruto
(1, 9781632369498, 75), -- Attack on Titan
(2, 9781421526928, 40), -- Dragon Ball Z
(2, 9781421526898, 80), -- One Piece
(2, 9781612620055, 50), -- Sailor Moon
(2, 9781421525822, 60), -- Naruto
(2, 9781974728687, 50); -- Demon Slayer



-- Insert series into the Series table
INSERT INTO [dbo].[Series] ([SeriesId], [Name], [Demographic], [Price_GroupId])
VALUES 
(1, 'Naruto', 1, 1),
(2, 'One Piece', 1, 1),
(3, 'Sailor Moon', 1, 3),
(4, 'Attack on Titan', 2, 1),
(5, 'My Hero Academia', 1, 2),
(6, 'Demon Slayer', 2, 2),
(7, 'Dragon Ball Z', 1, 1);

INSERT INTO [dbo].[Series_Specifications] ([SeriesId], [AuthorId], [IllustratorId], [SupplierId], [Release_Date], [Number_Of_Books])
VALUES 
-- For Dragon Ball Z
(7, 1, 4, 1, '1985-12-03', 42), 
-- For One Piece
(2, 2, 5, 2, '1997-07-19', 100),
-- For Sailor Moon
(3, 3, 3, 3, '1992-07-06', 60),
-- For Naruto
(1, 4, 1, 1, '1999-09-21', 72),
-- For Attack on Titan
(4, 5, 2, 1, '2009-09-09', 75),
-- For My Hero Academia
(5, 6, 5, 2, '2014-07-07', 80),
-- For Demon Slayer
(6, 7, 5, 3, '2016-02-15', 60);


-- Insert book specifications into the Book_Specifications table
INSERT INTO [dbo].[Book_Specifications] ([ISBN], [AuthorId], [IllustratorId], [SeriesId], [Publication_Date])
VALUES 
(9781421526928, 1, 4, 7, '1985-12-03'), -- Dragon Ball Z
(9781421526898, 2, 5, 2, '1997-07-19'), -- One Piece
(9781612620055, 3, 3, 3, '1992-07-06'), -- Sailor Moon
(9781421525822, 4, 1, 1, '1999-09-21'), -- Naruto
(9781632369498, 5, 2, 4, '2009-09-09'), -- Attack on Titan
(9781974710095, 6, 5, 5, '2014-07-07'), -- My Hero Academia
(9781974728687, 7, 5, 6, '2016-02-15'); -- Demon Slayer