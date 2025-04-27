# TaskListProject

This is a simple Web API built with ASP.NET Core for managing a to-do list.

## Features

- Create, read, update, and delete to-do items
- In-memory database using Entity Framework Core
- Swagger support for easy testing
- Follows RESTful API conventions

## Getting Started

### Prerequisites

- [.NET SDK 9.0 (Preview)](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio Code](https://code.visualstudio.com/)
- [C# for Visual Studio Code extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

### Setup Instructions

1. Clone the repository:
   ```
   git clone https://github.com/RebeccaHayleyPickles/TaskListProject.git
   cd TaskListProject
   ```

2. Open the project folder in VS Code:
    ```
    code .
    ```

3. Restore dependencies and run the API:
    ```
    dotnet restore
    dotnet run
    ```

4. Open your browser to view the Swagger UI:
    ```https://localhost:{port}/swagger```
    Replace {port} with the actual port shown in your terminal.

### API Endpoints

| HTTP Type | Method | Endpoint | Description |
| --- | --- | --- | --- |
| GET | GetToDoItems | /api/todoitems | Get all to-do items |
| GET | GetToDoItem | /api/todoitems/{id} | Get a specific item |
| POST | CreateToDoItem | /api/todoitems | Add a new to-do item |
| PUT | UpdateToDoItem | /api/todoitems/{id} | Update an existing item |
| DELETE | DeleteToDoItem | /api/todoitems/{id} | Delete a to-do item |
