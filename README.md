# Manga-Store-Management-System
A WPF application for managing a manga bookstore. The application retrieves manga books from a database and allows users to view, edit, add, and remove books and authors. Users can also manage inventory across multiple stores.

## Features:
- Display manga books from the database.
- Edit manga book details such as release date, author, and illustrator.
- Add new manga books and authors.
- Remove manga books and authors.
- Manage inventory across multiple stores.

## To get started
2. Clone this repository to your local machine.
2. Set up the database using the provided SQL scripts and instructions.
   - [Download SQL Script](/script.sql) and run the script in your SQL Server Management Studio (SSMS) to create the database.
   - [Download SQL Query](/DataQuery.sql) and run the data script in SSMS to populate the database with sample data. 
4. Open the WPF application in Visual Studio or your preferred IDE.
5. Find the connectionString section in BookStoreContext.cs and update the Data Source value with the name of your SQL Server instance.
6. Run the application and explore its features.
