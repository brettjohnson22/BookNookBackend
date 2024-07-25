# Book Nook Backend

## Project Overview

This project is a RESTful web API that allows users to review books and manage their favorite books. The application includes a secure backend with JWT authorization for user authentication and various endpoints to handle CRUD operations for reviews and favorites. It is built using C# with .NET Core and leverages a MySQL database for data storage.

## Features

- **User Authentication:** Secure JWT-based authentication to ensure that only authorized users can create and manage reviews and favorites.
- **Review Management:** Users can add, update, delete, and view reviews for books. Each review includes a rating, text, and association with a specific user and book.
- **Favorites Management:** Users can add, view, and delete books from their favorites list. Each favorite includes details like book ID, title, and thumbnail URL.
- **Detailed Book Information:** Endpoints to fetch detailed book information, including all related reviews and average user ratings.
- **Data Transfer Objects (DTOs):** Custom DTO classes to structure API responses and avoid exposing sensitive user data.
- **Entity Relationship Diagram (ERD):** A visual representation of the database schema, showing models, fields, and relationships.

## Technologies Used

- **Backend:**
  - C#
  - .NET Core / .NET Framework
  - Entity Framework
  - LINQ
- **Databases:**
  - MySQL
