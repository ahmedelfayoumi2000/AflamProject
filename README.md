# Aflam API Documentation

This project provides a **RESTful API** for managing movies and genres. It allows users to perform CRUD operations on movies and genres, including uploading movie posters and validating data. Below is the detailed documentation for each endpoint.

---

## Overview

### Key Features:
- **Movies Management**: Create, read, update, and delete movies with poster uploads.
- **Genres Management**: Create, read, update, and delete genres.
- **Validation**: Ensures data integrity with validation for file types, sizes, and genre IDs.
- **Error Handling**: Provides detailed error responses for invalid requests.

### Technologies Used:
- **Backend**: ASP.NET Core
- **Database**: Entity Framework Core (SQL Server)
- **Mapping**: AutoMapper
- **File Handling**: Supports image uploads for movie posters.
- **Error Handling**: Custom API responses for validation and errors.

---

## Getting Started

Follow the steps below to set up and run the project locally.

### Prerequisites:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0)
- [Git](https://git-scm.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or a compatible database)

### Installation:
1. **Clone the repository**:
   ```bash
   git clone https://github.com/ahmedelfayoumi2000/AflamProject.git

2. **Install dependencies**:
   ```bash
   dotnet restore
2. **Configure the database**:
   ```bash
   dotnet ef database update
2. **Run the project**:
   ```bash
   dotnet run
---
# API Endpoints

## 1. **Movies Management**

### `GET /api/movies`
- **Description**: Retrieves a list of all movies.
- **Input**: None.
 
- **Output**:
  ```json
  [
  {
    "id": 1,
    "title": "string",
    "year": 2023,
    "rate": 7.5,
    "genreId": 1,
    "poster": "base64-encoded-string"
  }
  ]

### `GET /api/movies/{id}`
- **Description**: Retrieves details of a specific movie by ID.
- **Input**:  id as a route parameter.
   
- **Output**:
  ```json
   {
    "id": 1,
    "title": "string",
    "year": 2023,
    "rate": 7.5,
    "genreId": 1,
    "poster": "base64-encoded-string"
  }

### `POST /api/movies`
- **Description**: Creates a new movie with an optional poster.
- **Input**: 
    ```json
   {
  "title": "string",
  "year": 2023,
  "rate": 7.5,
  "genreId": 1,
  "poster": "file" (allowed formats: .jpg, .jpeg, .png, max size: 1MB)
  }
- **Output**:
  ```json
   {
  "id": 1,
  "title": "string",
  "year": 2023,
  "rate": 7.5,
  "genreId": 1,
  "poster": "base64-encoded-string"
  }

### `PUT /api/movies/{id}`
- **Description**: Updates an existing movie by ID.
- **Input**: 
  ```json
   {
  "title": "string",
  "year": 2023,
  "rate": 7.5,
  "genreId": 1,
  "poster": "file" (optional, allowed formats: .jpg, .jpeg, .png, max size: 1MB)
  }
- **Output**:
  ```json
   {
  "id": 1,
  "title": "string",
  "year": 2023,
  "rate": 7.5,
  "genreId": 1,
  "poster": "base64-encoded-string"
  }
### `DELETE /api/movies/{id}`
- **Description**: Deletes a movie by ID.
- **Input**: id as a route parameter.
     
- **Output**:
   ```json
  {
  "statusCode": 200,
  "message": "Movie with ID {id} deleted successfully."
  }

---

## 2. **Genres Management**

### `GET /api/genres`
- **Description**: Retrieves a list of all genres.
- **Input**:: None.
- **Output**:
  ```json
  [
  {
    "id": 1,
    "name": "Action"
  }
  ]

### `POST /api/genres`
- **Description**: Creates a new genre.
- **Input**:
     ```json
   {
  "name": "string"
   }
    
- **Output**: Returns the updated basket.
    ```json
   {
    "id": 1,
    "name": "string"
   }


### `PUT /api/genres/{id}`
- **Description**: Updates an existing genre by ID.
- **Input**: basketId as a query parameter.
   ```json
   {
  "name": "string"
  }
- **Input**:
     ```json
   {
  "id": 1,
  "name": "string"
  }

### `DELETE /api/genres/{id}`
- **Description**: Deletes a genre by ID.
- **Input**: id as a route parameter.
    
- **Output**:
  ```json
  {
  "statusCode": 200,
  "message": "Genre with ID {id} deleted successfully."
  }

---

## 3. ** Error Responses**
  
### `Validation Errors`
- **Description**: Invalid File Type:
  
   ```json
  {
  "errors": ["Only .png, .jpg, and .jpeg are allowed!"]
  }

- **Description**: Invalid File Size:

   ```json
   {
    "errors": ["Max allowed size for poster is 1MB!"]
  }


- **Description**: Invalid Genre ID:
   ```json
  {
  "errors": ["Invalid genre ID!"]
  }

### `Not Found Errors`
- **Description**:Movie Not Found:
   ```json
  {
    "statusCode": 404,
    "message": "Movie with ID {id} not found."
  }

- **Description**: Genre Not Found:
  
  ```json
   {
    "statusCode": 404,
    "message": "Genre with ID {id} not found."
  }
